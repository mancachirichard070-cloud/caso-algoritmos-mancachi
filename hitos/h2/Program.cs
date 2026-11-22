// Nombre: Richard Mancachi Tantani

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

record Venta(
    int Codigo,
    DateTime Fecha,
    string Producto,
    int Cantidad,
    decimal PrecioUnitario)
{
    public decimal Total => Cantidad * PrecioUnitario;
}

// VERSION INGENUA: utiliza una lista.
class GestorIngenuo
{
    private readonly List<Venta> ventas = new();

    public void Registrar(Venta venta)
    {
        ventas.Add(venta);
    }

    public Venta? Buscar(int codigo)
    {
        foreach (Venta venta in ventas)
        {
            if (venta.Codigo == codigo)
                return venta;
        }

        return null;
    }
}

// VERSION MEJORADA: utiliza un Dictionary.
class GestorMejorado
{
    private readonly Dictionary<int, Venta> ventas = new();

    public void Registrar(Venta venta)
    {
        ventas.Add(venta.Codigo, venta);
    }

    public Venta? Buscar(int codigo)
    {
        return ventas.TryGetValue(codigo, out Venta? venta)
            ? venta
            : null;
    }
}

class Program
{
    const int Consultas = 3000;

    static readonly Venta[] Muestra =
    {
        new(1, new(2026,10,20), "UNO 620 g", 2, 12m),
        new(2, new(2026,10,20), "UNO 4,7 kg", 1, 90m),
        new(3, new(2026,10,21), "UNO 620 g", 5, 12m),
        new(4, new(2026,10,21), "UNO 620 g", 3, 12m),
        new(5, new(2026,10,22), "UNO 4,7 kg", 2, 90m),
        new(6, new(2026,10,22), "UNO 620 g", 15, 12m),
        new(7, new(2026,10,23), "UNO 620 g", 4, 12m),
        new(8, new(2026,10,23), "UNO 4,7 kg", 1, 90m),
        new(9, new(2026,10,24), "UNO 620 g", 6, 12m),
        new(10, new(2026,10,24), "UNO 620 g", 2, 12m),
        new(11, new(2026,10,25), "UNO 4,7 kg", 2, 90m),
        new(12, new(2026,10,25), "UNO 620 g", 15, 12m),
        new(13, new(2026,10,26), "UNO 620 g", 3, 12m),
        new(14, new(2026,10,26), "UNO 4,7 kg", 1, 90m),
        new(15, new(2026,10,27), "UNO 620 g", 7, 12m),
        new(16, new(2026,10,27), "UNO 620 g", 5, 12m),
        new(17, new(2026,10,28), "UNO 4,7 kg", 1, 90m),
        new(18, new(2026,10,28), "UNO 620 g", 15, 12m),
        new(19, new(2026,10,29), "UNO 620 g", 4, 12m),
        new(20, new(2026,10,29), "UNO 4,7 kg", 2, 90m)
    };

    static void Main()
    {
        Console.WriteLine("H2 - VENTAS DE DETERGENTES UNO");
        Console.WriteLine("Nombre: Richard Mancachi Tantani");
        Console.WriteLine("================================");

        int[] tamanos = { 2000, 4000, 8000, 16000 };

        double anteriorA = 0;
        double anteriorB = 0;

        foreach (int n in tamanos)
        {
            List<Venta> datos = CrearDatos(n);

            var ingenuo = new GestorIngenuo();
            var mejorado = new GestorMejorado();

            foreach (Venta venta in datos)
            {
                ingenuo.Registrar(venta);
                mejorado.Registrar(venta);
            }

            // Se busca el ultimo codigo para probar el peor caso
            // de la busqueda lineal.
            int[] consultas = Enumerable
                .Repeat(n, Consultas)
                .ToArray();

            // Verificar que ambas versiones den el mismo resultado.
            var pruebaA = ingenuo.Buscar(n);
            var pruebaB = mejorado.Buscar(n);

            if (pruebaA?.Codigo != pruebaB?.Codigo)
            {
                Console.WriteLine("ERROR: resultados diferentes.");
                return;
            }

            Resultado tiempoA = Medir(
                () => EjecutarIngenuo(ingenuo, consultas));

            Resultado tiempoB = Medir(
                () => EjecutarMejorado(mejorado, consultas));

            double factorA = anteriorA == 0
                ? 0 : tiempoA.Promedio / anteriorA;

            double factorB = anteriorB == 0
                ? 0 : tiempoB.Promedio / anteriorB;

            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"REGISTROS: {n:N0}");

            for (int i = 0; i < 3; i++)
                Console.WriteLine(
                    $"Ingenua corrida {i + 1}: {tiempoA.Tiempos[i]:F3} ms");

            Console.WriteLine(
                $"Ingenua promedio: {tiempoA.Promedio:F3} ms");

            for (int i = 0; i < 3; i++)
                Console.WriteLine(
                    $"Elegida corrida {i + 1}: {tiempoB.Tiempos[i]:F3} ms");

            Console.WriteLine(
                $"Elegida promedio: {tiempoB.Promedio:F3} ms");

            Console.WriteLine(
                $"Crecimiento ingenua: {(factorA == 0 ? "-" : factorA.ToString("F2") + "x")}");

            Console.WriteLine(
                $"Crecimiento elegida: {(factorB == 0 ? "-" : factorB.ToString("F2") + "x")}");

            anteriorA = tiempoA.Promedio;
            anteriorB = tiempoB.Promedio;
        }

        Console.WriteLine();
        Console.WriteLine("FIN DE LAS MEDICIONES");
    }

    static List<Venta> CrearDatos(int n)
    {
        var datos = new List<Venta>(n);

        for (int i = 0; i < n; i++)
        {
            Venta original = Muestra[i % Muestra.Length];

            datos.Add(new Venta(
                i + 1,
                original.Fecha.AddDays(
                    (i / Muestra.Length) * 10),
                original.Producto,
                original.Cantidad,
                original.PrecioUnitario
            ));
        }

        return datos;
    }

    static long EjecutarIngenuo(
        GestorIngenuo gestor, int[] consultas)
    {
        long resultado = 0;

        foreach (int codigo in consultas)
        {
            Venta? venta = gestor.Buscar(codigo);

            if (venta != null)
                resultado += venta.Codigo;
        }

        return resultado;
    }

    static long EjecutarMejorado(
        GestorMejorado gestor, int[] consultas)
    {
        long resultado = 0;

        foreach (int codigo in consultas)
        {
            Venta? venta = gestor.Buscar(codigo);

            if (venta != null)
                resultado += venta.Codigo;
        }

        return resultado;
    }

    record Resultado(double[] Tiempos, double Promedio);

    static Resultado Medir(Func<long> accion)
    {
        // Una corrida de calentamiento.
        GC.KeepAlive(accion());

        double[] tiempos = new double[3];

        for (int i = 0; i < 3; i++)
        {
            var reloj = Stopwatch.StartNew();

            long resultado = accion();

            reloj.Stop();
            GC.KeepAlive(resultado);

            tiempos[i] = reloj.Elapsed.TotalMilliseconds;
        }

        return new Resultado(
            tiempos,
            tiempos.Average()
        );
    }
}
