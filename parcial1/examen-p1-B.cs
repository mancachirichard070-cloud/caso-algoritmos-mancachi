// Nombre completo: Richard Mancachi Tantani
// PARCIAL 1 · VARIANTE B — "Registro de comprobantes"
// Problema: dada una lista de comprobantes de pago, ¿hay alguno repetido?
// La función HayComprobantesRepetidosPorPares es CORRECTA pero LENTA. Tu trabajo: analizarla, CURARLA (escribir
// una versión que cueste menos), medir las dos y elegir. Puedes usar este archivo tal cual (si tienes .NET)
// o portar todo a TU lenguaje.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parcial1.VarianteB;

public static class Caja
{
    // Version original: compara cada elemento con todos los siguientes.
    public static bool HayComprobantesRepetidosPorPares(int[] comprobantes)
    {
        for (int a = 0; a < comprobantes.Length; a++)
            for (int b = a + 1; b < comprobantes.Length; b++)
                if (comprobantes[a] == comprobantes[b]) return true;
        return false;
    }

    // Version curada: HashSet permite detectar un valor ya visto en tiempo esperado O(1).
    // No ordena ni modifica el arreglo recibido.
    public static bool HayComprobantesRepetidosCurado(int[] comprobantes)
    {
        var vistos = new HashSet<int>();
        foreach (int comprobante in comprobantes)
        {
            if (!vistos.Add(comprobante))
                return true;
        }
        return false;
    }

    public static int[] GenerarSinRepetidos(int n, int semilla = 77)
    {
        var datos = new int[n];
        for (int i = 0; i < n; i++) datos[i] = 1000 + i;
        var azar = new Random(semilla);
        for (int i = n - 1; i > 0; i--)
        {
            int k = azar.Next(i + 1);
            (datos[i], datos[k]) = (datos[k], datos[i]);
        }
        return datos;
    }
}

public static class Programa
{
    private static readonly (int[] datos, bool esperado, string que)[] Casos =
    {
        (new[] { 1010, 1003, 1009, 1003, 1005 }, true, "repetido en el medio"),
        (new[] { 1010, 1003, 1009, 1004, 1005 }, false, "sin repetidos"),
        (Array.Empty<int>(), false, "lista vacia"),
        (new[] { 1008 }, false, "un solo elemento"),
        (new[] { 1004, 1001, 1002, 1006, 1004 }, true, "repetido en extremos")
    };

    private static double MedirMs(Func<int[], bool> funcion, int[] datos)
    {
        var reloj = Stopwatch.StartNew();
        bool resultado = funcion(datos);
        reloj.Stop();
        if (resultado) throw new Exception("La prueba de rendimiento esperaba datos sin repetidos.");
        return reloj.Elapsed.TotalMilliseconds;
    }

    public static void Main()
    {
        Console.WriteLine("PRUEBAS FUNCIONALES");
        foreach (var (datos, esperado, que) in Casos)
        {
            int[] copia = (int[])datos.Clone();
            bool original = Caja.HayComprobantesRepetidosPorPares(datos);
            bool curada = Caja.HayComprobantesRepetidosCurado(datos);
            bool noModifico = SonIguales(datos, copia);
            Console.WriteLine($"{que}: esperado={esperado}, original={original}, curada={curada}, no modifica={noModifico}");
            if (original != esperado || curada != esperado || !noModifico)
                throw new Exception("Fallo en una prueba funcional.");
        }

        // n=1.000; por tanto se prueban n, 2n, 4n y 8n.
        const int n = 1000;
        int[] tamanos = { n, 2 * n, 4 * n, 8 * n };
        Console.WriteLine("\nMEDICIONES EN MILISEGUNDOS (3 corridas; promedio)");
        Console.WriteLine("Tamano;Original_1;Original_2;Original_3;Promedio_original;Curada_1;Curada_2;Curada_3;Promedio_curada");

        foreach (int tamano in tamanos)
        {
            int[] datos = Caja.GenerarSinRepetidos(tamano, 77);

            // Calentamiento: no se incluye en el promedio.
            Caja.HayComprobantesRepetidosPorPares(datos);
            Caja.HayComprobantesRepetidosCurado(datos);

            double[] original = new double[3];
            double[] curada = new double[3];
            for (int i = 0; i < 3; i++)
            {
                original[i] = MedirMs(Caja.HayComprobantesRepetidosPorPares, datos);
                curada[i] = MedirMs(Caja.HayComprobantesRepetidosCurado, datos);
            }

            double promOriginal = (original[0] + original[1] + original[2]) / 3.0;
            double promCurada = (curada[0] + curada[1] + curada[2]) / 3.0;
            Console.WriteLine($"{tamano};{original[0]:F4};{original[1]:F4};{original[2]:F4};{promOriginal:F4};{curada[0]:F4};{curada[1]:F4};{curada[2]:F4};{promCurada:F4}");
        }
    }

    private static bool SonIguales(int[] a, int[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i]) return false;
        return true;
    }
}