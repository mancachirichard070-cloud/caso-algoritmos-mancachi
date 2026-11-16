using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

record Venta(string Fecha, string Producto, int Cantidad, decimal PrecioUnitario)
{
    public decimal Total => Cantidad * PrecioUnitario;
}

record Resumen(int Cantidad, decimal Importe);

class Program
{
    static readonly List<Venta> VentasReales = new()
    {
        new("2026-10-20", "UNO 620 g", 2, 12m),
        new("2026-10-20", "UNO 4,7 kg", 1, 90m),
        new("2026-10-21", "UNO 620 g", 5, 12m),
        new("2026-10-21", "UNO 620 g", 3, 12m),
        new("2026-10-22", "UNO 4,7 kg", 2, 90m),
        new("2026-10-22", "UNO 620 g", 15, 12m),
        new("2026-10-23", "UNO 620 g", 4, 12m),
        new("2026-10-23", "UNO 4,7 kg", 1, 90m),
        new("2026-10-24", "UNO 620 g", 6, 12m),
        new("2026-10-24", "UNO 620 g", 2, 12m),
        new("2026-10-25", "UNO 4,7 kg", 2, 90m),
        new("2026-10-25", "UNO 620 g", 15, 12m),
        new("2026-10-26", "UNO 620 g", 3, 12m),
        new("2026-10-26", "UNO 4,7 kg", 1, 90m),
        new("2026-10-27", "UNO 620 g", 7, 12m),
        new("2026-10-27", "UNO 620 g", 5, 12m),
        new("2026-10-28", "UNO 4,7 kg", 1, 90m),
        new("2026-10-28", "UNO 620 g", 15, 12m),
        new("2026-10-29", "UNO 620 g", 4, 12m),
        new("2026-10-29", "UNO 4,7 kg", 2, 90m)
    };

    const string ProductoConsultado = "UNO 620 g";
    const int Consultas = 1000;

    static void Main()
    {
        Console.WriteLine("CLASE 6 - ELECCION DE ALGORITMO");
        Console.WriteLine("REGISTRO DE VENTAS E INVENTARIO DE DETERGENTES UNO");
        Console.WriteLine("=================================================");
        Console.WriteLine($"Muestra original: {VentasReales.Count} ventas");
        Console.WriteLine($"Producto consultado: {ProductoConsultado}");
        Console.WriteLine($"Consultas repetidas por prueba: {Consultas:N0}");
        Console.WriteLine();

        var resumenReal = Lineal(VentasReales, ProductoConsultado);
        Console.WriteLine("RESUMEN DE LA MUESTRA REAL");
        Console.WriteLine($"Unidades vendidas: {resumenReal.Cantidad}");
        Console.WriteLine($"Importe vendido: Bs {resumenReal.Importe:F2}");
        Console.WriteLine();

        Console.WriteLine("COMPARACION DE LOS TRES CANDIDATOS");
        Console.WriteLine("Los tamaños superiores a 20 son datos de prueba ampliados,");
        Console.WriteLine("generados repitiendo la muestra; no son ventas reales adicionales.");
        Console.WriteLine();

        Console.WriteLine(
            $"{ "n",8} {"A lineal ms",14} {"B ordenar+bin ms",19} {"C diccionario ms",19}");
        Console.WriteLine(new string('-', 65));

        foreach (int n in new[] { 20, 60, 100, 500, 1000, 5000, 10000 })
        {
            var datos = CrearDatosPrueba(n);

            // Preparación de B: ordenar una sola vez.
            var relojOrden = Stopwatch.StartNew();
            var ordenados = datos.OrderBy(v => v.Producto, StringComparer.Ordinal).ToList();
            relojOrden.Stop();

            // Preparación de C: construir acumulados por producto.
            var relojDic = Stopwatch.StartNew();
            var diccionario = ConstruirDiccionario(datos);
            relojDic.Stop();

            // Calentamiento de los tres métodos.
            EjecutarLineal(datos);
            EjecutarBinaria(ordenados);
            EjecutarDiccionario(diccionario);

            double a = MedirPromedio(() => EjecutarLineal(datos));
            double bConsultas = MedirPromedio(() => EjecutarBinaria(ordenados));
            double cConsultas = MedirPromedio(() => EjecutarDiccionario(diccionario));

            // Para B y C se informa también la preparación, separada de las consultas.
            double bTotal = relojOrden.Elapsed.TotalMilliseconds + bConsultas;
            double cTotal = relojDic.Elapsed.TotalMilliseconds + cConsultas;

            Console.WriteLine($"{n,8:N0} {a,14:F4} {bTotal,19:F4} {cTotal,19:F4}");
        }

        Console.WriteLine();
        Console.WriteLine("COMPLEJIDAD Y ELECCION");
        Console.WriteLine("A - Lineal: O(n) por consulta; no necesita preparacion.");
        Console.WriteLine("B - Ordenar + binaria: O(n log n) al ordenar y O(log n + k) por consulta.");
        Console.WriteLine("C - Diccionario: O(n) al construir y O(1) promedio para consultar el resumen.");
        Console.WriteLine();
        Console.WriteLine("ELECCION PARA ESTE CASO: C, porque se busca consultar repetidamente");
        Console.WriteLine("el total de unidades y el importe acumulado de cada presentacion.");
        Console.WriteLine("Si se necesitan las ventas individuales o rangos de fechas, B puede ser");
        Console.WriteLine("mas adecuado. Para pocos registros, A sigue siendo simple y suficiente.");
        Console.WriteLine();
        Console.WriteLine("PROTOCOLO: 1 calentamiento + 3 mediciones y promedio.");
        Console.WriteLine("Los tiempos dependen del equipo; las complejidades describen el crecimiento.");
    }

    static List<Venta> CrearDatosPrueba(int n)
    {
        // Los primeros 20 registros son los datos del H1.
        // Para n > 20 se repite la muestra solo para probar escalabilidad.
        var resultado = new List<Venta>(n);
        for (int i = 0; i < n; i++)
            resultado.Add(VentasReales[i % VentasReales.Count]);
        return resultado;
    }

    // Candidato A: recorre todos los registros para acumular unidades e importe.
    static Resumen Lineal(List<Venta> ventas, string producto)
    {
        int cantidad = 0;
        decimal importe = 0;

        foreach (var venta in ventas)
        {
            if (venta.Producto == producto)
            {
                cantidad += venta.Cantidad;
                importe += venta.Total;
            }
        }

        return new Resumen(cantidad, importe);
    }

    // Candidato B: requiere ordenar por producto una sola vez.
    // Busca la primera posición del producto y recorre sus k coincidencias.
    static Resumen Binaria(List<Venta> ordenadas, string producto)
    {
        int inicio = 0, fin = ordenadas.Count;

        while (inicio < fin)
        {
            int medio = inicio + (fin - inicio) / 2;
            int cmp = string.Compare(
                ordenadas[medio].Producto, producto, StringComparison.Ordinal);

            if (cmp < 0) inicio = medio + 1;
            else fin = medio;
        }

        int cantidad = 0;
        decimal importe = 0;

        for (int i = inicio; i < ordenadas.Count &&
             ordenadas[i].Producto == producto; i++)
        {
            cantidad += ordenadas[i].Cantidad;
            importe += ordenadas[i].Total;
        }

        return new Resumen(cantidad, importe);
    }

    // Candidato C: prepara un diccionario con el resumen de cada producto.
    static Dictionary<string, Resumen> ConstruirDiccionario(List<Venta> ventas)
    {
        var diccionario = new Dictionary<string, Resumen>(StringComparer.Ordinal);

        foreach (var venta in ventas)
        {
            if (diccionario.TryGetValue(venta.Producto, out var actual))
            {
                diccionario[venta.Producto] = new Resumen(
                    actual.Cantidad + venta.Cantidad,
                    actual.Importe + venta.Total);
            }
            else
            {
                diccionario[venta.Producto] =
                    new Resumen(venta.Cantidad, venta.Total);
            }
        }

        return diccionario;
    }

    static Resumen ConsultarDiccionario(
        Dictionary<string, Resumen> diccionario, string producto)
    {
        return diccionario.TryGetValue(producto, out var resumen)
            ? resumen
            : new Resumen(0, 0m);
    }

    static long EjecutarLineal(List<Venta> datos)
    {
        long acumulado = 0;
        for (int i = 0; i < Consultas; i++)
            acumulado += Lineal(datos, ProductoConsultado).Cantidad;
        return acumulado;
    }

    static long EjecutarBinaria(List<Venta> datos)
    {
        long acumulado = 0;
        for (int i = 0; i < Consultas; i++)
            acumulado += Binaria(datos, ProductoConsultado).Cantidad;
        return acumulado;
    }

    static long EjecutarDiccionario(Dictionary<string, Resumen> datos)
    {
        long acumulado = 0;
        for (int i = 0; i < Consultas; i++)
            acumulado += ConsultarDiccionario(datos, ProductoConsultado).Cantidad;
        return acumulado;
    }

    static double MedirPromedio(Func<long> accion)
    {
        accion(); // calentamiento
        double suma = 0;

        for (int i = 0; i < 3; i++)
        {
            var reloj = Stopwatch.StartNew();
            long resultado = accion();
            reloj.Stop();
            GC.KeepAlive(resultado);
            suma += reloj.Elapsed.TotalMilliseconds;
        }

        return suma / 3.0;
    }
}
