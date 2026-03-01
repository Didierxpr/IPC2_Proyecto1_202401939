public class NodoEstado
{
    public Rejilla Estado;     // Rejilla en ese período
    public int Periodo;        // Número de período
    public NodoEstado Siguiente;

    public NodoEstado(Rejilla estado, int periodo)
    {
        Estado = estado;
        Periodo = periodo;
        Siguiente = null;
    }
}