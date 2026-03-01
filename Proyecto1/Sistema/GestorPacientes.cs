using System;

public class GestorPacientes
{
    // Lista enlazada propia que almacena todos los pacientes cargados
    public ListaPacientes Lista;

    public GestorPacientes()
    {
        Lista = new ListaPacientes();
    }

    // Agrega un paciente a la lista
    public void AgregarPaciente(Paciente paciente)
    {
        Lista.Agregar(paciente);
    }

    // Busca un paciente por nombre
    public Paciente ObtenerPaciente(string nombre)
    {
        return Lista.BuscarPorNombre(nombre);
    }

    // Muestra todos los pacientes cargados
    public void MostrarPacientes()
    {
        NodoPaciente aux = Lista.Cabeza;

        if (aux == null)
        {
            Console.WriteLine("No hay pacientes cargados.");
            return;
        }

        Console.WriteLine("Pacientes disponibles:");

        while (aux != null)
        {
            Console.WriteLine("- " + aux.Dato.Nombre);
            aux = aux.Siguiente;
        }
    }

    // Limpia toda la lista (libera memoria)
    public void LimpiarPacientes()
    {
        Lista.Limpiar();
        Console.WriteLine("Memoria de pacientes limpiada.");
    }
}   