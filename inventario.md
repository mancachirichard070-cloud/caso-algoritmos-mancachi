# H1 – Inventario de operaciones

## Registro de ventas e inventario de detergentes UNO (620 g y 4,7 kg)

### 1. Datos que guarda el proceso y cantidad

El proceso registra las ventas de dos presentaciones de detergente UNO:

- **UNO de 620 g:** Bs 12 por unidad.
- **UNO de 4,7 kg:** Bs 90 por unidad.

Cada registro de venta contiene los siguientes datos:

| Dato | Descripción |
|---|---|
| Fecha | Día en que se realizó la venta. |
| Producto | Presentación del detergente vendido. |
| Cantidad | Número de unidades vendidas. |
| Precio unitario | Precio correspondiente a la presentación. |
| Total | Resultado de multiplicar la cantidad por el precio unitario. |

**Cantidad de información disponible:** se cuenta con una muestra de **20 registros de venta**, correspondientes al periodo de trabajo del 20 al 22 de octubre de 2026. El registro diario actual toma aproximadamente **30 minutos**.

El peor volumen mensual todavía debe determinarse con los registros reales de un mes de mayor movimiento. Para evaluar el rendimiento, se puede realizar una prueba adicional con un conjunto de datos más grande, sin confundir esos datos de prueba con ventas reales.

### 2. Operaciones que realiza el proceso

| Operación | Aplicación en el caso |
|---|---|
| Buscar por clave | Localizar una venta mediante un identificador único. Si todavía no existe un identificador, se recomienda asignarlo a cada venta. |
| Insertar | Registrar una nueva venta y actualizar la cantidad disponible del producto. |
| Borrar | Eliminar o anular un registro ingresado por error, conservando el control del inventario. |
| Recorrer en orden | Revisar las ventas ordenadas por fecha para elaborar reportes. |
| Buscar el menor | Identificar la venta de menor importe total. |
| Buscar el mayor | Identificar la venta de mayor importe total. |
| Buscar por rango | Consultar las ventas realizadas entre dos fechas determinadas. |

### 3. Operaciones más frecuentes

Las operaciones que se realizan con mayor frecuencia son:

1. **Insertar:** se registra cada nueva venta.
2. **Buscar:** se consulta la información de ventas y se verifica el inventario de cada presentación.
3. **Recorrer:** se revisan los movimientos para calcular las cantidades vendidas y preparar reportes.

Las operaciones de borrar, buscar el menor y buscar el mayor son menos frecuentes, porque se utilizan principalmente para corregir registros o elaborar consultas específicas.

### 4. Estructura de datos: cuál resuelve cada operación y cuál sufre

| Operación | Estructura conveniente | ¿Por qué? | Estructura que puede sufrir |
|---|---|---|---|
| Buscar por clave | Tabla hash (`Dictionary`) | Busca por clave en tiempo promedio **O(1)**. | Lista o arreglo sin ordenar: búsqueda **O(n)**. |
| Insertar | Arreglo dinámico (`List`) o tabla hash | El arreglo dinámico agrega al final en tiempo amortizado **O(1)**; la tabla hash inserta por clave en tiempo promedio **O(1)**. | Arreglo de tamaño fijo cuando se llena; debe redimensionarse o no admite más elementos. |
| Borrar | Tabla hash por clave o lista enlazada si ya se conoce el nodo | La tabla hash elimina por clave en promedio **O(1)**; la lista enlazada elimina en **O(1)** si ya se tiene la referencia al nodo. | Arreglo: eliminar un elemento puede requerir desplazar otros, **O(n)**. |
| Recorrer en orden | Arreglo dinámico o lista ordenada por fecha | Permite recorrer los registros secuencialmente. | Tabla hash: no garantiza el orden por fecha. |
| Buscar el menor | Árbol balanceado ordenado por importe | Puede localizar el menor en **O(log n)** si se mantiene ordenado. | Lista o arreglo desordenado: debe revisar todos los elementos, **O(n)**. |
| Buscar el mayor | Árbol balanceado ordenado por importe | Puede localizar el mayor en **O(log n)** si se mantiene ordenado. | Lista o arreglo desordenado: debe revisar todos los elementos, **O(n)**. |
| Buscar por rango | Árbol balanceado ordenado por fecha | Permite localizar el inicio del rango y recorrer los resultados; aproximadamente **O(log n + k)**, donde *k* es la cantidad de resultados. | Tabla hash: no organiza las fechas y normalmente debe revisar todos los registros, **O(n)**. |

> **Nota:** Los costos indicados son aproximados y dependen de la implementación. En una tabla hash, **O(1)** es el costo promedio; en el peor caso puede llegar a **O(n)**. Un árbol debe mantenerse balanceado para conservar costos logarítmicos.

### 5. Conclusión

En este caso, las operaciones más frecuentes son registrar ventas, buscar información y consultar el inventario. Por eso, una **tabla hash (`Dictionary`)** es conveniente para localizar rápidamente una venta mediante una clave única o consultar datos asociados a cada producto. Sin embargo, para ordenar las ventas por fecha y consultar periodos, resulta más conveniente un **árbol balanceado** o una colección ordenada. La estructura más adecuada depende de las operaciones que se realizan con mayor frecuencia y del volumen de información que maneja el proceso.
