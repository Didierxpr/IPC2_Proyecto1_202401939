using System;
using System.Xml;

public class EscritorXML
{
    // Genera archivo XML de salida con resultados finales
    public void GenerarSalida(string ruta, ListaPacientes lista)
    {
        XmlDocument doc = new XmlDocument();

        // Nodo raíz <pacientes>
        XmlElement raiz = doc.CreateElement("pacientes");
        doc.AppendChild(raiz);

        NodoPaciente aux = lista.Cabeza;

        // Recorremos todos los pacientes cargados
        while (aux != null)
        {
            Paciente p = aux.Dato;

            // <paciente>
            XmlElement paciente = doc.CreateElement("paciente");

            // <datospersonales>
            XmlElement datos = doc.CreateElement("datospersonales");

            XmlElement nombre = doc.CreateElement("nombre");
            nombre.InnerText = p.Nombre;

            XmlElement edad = doc.CreateElement("edad");
            edad.InnerText = p.Edad.ToString();

            datos.AppendChild(nombre);
            datos.AppendChild(edad);

            paciente.AppendChild(datos);

            // <periodos>
            XmlElement periodos = doc.CreateElement("periodos");
            periodos.InnerText = p.Periodos.ToString();
            paciente.AppendChild(periodos);

            // <m>
            XmlElement m = doc.CreateElement("m");
            m.InnerText = p.M.ToString();
            paciente.AppendChild(m);

            // <resultado>
            XmlElement resultado = doc.CreateElement("resultado");
            resultado.InnerText = p.Resultado;
            paciente.AppendChild(resultado);

            // Solo agregamos <n> si aplica
            if (p.N > 0)
            {
                XmlElement n = doc.CreateElement("n");
                n.InnerText = p.N.ToString();
                paciente.AppendChild(n);
            }

            // Solo agregamos <n1> si aplica
            if (p.N1 > 0)
            {
                XmlElement n1 = doc.CreateElement("n1");
                n1.InnerText = p.N1.ToString();
                paciente.AppendChild(n1);
            }

            raiz.AppendChild(paciente);

            aux = aux.Siguiente;
        }

        // Guarda el archivo
        doc.Save(ruta);

        Console.WriteLine("Archivo XML de salida generado correctamente.");
    }
}