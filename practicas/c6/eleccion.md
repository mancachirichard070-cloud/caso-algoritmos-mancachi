C6: Elección de algoritmo para el registro de ventas e inventario de detergentes
Mi proceso y sus datos

El proceso que deseo optimizar es la consulta de ventas y el control de inventario de detergentes UNO. Cuento con 20 registros del 20 al 29 de octubre de 2026, con dos presentaciones: 620 g (Bs 12) y 4,7 kg (Bs 90). El registro y revisión requieren aproximadamente 30 minutos diarios y afectan al propietario y al encargado del negocio. Se estiman 60 registros mensuales y se realizarán pruebas con 100, 500, 1.000, 5.000 y 10.000 registros, generados a partir de la muestra.

Justificación: Optimizar este proceso permitirá reducir el tiempo de trabajo y mejorar el control de las ventas y existencias.

La operación que más pesa

La operación principal es consultar las ventas de una presentación para conocer las unidades vendidas y el importe acumulado. Actualmente se revisan todos los registros y se realizan sumas manuales, lo que aumenta el trabajo cuando crece la información.

Justificación: Automatizar esta consulta facilitará obtener resultados rápidos y evitará revisiones repetitivas.

Dos candidatos y una alternativa adicional

Candidato A: búsqueda lineal. Revisa todos los registros y suma las cantidades e importes del producto solicitado. Su complejidad es O(n). Es sencilla de implementar y no necesita ordenar los datos.

Candidato B: ordenar y buscar con binaria. Ordena los registros con un costo O(n log n) y utiliza búsqueda binaria para localizar el producto, con un costo de O(log n + k). Es útil para consultas con datos ordenados.

Candidato C: diccionario de acumulados (Dictionary). Recorre una vez las ventas y almacena las cantidades e importes totales por producto. Su preparación cuesta O(n) y cada consulta tiene un costo promedio O(1).

Justificación: Comparar estas alternativas permite identificar cuál ofrece mayor eficiencia según la cantidad de registros y el tipo de consulta.
Registros (n)	A: lineal, peor caso	B: búsqueda binaria (aprox.)	C: diccionario
20	20	5	O(1)
60	60	6	O(1)
100	100	7	O(1)
500	500	9	O(1)
1.000	1.000	10	O(1)
5.000	5.000	13	O(1)
10.000	10.000	14	O(1)

La tabla muestra una comparación teórica, no tiempos medidos. En el método B también se debe recorrer las ventas coincidentes para acumular sus importes.

Mi elección y por qué

Elijo el Candidato C: Dictionary, porque permite consultar rápidamente las unidades vendidas y el importe acumulado de cada presentación, sin recorrer nuevamente todos los registros. Su preparación es O(n) y sus consultas tienen un costo promedio O(1).

Los otros métodos también pueden ser útiles: la búsqueda lineal para pocos registros y la búsqueda binaria cuando se necesita mantener los datos ordenados o consultar rangos.

Justificación: Dictionary es la alternativa más adecuada para este negocio porque las consultas de totales por producto pueden repetirse y se busca reducir el tiempo de procesamiento.


Implementaré los tres métodos en C# utilizando los 20 registros reales del H1. Consultaré la presentación UNO de 620 g y compararé las cantidades e importes obtenidos, verificando que los tres métodos produzcan los mismos resultados.

Después realizaré pruebas con 60, 100, 500, 1.000, 5.000 y 10.000 registros generados a partir de la muestra. Ejecutaré 1.000 consultas por tamaño, realizando una corrida de calentamiento y tres corridas medidas para calcular el promedio.

También mediré el tiempo de ordenamiento del método B y la construcción del diccionario del método C.

Justificación: Estas pruebas permitirán comprobar el rendimiento de cada algoritmo y determinar cuál responde mejor cuando aumenta la cantidad de información.

