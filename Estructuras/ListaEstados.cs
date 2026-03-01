public class ListaEstados
{
    public NodoEstado Cabeza;

    // Agrega un nuevo estado a la lista
    public void Agregar(Rejilla estado, int periodo)
    {
        NodoEstado nuevo = new NodoEstado(estado, periodo);

        if (Cabeza == null)
        {
            Cabeza = nuevo;
        }
        else
        {
            NodoEstado aux = Cabeza;
            while (aux.Siguiente != null)
                aux = aux.Siguiente;

            aux.Siguiente = nuevo;
        }
    }
}