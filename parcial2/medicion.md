Nombre: Richard Mancachi Tantani

# P2.3 — Medición de rendimiento

## 1. Protocolo utilizado

Se comparó el GestorIngenuo con el GestorMejorado utilizando tres tamaños de datos: 10.000, 20.000 y 40.000 pacientes.

Para cada tamaño se realizó una corrida de calentamiento y tres corridas medidas. Después se calculó el promedio de los tres tiempos obtenidos.

Las mediciones se realizaron en C# utilizando Stopwatch y ejecutando el programa en configuración Release.

## 2. Resultados obtenidos

### GestorIngenuo

| Pacientes | Corrida 1 (ms) | Corrida 2 (ms) | Corrida 3 (ms) | Promedio (ms) | Crecimiento |
______________________________________________________________________________________________
| 10.000    | 175,85         | 136,59         | 123,35         | 145,26        | —           |
| 20.000    | 566,41         | 480,73         | 473,01         | 506,71        | 3,49x       |
| 40.000    | 1838,54        | 2340,29        | 2325,85        | 2168,23       | 4,28x       |

### GestorMejorado

| Pacientes | Corrida 1 (ms) | Corrida 2 (ms) | Corrida 3 (ms) | Promedio (ms) | Crecimiento |
______________________________________________________________________________________________
| 10.000    | 7,97           | 13,46          | 14,09          | 11,84         | —           |
| 20.000    | 14,32          | 6,06           | 26,04          | 15,47         | 1,31x       |
| 40.000    | 49,78          | 34,19          | 11,79          | 31,92         | 2,06x       |

## 3. Conclusión

La solución más sencilla fue más lenta porque tenía que revisar uno por uno a los pacientes hasta encontrar al que debía atender. Por eso, mientras más pacientes había, más tiempo tardaba en funcionar.

En cambio, la solución mejorada fue mucho más rápida porque utilizaba una cola de prioridad y un diccionario, que permiten encontrar y organizar a los pacientes de manera más eficiente.

También hubo pequeñas diferencias en los tiempos de algunas pruebas, especialmente cuando se trabajó con 20.000 y 40.000 pacientes. Esto pudo deberse a que la computadora estaba realizando otras tareas al mismo tiempo.

A pesar de esas diferencias, los resultados demostraron que la solución mejorada funciona mucho más rápido que la solución sencilla, especialmente cuando aumenta la cantidad de pacientes.