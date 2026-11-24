// Nombre: Richard Mancachi Tantani
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

record Producto(string Nombre, int Peso, int Valor);

record Medicion(double[] Tiempos, double Promedio);

class Program
{
    const int Capacidad = 4700;

    static void Main()
    {
        Console.WriteLine("H3 - COMPARACION DE ALGORITMOS");
        Console.WriteLine("Nombre: Richard Mancachi Tantani");
        Console.WriteLine("Capacidad: 4700 gramos");
        Console.WriteLine();

        Console.WriteLine("DEMOSTRACION DEL PROBLEMA");

        var ejemplo = CrearProductos(8);

        int voraz = Voraz(ejemplo);
        int fuerza = FuerzaBruta(ejemplo);
        int dinamica = Dinamica(ejemplo);

        Console.WriteLine("Voraz: Bs " + voraz);
        Console.WriteLine("Fuerza bruta: Bs " + fuerza);
        Console.WriteLine("Programacion dinamica: Bs " + dinamica);

        Console.WriteLine("Los resultados optimos coinciden: "
            + (fuerza == dinamica));

        Console.WriteLine();
        Console.WriteLine("COMPARACION DE TIEMPOS");
        Console.WriteLine("1 calentamiento y 3 corridas medidas.");
        Console.WriteLine();

        Console.WriteLine(
            "n       Voraz ms   Factor   Fuerza ms  Factor   Dinamica ms Factor");

        double anteriorV = 0;
        double anteriorF = 0;
        double anteriorD = 0;

        int[] tamanos = { 16, 18, 20, 22 };

        foreach (int n in tamanos)
        {
            var productos = CrearProductos(n);

            Medicion mv = Medir(() => Voraz(productos), 1000);
            Medicion mf = Medir(() => FuerzaBruta(productos), 1);
            Medicion md = Medir(() => Dinamica(productos), 20);

            Console.WriteLine();
            Console.WriteLine("OBJETOS: " + n);

            MostrarCorridas("Voraz", mv);
            MostrarCorridas("Fuerza bruta", mf);
            MostrarCorridas("Programacion dinamica", md);

            double fv = anteriorV == 0 ? 0 : mv.Promedio / anteriorV;
            double ff = anteriorF == 0 ? 0 : mf.Promedio / anteriorF;
            double fd = anteriorD == 0 ? 0 : md.Promedio / anteriorD;

            Console.WriteLine(
                $"CRECIMIENTO: Voraz {Factor(fv)} | Fuerza {Factor(ff)} | Dinamica {Factor(fd)}");

            if (mf.Promedio > 0)
                Console.WriteLine(
                    $"Fuerza bruta / Dinamica: {mf.Promedio / md.Promedio:F2} veces");

            anteriorV = mv.Promedio;
            anteriorF = mf.Promedio;
            anteriorD = md.Promedio;
        }

        Console.WriteLine();
        Console.WriteLine("FIN DE LAS MEDICIONES");
    }

    static List<Producto> CrearProductos(int n)
    {
        var lista = new List<Producto>();

        for (int i = 0; i < n; i++)
        {
            if (i % 8 == 7)
                lista.Add(new Producto("UNO 4,7 kg", 4700, 90));
            else
                lista.Add(new Producto("UNO 620 g", 620, 12));
        }

        return lista;
    }

    // ALGORITMO VORAZ
    static int Voraz(List<Producto> productos)
    {
        int peso = 0;
        int valor = 0;

        var ordenados = productos
            .OrderByDescending(p => (double)p.Valor / p.Peso);

        foreach (var p in ordenados)
        {
            if (peso + p.Peso <= Capacidad)
            {
                peso += p.Peso;
                valor += p.Valor;
            }
        }

        return valor;
    }

    // FUERZA BRUTA
    static int FuerzaBruta(List<Producto> productos)
    {
        int n = productos.Count;
        int mejor = 0;
        int combinaciones = 1 << n;

        for (int mascara = 0; mascara < combinaciones; mascara++)
        {
            int peso = 0;
            int valor = 0;

            for (int i = 0; i < n; i++)
            {
                if ((mascara & (1 << i)) != 0)
                {
                    peso += productos[i].Peso;
                    valor += productos[i].Valor;

                    if (peso > Capacidad)
                        break;
                }
            }

            if (peso <= Capacidad && valor > mejor)
                mejor = valor;
        }

        return mejor;
    }

    // PROGRAMACION DINAMICA
    static int Dinamica(List<Producto> productos)
    {
        int n = productos.Count;
        int[,] dp = new int[n + 1, Capacidad + 1];

        for (int i = 1; i <= n; i++)
        {
            Producto p = productos[i - 1];

            for (int c = 0; c <= Capacidad; c++)
            {
                dp[i, c] = dp[i - 1, c];

                if (p.Peso <= c)
                {
                    int nuevo = p.Valor + dp[i - 1, c - p.Peso];
                    dp[i, c] = Math.Max(dp[i, c], nuevo);
                }
            }
        }

        return dp[n, Capacidad];
    }

    static Medicion Medir(Func<int> accion, int repeticiones)
    {
        GC.KeepAlive(accion());

        double[] tiempos = new double[3];

        for (int i = 0; i < 3; i++)
        {
            long acumulado = 0;
            var reloj = Stopwatch.StartNew();

            for (int j = 0; j < repeticiones; j++)
                acumulado += accion();

            reloj.Stop();
            GC.KeepAlive(acumulado);

            tiempos[i] =
                reloj.Elapsed.TotalMilliseconds / repeticiones;
        }

        return new Medicion(tiempos, tiempos.Average());
    }
    static void MostrarCorridas(string nombre, Medicion m)
    {
        for (int i = 0; i < 3; i++)
            Console.WriteLine(
                $"{nombre} corrida {i + 1}: {m.Tiempos[i]:F3} ms");

        Console.WriteLine(
            $"{nombre} promedio: {m.Promedio:F3} ms");
    }

    static string Factor(double f)
    {
        return f == 0 ? "-" : f.ToString("F2") + "x";
    }
}

