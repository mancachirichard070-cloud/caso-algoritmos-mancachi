Nombre: Richard Mancachi Tantani

# H2 - Inventario de operaciones

## 1. Datos del caso

El caso corresponde al registro de ventas y control de inventario de detergentes UNO, en dos presentaciones:

- UNO de 620 g: Bs 12 por unidad.
- UNO de 4,7 kg: Bs 90 por unidad.

Cada venta contiene un código único, fecha, producto, cantidad, precio unitario e importe total.

Se cuenta con 20 registros reales de ventas del 20 al 29 de octubre de 2026. El registro y la revisión del inventario requieren aproximadamente 30 minutos diarios.

Para las pruebas se utilizarán registros simulados, creados a partir de la muestra original y asignando códigos únicos. No representan ventas reales adicionales.

## 2. Operaciones del proceso

| Operación | Aplicación |
|---|---|
| Buscar por clave | Localizar una venta mediante su código único. |
| Insertar | Registrar una nueva venta y actualizar el inventario. |
| Borrar | Eliminar o anular una venta ingresada por error. |
| Recorrer en orden | Revisar las ventas por fecha para elaborar reportes. |
| Buscar el menor | Identificar la venta con menor importe. |
| Buscar el mayor | Identificar la venta con mayor importe. |
| Buscar por rango | Consultar ventas entre dos fechas. |

## 3. Operaciones más frecuentes

1. Insertar: se realiza cada vez que se registra una venta. En la muestra hay 20 ventas en 10 días.
2. Buscar por clave: permite localizar una venta y comprobar sus datos.
3. Recorrer en orden: permite revisar los movimientos y preparar reportes.

No existe un conteo exacto de las búsquedas diarias, por lo que no se asignan cantidades reales inventadas.

Las operaciones de borrar, buscar el menor, buscar el mayor y consultar por rango se realizan con menor frecuencia.

## 4. Datos para las pruebas

Los primeros 20 registros corresponden a las ventas reales. Los registros adicionales serán simulados, repitiendo la muestra y asignando un código único a cada venta.

Se utilizarán cuatro tamaños de datos: 2.000, 4.000, 8.000 y 16.000 registros.

## 5. Operación que se comparará

Se comparará la búsqueda de una venta mediante su código único.

La versión ingenua recorrerá una lista hasta encontrar el código. La versión mejorada utilizará un Dictionary para localizar la venta mediante su clave.
