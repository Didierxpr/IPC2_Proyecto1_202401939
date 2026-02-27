public class NodoPaciente
{
    public Paciente Dato;
    public NodoPaciente Siguiente;

    public NodoPaciente(Paciente dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}