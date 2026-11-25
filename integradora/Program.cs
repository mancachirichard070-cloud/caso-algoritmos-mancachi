// Nombre: Richard Mancachi Tantani
// Evaluacion Integradora - Variante B: Compras de una escuela
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Integradora.VarianteB;

public sealed record Util(string Codigo, int CostoBs, int Utilidad);

public interface IComprador
{
    (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs);
    bool EstaEnLaCompra(string codigo);
}

public sealed class CompradorIngenuo : IComprador
{
    private List<string> _elegidos = new();

    public (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs)
    {
        int n = utiles.Length, mejor = 0;
        long mejorMascara = 0;

        for (long mascara = 0; mascara < (1L << n); mascara++)
        {
            int costo = 0, utilidad = 0;
            for (int i = 0; i < n; i++)
                if ((mascara & (1L << i)) != 0)
                {
                    costo += utiles[i].CostoBs;
                    utilidad += utiles[i].Utilidad;
                }

            if (costo <= presupuestoBs && utilidad > mejor)
            {
                mejor = utilidad;
                mejorMascara = mascara;
            }
        }

        _elegidos = new List<string>();
        for (int i = 0; i < n; i++)
            if ((mejorMascara & (1L << i)) != 0)
                _elegidos.Add(utiles[i].Codigo);

        return (mejor, new List<string>(_elegidos));
    }

    public bool EstaEnLaCompra(string codigo) => _elegidos.Contains(codigo);
}

public sealed class CompradorMejorado : IComprador
{
    private HashSet<string> _enCompra = new(StringComparer.Ordinal);

    public (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs)
    {
        _enCompra.Clear();

        if (utiles == null)
            throw new ArgumentNullException(nameof(utiles));
        if (presupuestoBs < 0)
            return (0, new List<string>());

        int n = utiles.Length;
        if (n == 0)
            return (0, new List<string>());

        long costoTotal = 0;
        long utilidadTotal = 0;

        foreach (var u in utiles)
        {
            if (u.CostoBs < 0 || u.Utilidad < 0)
                throw new ArgumentException("No se admiten costos ni utilidades negativos.");
            costoTotal += u.CostoBs;
            utilidadTotal += u.Utilidad;
        }

        if (utilidadTotal >= int.MaxValue)
            throw new ArgumentException("La utilidad total excede el rango de int.");

        // Si todos los utiles caben, elegir todos los que aportan utilidad positiva.
        if (costoTotal <= presupuestoBs)
        {
            var todos = new List<string>();
            int total = 0;
            foreach (var u in utiles)
            {
                if (u.Utilidad > 0)
                {
                    todos.Add(u.Codigo);
                    _enCompra.Add(u.Codigo);
                    total = checked(total + u.Utilidad);
                }
            }
            return (total, todos);
        }

        int U = (int)utilidadTotal;
        const long INF = long.MaxValue / 4;

        // dp[u] = costo minimo para obtener exactamente utilidad u.
        long[] dp = new long[U + 1];
        Array.Fill(dp, INF);
        dp[0] = 0;

        // Decisiones para reconstruir la seleccion optima.
        bool[,] tomar = new bool[n, U + 1];

        for (int i = 0; i < n; i++)
        {
            int costo = utiles[i].CostoBs;
            int valor = utiles[i].Utilidad;
            if (valor <= 0)
                continue;

            // Descendente para que cada util se use una sola vez.
            for (int u = U; u >= valor; u--)
            {
                if (dp[u - valor] == INF)
                    continue;

                long nuevoCosto = dp[u - valor] + costo;
                if (nuevoCosto <= presupuestoBs && nuevoCosto < dp[u])
                {
                    dp[u] = nuevoCosto;
                    tomar[i, u] = true;
                }
            }
        }

        int mejor = U;
        while (mejor > 0 && dp[mejor] == INF)
            mejor--;

        int utilidadMaxima = mejor;
        var elegidos = new List<string>();

        for (int i = n - 1; i >= 0; i--)
        {
            int valor = utiles[i].Utilidad;
            if (valor > 0 && mejor >= valor && tomar[i, mejor])
            {
                elegidos.Add(utiles[i].Codigo);
                mejor -= valor;
            }
        }

        elegidos.Reverse();
        _enCompra = new HashSet<string>(elegidos, StringComparer.Ordinal);
        return (utilidadMaxima, elegidos);
    }

    public bool EstaEnLaCompra(string codigo) => _enCompra.Contains(codigo);
}

public static class Programa
{
    private static Util[] Lote(params (string c, int costo, int u)[] datos) =>
        datos.Select(d => new Util(d.c, d.costo, d.u)).ToArray();

    private static readonly (string que, Util[] utiles, int presupuesto, int esperada, string[] codigos)[] Casos =
    {
        ("el voraz se equivoca", Lote(("U1",20,120),("U2",40,200),("U3",60,240)), 100, 440, new[]{"U2","U3"}),
        ("cuatro utiles", Lote(("U1",4,6),("U2",6,8),("U3",8,10),("U4",10,12)), 16, 20, new[]{"U2","U4"}),
        ("presupuesto cero", Lote(("U1",5,10),("U2",7,12)), 0, 0, Array.Empty<string>()),
        ("sin utiles", Lote(), 30, 0, Array.Empty<string>()),
        ("nada cabe", Lote(("U1",80,90),("U2",100,100)), 60, 0, Array.Empty<string>()),
        ("entran todos", Lote(("U1",10,20),("U2",14,24),("U3",6,8)), 200, 52, new[]{"U1","U2","U3"})
    };

    private sealed record Medicion(double[] Tiempos, double Promedio, int Utilidad);

    private static void Probar(string titulo, Func<IComprador> fabrica)
    {
        int bien = 0, total = 0;
        foreach (var (que, utiles, presupuesto, esperada, codigos) in Casos)
        {
            var c = fabrica();
            var (utilidad, elegidos) = c.Elegir(utiles, presupuesto);
            total++;
            bool ok = utilidad == esperada &&
                elegidos.OrderBy(x => x).SequenceEqual(codigos.OrderBy(x => x));
            if (ok) bien++;
            else Console.WriteLine($"MAL en {que}: esperado {esperada} [{string.Join(",",codigos)}], obtuvo {utilidad} [{string.Join(",",elegidos)}]");
        }

        var consulta = fabrica();
        consulta.Elegir(Casos[0].utiles, Casos[0].presupuesto);
        total += 2;
        if (consulta.EstaEnLaCompra("U2")) bien++;
        else Console.WriteLine("MAL: EstaEnLaCompra(U2), esperado True.");
        if (!consulta.EstaEnLaCompra("U1")) bien++;
        else Console.WriteLine("MAL: EstaEnLaCompra(U1), esperado False.");

        Console.WriteLine($"{titulo}: {bien} de {total} pruebas correctas");
    }

    public static int EjecutarCarga(IComprador comprador, int n)
    {
        var azar = new Random(n + 1000);
        var utiles = Enumerable.Range(0, n)
            .Select(i => new Util("K" + i, azar.Next(4, 50), azar.Next(10, 100)))
            .ToArray();
        int presupuesto = utiles.Sum(u => u.CostoBs) / 3;
        return comprador.Elegir(utiles, presupuesto).utilidad;
    }

    private static Medicion Medir(Func<int> accion)
    {
        _ = accion(); // calentamiento
        double[] tiempos = new double[3];
        int utilidad = 0;

        for (int i = 0; i < 3; i++)
        {
            var reloj = Stopwatch.StartNew();
            utilidad = accion();
            reloj.Stop();
            tiempos[i] = reloj.Elapsed.TotalMilliseconds;
        }

        return new Medicion(tiempos, tiempos.Average(), utilidad);
    }

    private static string Factor(double actual, double anterior) =>
        anterior <= 0 ? "-" : (actual / anterior).ToString("F2") + "x";

    private static void Mostrar(string nombre, Medicion m)
    {
        Console.WriteLine($"{nombre}: {m.Tiempos[0]:F3}, {m.Tiempos[1]:F3}, {m.Tiempos[2]:F3} ms; promedio {m.Promedio:F3} ms; utilidad {m.Utilidad}");
    }

    public static void Main()
    {
        Console.WriteLine("EVALUACION INTEGRADORA - VARIANTE B");
        Console.WriteLine("Nombre: Richard Mancachi Tantani\n");

        Probar("CompradorIngenuo", () => new CompradorIngenuo());
        Probar("CompradorMejorado", () => new CompradorMejorado());

        Console.WriteLine("\nMEDICIONES: 1 calentamiento + 3 corridas");
        Console.WriteLine("n | Ingenua ms | Factor | Mejorada ms | Factor | Misma utilidad");

        int[] tamanos = { 20, 22, 24, 26 };
        double antI = 0, antM = 0;

        foreach (int n in tamanos)
        {
            var ingenua = Medir(() => EjecutarCarga(new CompradorIngenuo(), n));
            var mejorada = Medir(() => EjecutarCarga(new CompradorMejorado(), n));
            bool igual = ingenua.Utilidad == mejorada.Utilidad;

            Console.WriteLine($"{n} | {ingenua.Promedio:F3} | {Factor(ingenua.Promedio, antI)} | {mejorada.Promedio:F3} | {Factor(mejorada.Promedio, antM)} | {igual}");
            Mostrar("Ingenua n=" + n, ingenua);
            Mostrar("Mejorada n=" + n, mejorada);

            antI = ingenua.Promedio;
            antM = mejorada.Promedio;
        }

        Console.WriteLine("\nPRUEBA: 1.000 utiles, presupuesto Bs 10.000");
        var random = new Random(20261124);
        var utiles1000 = Enumerable.Range(0, 1000)
            .Select(i => new Util("G" + i, random.Next(4, 50), random.Next(10, 100)))
            .ToArray();

        var grande = Medir(() =>
            new CompradorMejorado().Elegir(utiles1000, 10000).utilidad);
        Mostrar("Mejorada con 1.000 utiles", grande);

        Console.WriteLine("\nPRESUPUESTO Bs 1.000.000.000");
        var gigante = new CompradorMejorado().Elegir(utiles1000, 1_000_000_000);
        Console.WriteLine($"Utilidad: {gigante.utilidad}; utiles elegidos: {gigante.codigos.Count}");
        Console.WriteLine("El presupuesto gigante permite seleccionar directamente todos los utiles positivos si el costo total cabe.");

        Console.WriteLine("\nFIN DE LAS PRUEBAS Y MEDICIONES");
    }
}