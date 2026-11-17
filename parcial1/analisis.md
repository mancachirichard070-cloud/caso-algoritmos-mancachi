Nombre completo:Richard Mancachi Tantani

PARCIAL 1 - VARIANTE B

P1.1 - ANALIZAR

Parte A. Análisis de los cuatro fragmentos

F1. Costo en el peor caso: O(n).

Justificación: El ciclo se ejecuta n veces y en cada repetición realiza una comprobación y, si corresponde, un incremento, que son operaciones constantes.

F2. Costo en el peor caso: O(n²).

Justificación: Tiene dos ciclos anidados; cada uno se ejecuta n veces, por lo que se realizan n por n iteraciones.

F3. Costo en el peor caso: O(log n).

Justificación: La variable k comienza en 1 y se multiplica por 2 en cada repetición hasta alcanzar o superar n; por eso el número de iteraciones es logarítmico.

F4. Costo en el peor caso: O(n log n).

Justificación: El ciclo externo se ejecuta n veces y, en cada repetición, el ciclo interno divide k entre 3, por lo que realiza aproximadamente log₃(n) iteraciones.

Resumen de la parte A

| Fragmento | Costo en el peor caso |
|---|---|
| F1 | O(n) |
| F2 | O(n²) |
| F3 | O(log n) |
| F4 | O(n log n) |

Parte B. Resolución de recurrencias con el teorema maestro
Se utiliza la forma:

T(n) = aT(n/b) + O(n^d)

Se identifican a, b y d. Luego se compara a con b^d.

Ejercicio (i)
Ecuación: T(n) = 4T(n/3) + n

Identificación:
- a = 4, porque hay 4 subproblemas.
- b = 3, porque el tamaño se divide entre 3.
- d = 1, porque el trabajo adicional es n¹.

Comparación:
- b^d = 3¹ = 3.
- a = 4.
- Como 4 > 3, se aplica el caso en que a > b^d.

Resultado:

T(n) = O(n^(log₃ 4))

Conclusión: El costo crece aproximadamente como n elevado a logaritmo en base 3 de 4.

Ejercicio (ii)

Ecuación:T(n) = 2T(n/2) + n²

Identificación:
- a = 2, porque hay 2 subproblemas.
- b = 2, porque el tamaño se divide entre 2.
- d = 2, porque el trabajo adicional es n².

Comparación:
- b^d = 2² = 4.
- a = 2.
- Como 2 < 4, se aplica el caso en que a < b^d.

Resultado:

T(n) = O(n²)
