Nombre completo: Richard Mancachi Tantani

# PARCIAL 1 - VARIANTE B
P1.3 - ELEGIR

1. Datos del problema

El almacén tiene 250.000 códigos de productos y recibe 8.000 consultas para saber si un código está registrado.

Se utilizarán:
- N = 250.000 códigos.
- Q = 8.000 consultas.
- log₂(250.000) ≈ 17,93.

Los cálculos son aproximados y consideran el peor caso para la búsqueda lineal.

2. Comparación de los tres métodos

Opción 1. Recorrer toda la lista en cada consulta

En cada consulta se podría revisar la lista completa. En el peor caso se realizan N comparaciones por consulta.

Costo total:

Q × N = 8.000 × 250.000

Costo aproximado: 2.000.000.000 comparaciones.

Opción 2. Ordenar una vez y utilizar búsqueda binaria

Primero se ordena el listado una sola vez con un algoritmo de costo O(n log n). Como la memoria es limitada y no se puede duplicar el listado, se puede utilizar un algoritmo de ordenamiento eficiente que trabaje sobre el mismo arreglo, como Heapsort.

Después se realizan las 8.000 consultas mediante búsqueda binaria. Cada consulta necesita aproximadamente log₂(N) comparaciones.

Costo aproximado:

Ordenamiento: 250.000 × 17,93 ≈ 4.482.892

Consultas: 8.000 × 17,93 ≈ 143.453

Costo total: 4.482.892 + 143.453

Costo aproximado: 4.626.345 operaciones.
Opción 3. Ordenar con un algoritmo cuadrático y utilizar búsqueda binaria

Para estimar esta opción se toma como ejemplo Selection Sort. Este algoritmo realiza aproximadamente N(N-1)/2 comparaciones para ordenar.

Ordenamiento:

250.000 × 249.999 / 2 = 31.249.875.000 comparaciones.

Consultas:

8.000 × 17,93 ≈ 143.453 comparaciones.

Costo total aproximado:

31.249.875.000 + 143.453

Costo aproximado: 31.250.018.453 operaciones.

3. Resumen de costos

| Método | Costo aproximado |
|---|---:|
| 1. Búsqueda lineal en cada consulta | 2.000.000.000 |
| 2. Heapsort + búsqueda binaria | 4.626.345 |
| 3. Selection Sort + búsqueda binaria | 31.250.018.453 |

Los valores son aproximaciones basadas en el orden de crecimiento y en los conteos indicados. El número exacto de operaciones depende de la implementación del algoritmo.

4. Elección

La opción elegida es la **opción 2: ordenar una vez con un algoritmo O(n log n) y después utilizar búsqueda binaria**.

Es la alternativa más conveniente porque el listado se ordena una sola vez al inicio del día y las consultas posteriores se resuelven rápidamente. Además, se puede utilizar Heapsort para ordenar el mismo arreglo sin necesitar una segunda copia completa del listado.

5. ¿Desde cuántas consultas cambiaría la decisión?

Para comparar la búsqueda lineal con ordenar una vez y realizar búsquedas binarias, se igualan sus costos aproximados:

Q × N = N log₂(N) + Q log₂(N)

Despejando Q:

Q = [N log₂(N)] / [N - log₂(N)]

Reemplazando N = 250.000:

Q ≈ [250.000 × 17,93] / [250.000 - 17,93]

Q ≈ 17,94

Por lo tanto, a partir de aproximadamente **18 consultas** conviene ordenar primero y luego utilizar búsqueda binaria, según este modelo aproximado. Con menos consultas podría convenir recorrer la lista directamente, porque se evita el costo inicial de ordenar.


