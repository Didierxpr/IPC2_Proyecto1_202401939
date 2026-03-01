using System;

class Program
{
    static void Main(string[] args)
    {
        // Instancia principal que administra pacientes
        GestorPacientes gestor = new GestorPacientes();

        // Instancia para leer XML
        LectorXML lector = new LectorXML();

        // Instancia para escribir XML
        EscritorXML escritor = new EscritorXML();

        // Instancia para analizar patrones
        DetectorPatrones detector = new DetectorPatrones();

        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n===== SISTEMA DE ANALISIS EPIDEMIOLOGICO =====");
            Console.WriteLine("1. Cargar archivo XML");
            Console.WriteLine("2. Mostrar pacientes cargados");
            Console.WriteLine("3. Analizar paciente");
            Console.WriteLine("4. Generar archivo XML de salida");
            Console.WriteLine("5. Limpiar memoria");
            Console.WriteLine("6. Generar grafica de un paciente");
            Console.WriteLine("7. Simulacion visual completa");
            Console.WriteLine("8. Simulacion paso a paso");
            Console.WriteLine("9. Salir");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CargarXML(lector, gestor);
                    break;

                case "2":
                    gestor.MostrarPacientes();
                    break;

                case "3":
                    AnalizarPaciente(gestor, detector);
                    break;

                case "4":
                    GenerarSalida(escritor, gestor);
                    break;

                case "5":
                    gestor.LimpiarPacientes();
                    break;

                case "6":
                    GenerarGraficaPaciente(gestor);
                    break;

                case "7":
                    SimulacionVisual(gestor, detector);
                    break;

                case "8":
                    SimulacionManual(gestor, detector);
                    break;

                case "9":
                    salir = true;
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }
        }
    }

    // Permite cargar archivo XML desde una ruta ingresada por el usuario
    static void CargarXML(LectorXML lector, GestorPacientes gestor)
    {
        Console.Write("Ingrese ruta del archivo XML: ");
        string ruta = Console.ReadLine();

        try
        {
            lector.CargarPacientes(ruta, gestor);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar archivo: " + ex.Message);
        }
    }

    // Permite analizar un paciente por nombre
    static void AnalizarPaciente(GestorPacientes gestor, DetectorPatrones detector)
    {
        Console.Write("Ingrese nombre del paciente: ");
        string nombre = Console.ReadLine();

        Paciente paciente = gestor.ObtenerPaciente(nombre);

        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado.");
            return;
        }

        // Ejecuta análisis completo
        detector.AnalizarPaciente(paciente);

        Console.WriteLine("\nResultado del paciente:");
        Console.WriteLine("Nombre: " + paciente.Nombre);
        Console.WriteLine("Resultado: " + paciente.Resultado);

        if (paciente.N > 0)
            Console.WriteLine("N: " + paciente.N);

        if (paciente.N1 > 0)
            Console.WriteLine("N1: " + paciente.N1);
    }

    // Genera archivo XML final con resultados
    static void GenerarSalida(EscritorXML escritor, GestorPacientes gestor)
    {
        Console.Write("Ingrese ruta para guardar XML de salida: ");
        string ruta = Console.ReadLine();

        try
        {
            escritor.GenerarSalida(ruta, gestor.Lista);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al generar archivo: " + ex.Message);
        }
    }
    static void GenerarGraficaPaciente(GestorPacientes gestor)
{
    Console.Write("Ingrese nombre del paciente: ");
    string nombre = Console.ReadLine();

    Paciente paciente = gestor.ObtenerPaciente(nombre);

    if (paciente == null)
    {
        Console.WriteLine("Paciente no encontrado.");
        return;
    }

    GeneradorGraphviz generador = new GeneradorGraphviz();

    string rutaDot = "rejilla.dot";

    // Genera archivo DOT
    generador.GenerarDot(paciente.RejillaInicial, rutaDot);

    // Genera imagen PNG
    generador.GenerarImagen(rutaDot);
}
static void SimulacionVisual(GestorPacientes gestor, DetectorPatrones detector)
{
    Console.Write("Ingrese nombre del paciente: ");
    string nombre = Console.ReadLine();

    Paciente paciente = gestor.ObtenerPaciente(nombre);

    if (paciente == null)
    {
        Console.WriteLine("Paciente no encontrado.");
        return;
    }

    detector.AnalizarConVisualizacion(paciente);

    Console.WriteLine("Simulación visual completada.");
}
static void SimulacionManual(GestorPacientes gestor, DetectorPatrones detector)
{
    Console.Write("Ingrese nombre del paciente: ");
    string nombre = Console.ReadLine();

    Paciente paciente = gestor.ObtenerPaciente(nombre);

    if (paciente == null)
    {
        Console.WriteLine("Paciente no encontrado.");
        return;
    }

    detector.SimulacionPasoAPaso(paciente);
}
}