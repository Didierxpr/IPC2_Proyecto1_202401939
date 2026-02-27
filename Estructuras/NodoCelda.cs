public class NodoCelda
{
    public Celda Dato;
    public NodoCelda Siguiente;

    public NodoCelda(Celda dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}