public class Celda
{
    public int Fila;
    public int Columna;
    public bool Infectada;

    public Celda(int fila, int columna, bool infectada)
    {
        Fila = fila;
        Columna = columna;
        Infectada = infectada;
    }
}