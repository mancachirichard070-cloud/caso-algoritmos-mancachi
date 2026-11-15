# H1: Registro de ventas y control de inventario de detergentes

El proceso que se analizará es el registro de ventas y el control de
inventario de detergentes en un pequeño negocio. Actualmente, se
comercializa detergente de la marca UNO en dos presentaciones: 620 g y
4,7 kg. El precio de venta es de Bs 12 por unidad para la presentación
de 620 g y Bs 90 para la de 4,7 kg. Cada vez que se realiza una venta,
se registra la fecha, el producto vendido, la cantidad, el precio
unitario y el importe total. Con estos datos se calcula el monto de la
venta y se actualiza la cantidad disponible en el inventario. En algunas
ocasiones se venden varias unidades en una sola operación, llegando
incluso a 15 unidades. El registro de las ventas y la revisión de las
existencias requieren aproximadamente 30 minutos al día, tiempo que se
utiliza para anotar las operaciones, realizar los cálculos y verificar
los productos disponibles. Este proceso afecta principalmente al
propietario del negocio y a la persona encargada de las ventas y del
inventario, ya que deben realizar estas tareas manualmente y pueden
cometer errores en los registros. Si la información no se actualiza
oportunamente, pueden existir diferencias entre el inventario registrado
y las existencias físicas, ocasionando que se ofrezcan productos que ya
no están disponibles. Para analizar este proceso se utilizará un archivo
CSV con 20 registros reales de ventas, organizados por fecha, producto,
cantidad, precio unitario e importe total. Se estima una complejidad
O(n) para la revisión de los registros, porque en el peor caso es
necesario recorrer cada fila una vez para consultar o comprobar la
información; por ejemplo, si existen 20 registros, se pueden revisar
hasta 20 filas, y si aumentan a 100, el trabajo también aumenta
proporcionalmente. Por ello, se busca organizar y agilizar el registro
de las operaciones, facilitar los cálculos y mantener actualizado el
inventario.

## Muestra de datos reales

Los 20 registros de ventas se encuentran en el archivo `ventas.csv`, con
los campos `fecha`, `producto`, `cantidad`, `precio_unitario_bs` y
`total_bs`.
