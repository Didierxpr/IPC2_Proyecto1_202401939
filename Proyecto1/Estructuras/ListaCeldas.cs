public class ListaCeldas
{
    public NodoCelda Cabeza;
    public int Cantidad;

    public void Agregar(Celda celda)
    {
        NodoCelda nuevo = new NodoCelda(celda);

        if (Cabeza == null)
        {
            Cabeza = nuevo;
        }
        else
        {
            NodoCelda aux = Cabeza;
            while (aux.Siguiente != null)
                aux = aux.Siguiente;

            aux.Siguiente = nuevo;
        }

        Cantidad++;
    }

    public bool Existe(int fila, int columna)
    {
        NodoCelda aux = Cabeza;
        while (aux != null)
        {
            if (aux.Dato.Fila == fila && aux.Dato.Columna == columna)
                return true;
            aux = aux.Siguiente;
        }
        return false;
    }

    public void Limpiar()
    {
        Cabeza = null;
        Cantidad = 0;
    }
}