using System;

public class MotorSimulacion
{
    // Genera la siguiente rejilla según las reglas del problema
    public Rejilla GenerarSiguientePeriodo(Rejilla actual)
    {
        // Nueva rejilla vacía del mismo tamaño
        Rejilla nueva = new Rejilla(actual.M);

        NodoCelda aux = actual.Infectadas.Cabeza;

        // Recorremos cada celda infectada actual
        while (aux != null)
        {
            int fila = aux.Dato.Fila;
            int columna = aux.Dato.Columna;

            // Contamos vecinos infectados
            int vecinos = ContarVecinos(actual, fila, columna);

            // Regla 1: sigue infectada si tiene 2 o 3 vecinos
            if (vecinos == 2 || vecinos == 3)
            {
                nueva.Infectadas.Agregar(new Celda(fila, columna, true));
            }

            // Evaluamos también sus vecinos sanos
            EvaluarVecinosSanos(actual, nueva, fila, columna);

            aux = aux.Siguiente;
        }

        return nueva;
    }

    // Cuenta cuántos vecinos infectados tiene una celda
    private int ContarVecinos(Rejilla rejilla, int fila, int columna)
    {
        int conteo = 0;

        // Recorremos las 8 posiciones vecinas manualmente
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // omitimos la misma celda

                int nuevaFila = fila + i;
                int nuevaCol = columna + j;

                // Verificamos límites de la rejilla
                if (nuevaFila >= 1 && nuevaFila <= rejilla.M &&
                    nuevaCol >= 1 && nuevaCol <= rejilla.M)
                {
                    // Si existe en lista de infectadas, sumamos
                    if (rejilla.Infectadas.Existe(nuevaFila, nuevaCol))
                        conteo++;
                }
            }
        }

        return conteo;
    }

    // Evalúa celdas sanas alrededor para posible contagio
    private void EvaluarVecinosSanos(Rejilla actual, Rejilla nueva, int fila, int columna)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int nuevaFila = fila + i;
                int nuevaCol = columna + j;

                if (nuevaFila < 1 || nuevaFila > actual.M ||
                    nuevaCol < 1 || nuevaCol > actual.M)
                    continue;

                // Si NO está infectada actualmente
                if (!actual.Infectadas.Existe(nuevaFila, nuevaCol))
                {
                    int vecinos = ContarVecinos(actual, nuevaFila, nuevaCol);

                    // Regla 2: se infecta si tiene exactamente 3 vecinos
                    if (vecinos == 3)
                    {
                        // Evitamos duplicados en nueva lista
                        if (!nueva.Infectadas.Existe(nuevaFila, nuevaCol))
                        {
                            nueva.Infectadas.Agregar(
                                new Celda(nuevaFila, nuevaCol, true));
                        }
                    }
                }
            }
        }
    }

    // Cuenta cuántas celdas infectadas existen
    public int ContarInfectadas(Rejilla rejilla)
    {
        return rejilla.Infectadas.Cantidad;
    }

    // Calcula cantidad de celdas sanas
    public int ContarSanas(Rejilla rejilla)
    {
        int total = rejilla.M * rejilla.M;
        return total - rejilla.Infectadas.Cantidad;
    }
}