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
}