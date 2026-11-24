Nombre: Richard Mancachi Tantani

# H3 - Algoritmos voraces y programacion dinamica

## 1. Descripcion del problema

Mi caso de estudio es el registro de ventas e inventario de detergentes UNO, que comercializa dos presentaciones: UNO de 620 g a Bs 12 y UNO de 4,7 kg a Bs 90.

En el negocio se registran ventas y se controla la cantidad disponible de cada producto. Como no existe una decision documentada de transporte con capacidad limitada, se utilizara un problema analogo basado en los productos del negocio.

El problema consiste en seleccionar productos para una mochila con capacidad maxima de 4.700 gramos, buscando obtener el mayor valor de venta posible sin superar dicha capacidad.

Para este ejercicio se utilizaran cantidades simuladas:
- 7 unidades de UNO de 620 g.
- 1 unidad de UNO de 4,7 kg.
- Capacidad maxima: 4.700 gramos.

Cada producto es indivisible: se selecciona completo o se deja fuera.

## 2. Objetivo

Encontrar la combinacion de productos que permita obtener el mayor valor de venta sin superar la capacidad de la mochila.

Se compararan tres metodos:
- Algoritmo voraz (Greedy).
- Fuerza bruta.
- Programacion dinamica.

## 3. Solucion mediante algoritmo voraz

El algoritmo voraz selecciona primero el producto que ofrece mayor valor por unidad de peso.

Producto UNO de 620 g:

12 / 620 = 0,01935 Bs por gramo.

Producto UNO de 4,7 kg:

90 / 4700 = 0,01915 Bs por gramo.

El algoritmo selecciona primero las unidades pequenas porque tienen una relacion de valor por peso ligeramente mayor.

Selecciona las 7 unidades pequenas:

Peso total = 7 x 620 = 4.340 gramos.

Valor total = 7 x 12 = Bs 84.

La unidad grande ya no entra porque supera la capacidad restante.

Resultado voraz: Bs 84.

## 4. Solucion mediante fuerza bruta

La fuerza bruta consiste en probar todas las combinaciones posibles y comprobar cuales cumplen la capacidad maxima.

Existen 8 productos disponibles, por lo que se pueden evaluar:

2^8 = 256 combinaciones.

Para cada combinacion se calcula el peso y el valor total. Se descartan las que superan los 4.700 gramos y se conserva la de mayor valor.

## 5. Solucion mediante programacion dinamica

La programacion dinamica divide el problema en subproblemas y guarda los resultados para evitar repetir calculos.

Se utiliza una tabla donde las filas representan los productos y las columnas representan las capacidades disponibles.

Para cada producto existen dos posibilidades:
- No seleccionar el producto.
- Seleccionarlo si su peso no supera la capacidad restante.

La relacion de recurrencia es:

DP[i,c] = max(DP[i-1,c], valor[i] + DP[i-1,c-peso[i]])

La segunda alternativa se utiliza solamente cuando peso[i] <= c.

La programacion dinamica encuentra que seleccionar una unidad de UNO de 4,7 kg permite obtener Bs 90, utilizando toda la capacidad.

## 6. Comparacion de resultados

| Metodo                | Combinacion           | Peso    | Valor |
|---                    |---                    |---:     |---:   |
| Voraz                 | 7 unidades pequenas   | 4.340 g | Bs 84 |
| Fuerza bruta          | 1 unidad grande       | 4.700 g | Bs 90 |
| Programacion dinamica | 1 unidad grande       | 4.700 g | Bs 90 |

## 7. Identificacion del problema dinamico

Subproblemas repetidos: al evaluar las combinaciones se presentan varias veces problemas relacionados con una cantidad de productos y una capacidad restante.

Subestructura optima: la solucion optima de un problema puede construirse utilizando soluciones optimas de problemas mas pequenos.

## 8. Complejidad de los algoritmos

| Algoritmo             | Complejidad temporal                     |
|---                    |---                                       |
| Voraz                 | O(n log n), considerando el ordenamiento |
| Fuerza bruta          | O(n x 2^n)                               |
| Programacion dinamica | O(n x W)                                 |

Donde n representa la cantidad de productos y W representa la capacidad de la mochila en unidades enteras de peso.

## 9. Conclusion

El algoritmo voraz es sencillo y rapido, pero en este ejemplo obtiene Bs 84 y no alcanza el mejor resultado. La fuerza bruta y la programacion dinamica encuentran la combinacion optima de Bs 90. La programacion dinamica aprovecha los resultados de subproblemas anteriores y evita repetir calculos. Su uso resulta conveniente cuando aumenta la cantidad de productos y la capacidad puede manejarse mediante una tabla.
'@ | Set-Content .\hitos\h3\analisis.md -Encoding utf8
