Nombre: Richard Mancachi Tantani

# H2 - Estructura de datos elegida

## 1. Estructuras seleccionadas

Para comparar la búsqueda de ventas por código único se utilizarán dos estructuras.

La versión ingenua utiliza List y recorre sus elementos hasta encontrar el código. La versión elegida utiliza Dictionary, que funciona mediante una tabla hash y permite localizar una venta directamente por su clave.

## 2. Tabla de operaciones y costos

| Operación | Cuántas veces se hace | Costo O |
|---|---|---|
| Insertar | 20 registros en la muestra de 10 días | O(1) amortizado en List y O(1) promedio en Dictionary |
| Buscar por clave | Consultas al historial; cantidad diaria no registrada | O(n) en List y O(1) promedio en Dictionary |
| Recorrer en orden | Al revisar el historial | O(n) en List |
| Borrar | Ocasionalmente | O(n) en List; O(1) promedio para localizar por clave en Dictionary |
| Buscar el menor | En consultas específicas | O(n) |
| Buscar el mayor | En consultas específicas | O(n) |
| Buscar por rango | Cuando se consulta un periodo | O(n) recorriendo una lista |

## 3. Justificación

La List es sencilla, pero cuando aumenta la cantidad de ventas necesita revisar más registros para encontrar un código. Por eso su búsqueda tiene costo O(n).

En cambio, Dictionary utiliza una tabla hash y permite encontrar una venta por su código con costo O(1) promedio. Esto evita recorrer todos los registros en cada consulta.

La comparación del programa se concentra en buscar una venta por código, utilizando los mismos datos y consultas en ambas versiones. Para revisar el historial completo también es importante conservar la posibilidad de recorrer los registros y preparar reportes.
