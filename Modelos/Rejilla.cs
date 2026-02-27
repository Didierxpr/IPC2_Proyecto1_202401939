public class Rejilla
{
    public int M; // tamaño MxM
    public ListaCeldas Infectadas;

    public Rejilla(int m)
    {
        M = m;
        Infectadas = new ListaCeldas();
    }
}