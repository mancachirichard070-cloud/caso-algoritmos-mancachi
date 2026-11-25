Nombre: Richard Mancachi Tantani

# EVALUACION INTEGRADORA - VARIANTE B
# I3 - MEDICION DE RENDIMIENTO

## 1. Protocolo de medicion

Las pruebas se realizaron en C# utilizando .NET 10 y configuracion Release.

Se compararon CompradorIngenuo y CompradorMejorado mediante el metodo EjecutarCarga.

Se utilizaron cuatro tamanos: 20, 22, 24 y 26 utiles.

Para cada tamano se realizo:
- Una corrida de calentamiento.
- Tres corridas medidas.
- Calculo del promedio de los tres tiempos.

Se verifico que ambas soluciones obtuvieran la misma utilidad en cada tamano.

## 2. Resultados de las mediciones

| n  | Ingenua promedio (ms) | Veces mas | Mejorada promedio (ms) | Veces mas | Misma utilidad  |
|---:|---:                   |---:       |---:                    |---:       |---              |
| 20 | 49.150                | -         | 0.055                  | -         | Si              |
| 22 | 225.989               | 4.60x     | 0.060                  | 1.09x     | Si              |
| 24 | 794.732               | 3.52x     | 0.061                  | 1.02x     | Si              |
| 26 | 3294.086              | 4.14x     | 0.060                  | 0.97x     | Si              |

## 3. Detalle de las corridas

### 20 utiles

Ingenua:
- Corrida 1: 51.623 ms
- Corrida 2: 49.697 ms
- Corrida 3: 46.132 ms
- Promedio: 49.150 ms

Mejorada:
- Corrida 1: 0.072 ms
- Corrida 2: 0.046 ms
- Corrida 3: 0.047 ms
- Promedio: 0.055 ms

Utilidad obtenida por ambas: 517.

### 22 utiles

Ingenua:
- Corrida 1: 231.305 ms
- Corrida 2: 235.308 ms
- Corrida 3: 211.354 ms
- Promedio: 225.989 ms

Mejorada:
- Corrida 1: 0.063 ms
- Corrida 2: 0.069 ms
- Corrida 3: 0.049 ms
- Promedio: 0.060 ms

Utilidad obtenida por ambas: 634.

### 24 utiles

Ingenua:
- Corrida 1: 797.448 ms
- Corrida 2: 793.257 ms
- Corrida 3: 793.491 ms
- Promedio: 794.732 ms

Mejorada:
- Corrida 1: 0.066 ms
- Corrida 2: 0.058 ms
- Corrida 3: 0.060 ms
- Promedio: 0.061 ms

Utilidad obtenida por ambas: 705.

### 26 utiles

Ingenua:
- Corrida 1: 3221.062 ms
- Corrida 2: 3279.419 ms
- Corrida 3: 3381.778 ms
- Promedio: 3294.086 ms

Mejorada:
- Corrida 1: 0.063 ms
- Corrida 2: 0.059 ms
- Corrida 3: 0.057 ms
- Promedio: 0.060 ms

Utilidad obtenida por ambas: 843.

## 4. Interpretacion de los resultados

La solucion ingenua incremento considerablemente su tiempo cuando aumentaron los utiles. Los factores de crecimiento fueron 4.60x, 3.52x y 4.14x al agregar dos utiles en cada prueba.

Este comportamiento se aproxima al crecimiento exponencial esperado de la enumeracion de combinaciones.

La solucion mejorada mantuvo tiempos cercanos a 0.06 ms. Sus factores de crecimiento fueron 1.09x, 1.02x y 0.97x.

Los resultados de ambas soluciones coincidieron en los cuatro tamanos.

## 5. Prueba adicional: 1.000 utiles y presupuesto Bs 10.000

Se realizo una prueba con 1.000 utiles generados con semilla fija y presupuesto de Bs 10.000.

Se utilizo una corrida de calentamiento y tres corridas medidas.

Resultados:
- Corrida 1: 61.343 ms.
- Corrida 2: 59.969 ms.
- Corrida 3: 60.339 ms.
- Promedio: 60.550 ms.
- Utilidad obtenida: 36.569.

La solucion mejorada completo el calculo sin recurrir a la enumeracion de todas las combinaciones.

## 6. Presupuesto de Bs 1.000.000.000

Se ejecuto una prueba adicional utilizando los mismos 1.000 utiles y un presupuesto de Bs 1.000.000.000.

Resultados:
- Utilidad total: 54.926.
- Utiles seleccionados: 1.000.

Como el presupuesto supera el costo total de los utiles, la implementacion utiliza una comprobacion previa y selecciona directamente todos los utiles con utilidad positiva.

Por esta razon no necesita construir la tabla de programacion dinamica en este caso.

En general, la estrategia elegida depende de la suma de utilidades U y tiene costo O(n * U). Si U aumenta considerablemente, tambien pueden aumentar el tiempo y la memoria necesarios, aunque el presupuesto sea grande.

## 7. Conclusion

En las pruebas se pudo ver que la solución sencilla tarda cada vez más porque revisa todas las posibles combinaciones. En cambio, la solución mejorada utiliza programación dinámica, lo que le permite encontrar los resultados en mucho menos tiempo. Ambas soluciones obtuvieron el mismo resultado en las cuatro pruebas realizadas. También hubo pequeñas diferencias en los tiempos de cada prueba debido al rendimiento de la computadora y a otros programas que estaban funcionando. Por eso, se realizaron varias pruebas y se calculó un promedio para obtener resultados más confiables.