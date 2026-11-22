// Nombre: Richard Mancachi Tantani
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parcial2.VarianteA;

public interface IGestorTurnos
{
    void Registrar(string nombre, int urgencia);
    string? AtenderSiguiente();
    bool EstaEsperando(string nombre);
    int Esperando { get; }
}

public sealed record Paciente(string Nombre, int Urgencia, int Llegada);

// SOLUCION INGENUA
public sealed class GestorIngenuo : IGestorTurnos
{
    private readonly List<Paciente> _lista = new();
    private int _llegadas;

    public void Registrar(string nombre, int urgencia)
    {
        _lista.Add(new Paciente(nombre, urgencia, _llegadas++));
    }

    public string? AtenderSiguiente()
    {
        if (_lista.Count == 0) return null;

        int mejor = 0;

        for (int i = 1; i < _lista.Count; i++)
        {
            var p = _lista[i];
            var m = _lista[mejor];

            if (p.Urgencia < m.Urgencia ||
                (p.Urgencia == m.Urgencia && p.Llegada < m.Llegada))
            {
                mejor = i;
            }
        }

        string nombre = _lista[mejor].Nombre;
        _lista.RemoveAt(mejor);
        return nombre;
    }

    public bool EstaEsperando(string nombre)
    {
        foreach (var p in _lista)
        {
            if (p.Nombre == nombre) return true;
        }

        return false;
    }

    public int Esperando => _lista.Count;
}

// SOLUCION MEJORADA
public sealed class GestorMejorado : IGestorTurnos
{
    private readonly PriorityQueue<Paciente, (int Urgencia, int Llegada)> _cola = new();

    private readonly Dictionary<string, Paciente> _espera =
        new(StringComparer.Ordinal);

    private int _llegadas;

    public void Registrar(string nombre, int urgencia)
    {
        var paciente = new Paciente(nombre, urgencia, _llegadas++);

        _cola.Enqueue(paciente, (urgencia, paciente.Llegada));
        _espera.Add(nombre, paciente);
    }

    public string? AtenderSiguiente()
    {
        if (_cola.Count == 0) return null;

        var paciente = _cola.Dequeue();
        _espera.Remove(paciente.Nombre);

        return paciente.Nombre;
    }

    public bool EstaEsperando(string nombre)
    {
        return _espera.ContainsKey(nombre);
    }

    public int Esperando => _espera.Count;
}

// PRUEBAS
public static class Programa
{
    private static readonly (string op, string arg, int urg, string esperado)[] Guion =
    {
        ("registrar", "Ana", 3, ""),
        ("registrar", "Beto", 1, ""),
        ("registrar", "Carla", 3, ""),
        ("registrar", "Diego", 2, ""),
        ("esperando", "", 0, "4"),
        ("esta", "Beto", 0, "True"),
        ("esta", "Zoe", 0, "False"),
        ("atender", "", 0, "Beto"),
        ("esta", "Beto", 0, "False"),
        ("atender", "", 0, "Diego"),
        ("registrar", "Elena", 1, ""),
        ("atender", "", 0, "Elena"),
        ("atender", "", 0, "Ana"),
        ("atender", "", 0, "Carla"),
        ("atender", "", 0, "(nadie)"),
        ("esperando", "", 0, "0")
    };

    private static string Ejecutar(IGestorTurnos g, string op, string arg, int urg)
    {
        switch (op)
        {
            case "registrar":
                g.Registrar(arg, urg);
                return "";

            case "atender":
                return g.AtenderSiguiente() ?? "(nadie)";

            case "esta":
                return g.EstaEsperando(arg).ToString();

            default:
                return g.Esperando.ToString();
        }
    }

    private static void Probar(string titulo, IGestorTurnos g)
    {
        int bien = 0;
        int total = 0;

        foreach (var (op, arg, urg, esperado) in Guion)
        {
            string obtenido = Ejecutar(g, op, arg, urg);

            if (op == "registrar") continue;

            total++;

            if (obtenido == esperado)
                bien++;
            else
                Console.WriteLine($"ERROR en {op} {arg}: esperado {esperado}, obtenido {obtenido}");
        }

        Console.WriteLine($"{titulo}: {bien} de {total} pasos correctos");
    }

// Carga de pacientes para realizar las mediciones
public static long EjecutarCarga(IGestorTurnos g, int n)
{
    for (int i = 0; i < n; i++)
    {
        g.Registrar("P" + i, (int)((i * 7919L) % 5) + 1);
    }

    long control = 0;

    for (int i = 0; i < n; i++)
    {
        control += g.AtenderSiguiente()!.Length;
    }

    return control;
}

// Realiza calentamiento, tres corridas y calcula el promedio
private static double Medir(
    string nombre,
    Func<IGestorTurnos> crear,
    int n)
{
    Console.WriteLine($"\n{nombre} con {n:N0} pacientes");

    // Calentamiento
    EjecutarCarga(crear(), n);

    double suma = 0;

    for (int i = 1; i <= 3; i++)
    {
        var gestor = crear();

        var reloj = Stopwatch.StartNew();
        long control = EjecutarCarga(gestor, n);
        reloj.Stop();

        double tiempo = reloj.Elapsed.TotalMilliseconds;
        suma += tiempo;

        Console.WriteLine($"Corrida {i}: {tiempo:F2} ms");
    }

    double promedio = suma / 3;

    Console.WriteLine($"PROMEDIO: {promedio:F2} ms");

    return promedio;
}

   
public static void Main()
{
    Console.WriteLine("PARCIAL 2 - VARIANTE A");
    Console.WriteLine("TURNOS DE UNA CLINICA");
    Console.WriteLine("==============================");

    Probar("GestorIngenuo ", new GestorIngenuo());
    Probar("GestorMejorado", new GestorMejorado());

    int[] tamanos = { 10_000, 20_000, 40_000 };

    double anteriorIngenuo = 0;
    double anteriorMejorado = 0;

    foreach (int n in tamanos)
    {
        Console.WriteLine("\n==============================");
        Console.WriteLine($"COMPARACION CON {n:N0} PACIENTES");

        double ingenuo = Medir(
            "GESTOR INGENUO",
            () => new GestorIngenuo(),
            n
        );

        double mejorado = Medir(
            "GESTOR MEJORADO",
            () => new GestorMejorado(),
            n
        );

        if (anteriorIngenuo > 0)
        {
            Console.WriteLine(
                $"Crecimiento ingenuo: {ingenuo / anteriorIngenuo:F2}x"
            );

            Console.WriteLine(
                $"Crecimiento mejorado: {mejorado / anteriorMejorado:F2}x"
            );
        }

        anteriorIngenuo = ingenuo;
        anteriorMejorado = mejorado;
    }
}

}

