Nombre: Richard Mancachi Tantani

# P2.1 — Elección de estructuras de datos

## 1. Estructuras elegidas

Para este sistema utilizaría una cola de prioridad (Min-Heap) y un diccionario (Dictionary). La cola de prioridad permite atender primero al paciente con mayor urgencia y, cuando dos tienen la misma urgencia, utiliza el número de llegada para respetar el orden. El diccionario permite consultar rápidamente si un paciente todavía está esperando.

## 2. Tabla de operaciones

| Operación | Cantidad por día | Estructura utilizada | Costo O |
|---|---:|---|---|
| Registrar | 100.000 | Min-Heap + Dictionary | O(log n) |
| AtenderSiguiente | 100.000 | Min-Heap + Dictionary | O(log n) |
| EstaEsperando | 30.000 | Dictionary | O(1) promedio |
| Esperando | Según consulta | Count del Dictionary | O(1) |

## 3. ¿Por qué no utilizar una lista simple?

Con 100.000 pacientes, la lista simple tiene que recorrer los elementos para encontrar al paciente más urgente. Solamente en las 100.000 atenciones realizaría aproximadamente 5.000.050.000 comparaciones.

Además, al eliminar un paciente con RemoveAt, puede tener que desplazar muchos elementos. Las 30.000 consultas de EstaEsperando también pueden recorrer hasta 100.000 pacientes cada una, llegando a 3.000.000.000 de comprobaciones en el peor caso.

En cambio, con la solución mejorada, las operaciones de registro y atención utilizan el heap, con un máximo aproximado de 16,6 niveles por operación para 100.000 pacientes. Esto representa alrededor de 3.320.000 niveles entre registros y atenciones. Las consultas se realizan mediante el diccionario, con O(1) promedio.

Por eso, la solución mejorada evita recorrer toda la lista repetidamente y reduce bastante el trabajo.

## 4. ¿En qué situación cambiaría de opinión?

Si la clínica dejara de clasificar a los pacientes por urgencia y solamente los atendiera según su orden de llegada, utilizaría una cola normal (Queue), porque permitiría registrar y atender pacientes en O(1), sin necesidad de utilizar una cola de prioridad.
