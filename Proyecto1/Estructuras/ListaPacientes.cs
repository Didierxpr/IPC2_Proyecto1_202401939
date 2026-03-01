public class ListaPacientes
{
    public NodoPaciente Cabeza;
    public int Cantidad;

    public void Agregar(Paciente paciente)
    {
        NodoPaciente nuevo = new NodoPaciente(paciente);

        if (Cabeza == null)
        {
            Cabeza = nuevo;
        }
        else
        {
            NodoPaciente aux = Cabeza;
            while (aux.Siguiente != null)
                aux = aux.Siguiente;

            aux.Siguiente = nuevo;
        }

        Cantidad++;
    }

    public Paciente BuscarPorNombre(string nombre)
    {
        NodoPaciente aux = Cabeza;
        while (aux != null)
        {
            if (aux.Dato.Nombre == nombre)
                return aux.Dato;

            aux = aux.Siguiente;
        }
        return null;
    }

    public void Limpiar()
    {
        Cabeza = null;
        Cantidad = 0;
    }
}