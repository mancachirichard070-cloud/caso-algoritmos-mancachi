Nombre: Richard Mancachi Tantani

# H2 - Medición de rendimiento

## 1. Objetivo

Comparar la búsqueda de una venta por su código único utilizando dos versiones del mismo problema:

- Versión ingenua: utiliza una List y recorre los registros hasta encontrar el código.
- Versión elegida: utiliza un Dictionary para localizar directamente la venta mediante su código.

## 2. Protocolo

Las pruebas se realizaron en C# con .NET 10 y configuración Release.

Se utilizaron cuatro tamaños: 2.000, 4.000, 8.000 y 16.000 registros.

Para cada tamaño se realizó una corrida de calentamiento y tres corridas medidas. Se calculó el promedio de las tres mediciones.

Se utilizaron 3.000 consultas del código correspondiente al último registro, para probar el peor caso de la búsqueda lineal. Los registros adicionales son simulados.

## 3. Resultados

| n      | Ingenua (ms) | Veces más | Elegida (ms) | Veces más |
----------------------------------------------------------- 
| 2.000  | 22.117       | —         | 0.185        | —         |
| 4.000  | 18.127       | 0.82x     | 0.047        | 0.25x     |
| 8.000  | 32.528       | 1.79x     | 0.043        | 0.92x     |
| 16.000 | 49.134       | 1.51x     | 0.042        | 0.99x     |

## 4. Conclusión

La búsqueda ingenua tiende a aumentar su tiempo cuando crece la cantidad de registros, lo que corresponde a su costo O(n). En cambio, Dictionary mantiene tiempos mucho menores y casi constantes, como se espera de una tabla hash con costo promedio O(1).

Los resultados no aumentaron de manera regular en todas las filas. Esto puede deberse al ruido de medición, al procesador y a otros procesos de la computadora. Por eso se realizaron tres corridas y se utilizó el promedio.
