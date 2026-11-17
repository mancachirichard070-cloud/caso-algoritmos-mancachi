Nombre completo: Richard Mancachi Tantani
P1.2 - PROGRAMAR Y MEDIR

1. Objetivo

Comparar el tiempo que tarda la función original `HayComprobantesRepetidosPorPares` con la función mejorada `HayComprobantesRepetidosCurado`. La función mejorada utiliza un `HashSet<int>` para detectar comprobantes repetidos sin cambiar el arreglo recibido.
 2. Pruebas de funcionamiento

Se probaron los cinco casos indicados en el enunciado:

| Caso de prueba | Resultado esperado | Original | Curada | ¿Modifica el arreglo? |
|---|---|---|---|---|
| Repetido en el medio | true | true | true | No |
| Sin repetidos | false | false | false | No |
| Lista vacía | false | false | false | No |
| Un solo elemento | false | false | false | No |
| Repetido en los extremos | true | true | true | No |

Resultado:Las dos funciones dieron la respuesta esperada en los cinco casos. También se comprobó que la función curada no modifica el arreglo original.

### 3. Cómo funciona la solución mejorada

La función curada recorre el arreglo una sola vez y utiliza un `HashSet<int>` para guardar los comprobantes que ya encontró. Si intenta agregar un número que ya estaba guardado, significa que hay un repetido y devuelve `true`. Si termina de revisar todos los números sin encontrar repetidos, devuelve `false`.

Su costo esperado es O(n) y utiliza espacio adicional O(n). La función original tiene costo O(n²) en el peor caso.

4. Procedimiento de medición

Las mediciones se realizan en Visual Studio Code desde la terminal, utilizando el modo Release con el comando:

```bash
dotnet run -c Release
```

Se generan arreglos sin comprobantes repetidos. Este es el peor caso para las dos funciones, porque deben revisar todos los elementos antes de devolver `false`.

Se utilizan cuatro tamaños que se duplican: 4.000, 8.000, 16.000 y 32.000 elementos Se eligió 4.000 como tamaño inicial para que el mayor arreglo pueda acercarse al intervalo de 0,5 a 20 segundos solicitado para la función original. El tiempo real debe comprobarse en el equipo donde se ejecuta el programa.

Para cada tamaño, el programa realiza una corrida de calentamiento para cada función y después toma tres mediciones de cada una. Finalmente calcula el promedio:

Promedio = (tiempo 1 + tiempo 2 + tiempo 3) / 3

### 5. Resultados de las mediciones

Después de ejecutar el código actualizado, copiar aquí los tiempos que aparecen en la terminal. Los tiempos se expresan en milisegundos.

| Tamaño | Promedio original (ms) | Veces respecto a la fila anterior | Promedio curada (ms) | Veces respecto a la fila anterior |
|---:|---:|---:|---:|---:|
| 4.000 | [copiar] | — | [copiar] | — |
| 8.000 | [copiar] | [copiar] | [copiar] | [copiar] |
| 16.000 | [copiar] | [copiar] | [copiar] | [copiar] |
| 32.000 | [copiar] | [copiar] | [copiar] | [copiar] |

El factor de crecimiento se calcula dividiendo el promedio de una fila entre el promedio de la fila anterior. Por ejemplo: promedio con 8.000 elementos / promedio con 4.000 elementos.

