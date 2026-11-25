Nombre: Richard Mancachi Tantani

# EVALUACION INTEGRADORA - VARIANTE B
# I1 - ANALISIS Y ELECCION

## 1. Costo de la solucion ingenua

La clase CompradorIngenuo prueba todos los subconjuntos posibles de utiles.

Cada util tiene dos posibilidades: comprarlo o no comprarlo. Por lo tanto, con n utiles existen 2^n combinaciones.

Para cada combinacion se recorren hasta n utiles para calcular el costo y la utilidad.

La complejidad temporal de la implementacion es O(n * 2^n).

La memoria adicional para almacenar los utiles elegidos es O(n).

## 2. Analisis de la recurrencia

La version recursiva cumple:

T(n) = 2T(n-1) + 1

Desarrollando:

T(n) = 2T(n-1) + 1
T(n) = 4T(n-2) + 3
T(n) = 8T(n-3) + 7

Su crecimiento es Theta(2^n).

El teorema maestro de bolsillo no se aplica directamente porque esta recurrencia reduce el problema de n a n-1, en lugar de dividirlo entre un factor b.

Es importante distinguir que la recurrencia recursiva tiene costo Theta(2^n), mientras que la implementacion ingenua entregada recorre n elementos por cada subconjunto y tiene costo O(n * 2^n).

## 3. Contraejemplo del algoritmo voraz

Primer caso de prueba:

| Util  | Costo Bs  | Utilidad | Utilidad por Bs    |
|---    |---:       |---:      |---:                |
| U1    | 20        | 120      | 6                  |
| U2    | 40        | 200      | 5                  |
| U3    | 60        | 240      | 4                  |

Presupuesto disponible: Bs 100.

El algoritmo voraz ordena por utilidad por boliviano y selecciona primero U1 y despues U2.

Costo: 20 + 40 = Bs 60.
Utilidad: 120 + 200 = Bs 320.

Ya no puede incorporar U3 porque el presupuesto restante es Bs 40.

Sin embargo, seleccionar U2 y U3 produce:

Costo: 40 + 60 = Bs 100.
Utilidad: 200 + 240 = Bs 440.

Por tanto, el algoritmo voraz obtiene Bs 320, mientras que la solucion optima obtiene Bs 440.

Esto demuestra que una decision local no garantiza la mejor solucion global.

## 4. Estrategia elegida: programacion dinamica por utilidad

Se utiliza programacion dinamica para resolver el problema de la mochila 0/1.

Se define U como la suma de las utilidades de todos los utiles.

El estado DP[u] representa el costo minimo necesario para conseguir exactamente una utilidad acumulada u.

Para cada util se consideran dos posibilidades:

- No comprarlo: conservar el costo minimo anterior.
- Comprarlo: sumar su costo al costo minimo necesario para alcanzar la utilidad restante.

La recurrencia es:

DP[i,u] = min(DP[i-1,u], costo[i] + DP[i-1,u-utilidad[i]])

La segunda alternativa solamente se considera cuando la utilidad restante es alcanzable.

Los resultados se actualizan en orden descendente para evitar utilizar un mismo util mas de una vez.

Se reutilizan resultados de subproblemas que representan diferentes combinaciones para alcanzar una misma utilidad.

La complejidad temporal es O(n * U), donde n es la cantidad de utiles y U es la suma de sus utilidades.

Para reconstruir los utiles seleccionados se conserva informacion de las decisiones, utilizando O(n * U) bits adicionales, ademas del arreglo de costos O(U).

Esta estrategia permite trabajar con presupuestos grandes sin crear una tabla cuyo tamano dependa directamente del presupuesto.

## 5. Estructura para EstaEnLaCompra

Para responder miles de consultas se utiliza HashSet<string>.

Despues de determinar la compra, se guardan los codigos seleccionados en un conjunto.

La consulta de pertenencia tiene costo promedio O(1).

En cambio, recorrer una lista para encontrar un codigo puede costar O(n).

## 6. Presupuesto de Bs 1.000.000.000

La estrategia elegida utiliza la suma de utilidades como dimension de la programacion dinamica, no el presupuesto.

Por eso, aumentar el presupuesto a Bs 1.000.000.000 no aumenta directamente el tamano de la tabla.

Ademas, si el presupuesto alcanza para comprar todos los utiles, se puede seleccionar directamente todos los utiles con utilidad positiva.

Sin embargo, si aumenta demasiado la suma de utilidades, la programacion dinamica tambien puede requerir mucha memoria y tiempo, porque su costo depende de U.

## 7. Conclusion

La solución ingenua se vuelve cada vez más lenta porque revisa todas las posibilidades cuando aumenta la cantidad de productos. El método voraz es más rápido, pero no siempre encuentra la mejor solución, como se pudo observar en la primera prueba. En cambio, la programación dinámica aprovecha los resultados que ya calculó para resolver el problema de forma más eficiente. Además, para buscar productos repetidos se utiliza HashSet, que permite encontrarlos rápidamente sin tener que revisar toda la lista.