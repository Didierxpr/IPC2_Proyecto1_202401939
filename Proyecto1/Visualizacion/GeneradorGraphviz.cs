using System;
using System.IO;
using System.Diagnostics;

public class GeneradorGraphviz
{
    // Genera archivo .dot representando la rejilla
    public void GenerarDot(Rejilla rejilla, string rutaArchivoDot)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivoDot))
            {
                writer.WriteLine("digraph G {");
                writer.WriteLine("node [shape=plaintext]");
                writer.WriteLine("matriz [label=<");
                writer.WriteLine("<table border='1' cellborder='1' cellspacing='0'>");

                // Recorremos filas
                for (int i = 1; i <= rejilla.M; i++)
                {
                    writer.WriteLine("<tr>");

                    // Recorremos columnas
                    for (int j = 1; j <= rejilla.M; j++)
                    {
                        // Si la celda está infectada la pintamos roja
                        if (rejilla.Infectadas.Existe(i, j))
                            writer.WriteLine("<td bgcolor='red'> </td>");
                        else
                            writer.WriteLine("<td> </td>");
                    }

                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");
                writer.WriteLine(">];");
                writer.WriteLine("}");
            }

            Console.WriteLine("Archivo DOT generado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al generar DOT: " + ex.Message);
        }
    }

    // Convierte archivo .dot en imagen .png usando Graphviz
    public void GenerarImagen(string rutaArchivoDot)
    {
        try
        {
            string rutaImagenSalida = rutaArchivoDot.Replace(".dot", ".png");

            ProcessStartInfo informacionProceso = new ProcessStartInfo
            {
                FileName = "dot",  // Comando de Graphviz
                Arguments = $"-Tpng \"{rutaArchivoDot}\" -o \"{rutaImagenSalida}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process? proceso = Process.Start(informacionProceso);
            proceso?.WaitForExit();

            Console.WriteLine("Imagen generada en: " + rutaImagenSalida);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al generar imagen: " + ex.Message);
        }
    }
    // Genera gráfica de la lista enlazada de pacientes
public void GenerarGraficaListaPacientes(ListaPacientes lista, string rutaArchivoDot)
{
    try
    {
        using (StreamWriter writer = new StreamWriter(rutaArchivoDot))
        {
            writer.WriteLine("digraph ListaSimple {");

            // Configuración general
            writer.WriteLine("bgcolor=\"#f5f5f5\";");
            writer.WriteLine("label=\"Lista Simple de Pacientes\";");
            writer.WriteLine("labelloc=t;");
            writer.WriteLine("fontsize=24;");
            writer.WriteLine("fontcolor=\"#1565c0\";");
            writer.WriteLine("fontname=\"Helvetica Bold\";");
            writer.WriteLine("node [fontname=\"Helvetica\"];");
            writer.WriteLine("edge [fontname=\"Helvetica\"];");

            NodoPaciente aux = lista.Cabeza;
            int contador = 0;

            // Crear nodos
            while (aux != null)
            {
                Paciente p = aux.Dato;

                writer.WriteLine($@"
node{contador} [
    shape=record,
    style=""filled,rounded"",
    fillcolor=""lightblue:white"",
    gradientangle=90,
    color=""#1976d2"",
    penwidth=2,
    label=""{{
        Nombre: {p.Nombre} |
        Edad: {p.Edad} |
        Períodos: {p.Periodos} |
        Matriz: {p.M}x{p.M}
    }}""
];");

                aux = aux.Siguiente;
                contador++;
            }

            // Crear conexiones
            for (int i = 0; i < contador - 1; i++)
            {
                writer.WriteLine($@"
node{i} -> node{i + 1} [
    color=""#1976d2"",
    penwidth=2.5,
    arrowsize=1.2
];");
            }

            writer.WriteLine("}");
        }

        Console.WriteLine("Archivo DOT de lista generado correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al generar gráfica de lista: " + ex.Message);
    }
}
}