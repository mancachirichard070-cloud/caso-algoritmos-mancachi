Nombre: Richard Mancachi Tantani

# H3 - Medicion y comparacion de algoritmos

## 1. Objetivo

Comparar el rendimiento de tres algoritmos que resuelven el problema de la mochila 0/1 utilizando productos de detergentes UNO: voraz, fuerza bruta y programacion dinamica.

El objetivo es observar como cambia el tiempo de ejecucion cuando aumenta la cantidad de objetos y comprobar las diferencias entre sus complejidades.

## 2. Protocolo de medicion

Las pruebas se realizaron en C# con .NET 10 y configuracion Release.

Se utilizaron cuatro cantidades de objetos: 16, 18, 20 y 22. La capacidad de la mochila se mantuvo en 4.700 gramos.

Para cada algoritmo y tamaño se realizo una corrida de calentamiento y tres corridas medidas. Se calculo el promedio de las tres mediciones.

Para mejorar la resolucion de los tiempos, el programa repitio 1.000 veces cada ejecucion voraz, una vez fuerza bruta y 20 veces programacion dinamica. Los tiempos se dividieron entre las repeticiones para obtener el promedio por ejecucion.

## 3. Resultados de las mediciones

| Objetos | Algoritmo | Corrida 1 (ms) | Corrida 2 (ms) | Corrida 3 (ms) | Promedio (ms) |
|---:|---|---:|---:|---:|---:|
| 16 | Voraz | 0.003 | 0.002 | 0.002 | 0.002 |
| 16 | Fuerza bruta | 2.277 | 2.607 | 2.318 | 2.401 |
| 16 | Programacion dinamica | 0.995 | 1.613 | 0.950 | 1.186 |
| 18 | Voraz | 0.002 | 0.003 | 0.003 | 0.003 |
| 18 | Fuerza bruta | 10.142 | 16.033 | 18.388 | 14.854 |
| 18 | Programacion dinamica | 0.535 | 0.527 | 0.507 | 0.523 |
| 20 | Voraz | 0.003 | 0.003 | 0.003 | 0.003 |
| 20 | Fuerza bruta | 44.840 | 40.891 | 38.245 | 41.325 |
| 20 | Programacion dinamica | 0.550 | 0.609 | 0.489 | 0.550 |
| 22 | Voraz | 0.005 | 0.006 | 0.015 | 0.008 |
| 22 | Fuerza bruta | 153.897 | 138.290 | 164.412 | 152.200 |
| 22 | Programacion dinamica | 0.485 | 0.446 | 0.432 | 0.454 |

## 4. Crecimiento entre tamaños

| Objetos | Voraz: veces mas | Fuerza bruta: veces mas | Dinamica: veces mas |
|---:|---:|---:|---:|
| 16 | - | - | - |
| 18 | 1.27x | 6.19x | 0.44x |
| 20 | 1.11x | 2.78x | 1.05x |
| 22 | 2.69x | 3.68x | 0.83x |

## 5. Comparacion de resultados

En la prueba inicial con ocho objetos y una capacidad de 4.700 gramos se obtuvieron los siguientes resultados:

| Algoritmo | Valor obtenido |
|---|---:|
| Voraz | Bs 84 |
| Fuerza bruta | Bs 90 |
| Programacion dinamica | Bs 90 |

La fuerza bruta y la programacion dinamica encontraron el mismo valor optimo. El algoritmo voraz obtuvo Bs 6 menos porque selecciono primero los productos pequenos y no pudo incorporar el producto grande.

## 6. Conclusion

El algoritmo voraz presento tiempos muy bajos, pero no encontro la mejor solucion en el ejemplo. La fuerza bruta aumento considerablemente su tiempo al crecer la cantidad de objetos, lo que se relaciona con su complejidad exponencial O(n x 2^n). La programacion dinamica encontro el mismo resultado optimo que la fuerza bruta utilizando una tabla, con complejidad O(n x W). Las variaciones entre corridas y los tiempos pequenos muestran que existe ruido de medicion, por lo que se utilizaron tres mediciones y sus promedios.
