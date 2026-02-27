using System;
using System.Xml;

public class LectorXML
{
    // Método que carga pacientes desde un archivo XML
    public void CargarPacientes(string ruta, GestorPacientes gestor)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(ruta); // Carga el archivo XML

        // Obtiene todos los nodos <paciente>
        XmlNodeList pacientes = doc.GetElementsByTagName("paciente");

        foreach (XmlNode nodoPaciente in pacientes)
        {
            // Extrae datos personales
            string nombre = nodoPaciente["datospersonales"]["nombre"].InnerText;
            int edad = int.Parse(nodoPaciente["datospersonales"]["edad"].InnerText);

            // Extrae número máximo de periodos
            int periodos = int.Parse(nodoPaciente["periodos"].InnerText);

            // Extrae tamaño de rejilla
            int m = int.Parse(nodoPaciente["m"].InnerText);

            // Crea el objeto Paciente
            Paciente paciente = new Paciente(nombre, edad, periodos, m);

            // Obtiene todas las celdas infectadas
            XmlNodeList celdas = nodoPaciente.GetElementsByTagName("celda");

            foreach (XmlNode celda in celdas)
            {
                // Lee fila y columna desde atributos
                int fila = int.Parse(celda.Attributes["f"].Value);
                int columna = int.Parse(celda.Attributes["c"].Value);

                // Crea celda infectada
                Celda nuevaCelda = new Celda(fila, columna, true);

                // Agrega celda a la rejilla inicial
                paciente.RejillaInicial.Infectadas.Agregar(nuevaCelda);
            }

            // Agrega paciente al sistema
            gestor.AgregarPaciente(paciente);
        }

        Console.WriteLine("Pacientes cargados correctamente.");
    }
}