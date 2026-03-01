using System;
using System.IO;

public class DetectorPatrones
{
    private MotorSimulacion motor;

    public DetectorPatrones()
    {
        motor = new MotorSimulacion();
    }

    // Analiza un paciente completo
    public void AnalizarPaciente(Paciente paciente)
    {
        ListaEstados estados = new ListaEstados();

        Rejilla actual = paciente.RejillaInicial;

        // Guardamos estado inicial como período 0
        estados.Agregar(actual, 0);

        for (int periodo = 1; periodo <= paciente.Periodos; periodo++)
        {
            // Generamos siguiente período
            Rejilla siguiente = motor.GenerarSiguientePeriodo(actual);

            // Buscamos si ya existía ese estado antes
            NodoEstado repetido = BuscarEstadoRepetido(estados, siguiente);

            if (repetido != null)
            {
                // Detectamos repetición
                int N = periodo;
                int N1 = periodo - repetido.Periodo;

                paciente.N = N;
                paciente.N1 = N1;

                // Determinamos resultado
                if (repetido.Periodo == 0)
                {
                    // Se repite el patrón inicial
                    if (N == 1)
                        paciente.Resultado = "mortal";
                    else
                        paciente.Resultado = "grave";
                }
                else
                {
                    // Patrón distinto que luego se repite
                    if (N1 == 1)
                        paciente.Resultado = "mortal";
                    else
                        paciente.Resultado = "grave";
                }

                return;
            }

            // Si no se repite, lo agregamos a lista
            estados.Agregar(siguiente, periodo);

            actual = siguiente;
        }

        // Si nunca se repite dentro del límite
        paciente.Resultado = "leve";
    }

    // Busca si una rejilla ya existía en estados anteriores
    private NodoEstado BuscarEstadoRepetido(ListaEstados estados, Rejilla nueva)
    {
        NodoEstado aux = estados.Cabeza;

        while (aux != null)
        {
            if (SonIguales(aux.Estado, nueva))
                return aux;

            aux = aux.Siguiente;
        }

        return null;
    }

    // Compara dos rejillas
    private bool SonIguales(Rejilla r1, Rejilla r2)
    {
        if (r1.Infectadas.Cantidad != r2.Infectadas.Cantidad)
            return false;

        NodoCelda aux = r1.Infectadas.Cabeza;

        while (aux != null)
        {
            if (!r2.Infectadas.Existe(aux.Dato.Fila, aux.Dato.Columna))
                return false;

            aux = aux.Siguiente;
        }

        return true;
    }

    // Simula generando imagen por cada período
public void AnalizarConVisualizacion(Paciente paciente)
{
    MotorSimulacion motor = new MotorSimulacion();
    GeneradorGraphviz generador = new GeneradorGraphviz();

    ListaEstados estados = new ListaEstados();

    Rejilla actual = paciente.RejillaInicial;

    // Carpeta donde se guardarán las imágenes
    string carpeta = "Simulaciones";
    if (!Directory.Exists(carpeta))
        Directory.CreateDirectory(carpeta);

    // Guardamos período 0
    estados.Agregar(actual, 0);

    string rutaDot = $"{carpeta}/{paciente.Nombre}_periodo_0.dot";
    generador.GenerarDot(actual, rutaDot);
    generador.GenerarImagen(rutaDot);

    for (int periodo = 1; periodo <= paciente.Periodos; periodo++)
    {
        Rejilla siguiente = motor.GenerarSiguientePeriodo(actual);

        // Generar gráfica del período actual
        rutaDot = $"{carpeta}/{paciente.Nombre}_periodo_{periodo}.dot";
        generador.GenerarDot(siguiente, rutaDot);
        generador.GenerarImagen(rutaDot);

        NodoEstado repetido = BuscarEstadoRepetido(estados, siguiente);

        if (repetido != null)
        {
            int N = periodo;
            int N1 = periodo - repetido.Periodo;

            paciente.N = N;
            paciente.N1 = N1;

            if (repetido.Periodo == 0)
            {
                paciente.Resultado = (N == 1) ? "mortal" : "grave";
            }
            else
            {
                paciente.Resultado = (N1 == 1) ? "mortal" : "grave";
            }

            Console.WriteLine("Repetición detectada. Simulación finalizada.");
            return;
        }

        estados.Agregar(siguiente, periodo);
        actual = siguiente;
    }

    paciente.Resultado = "leve";
    Console.WriteLine("No hubo repetición dentro del límite.");
}
// Simulación manual período por período
public void SimulacionPasoAPaso(Paciente paciente)
{
    MotorSimulacion motor = new MotorSimulacion();
    GeneradorGraphviz generador = new GeneradorGraphviz();

    ListaEstados estados = new ListaEstados();

    Rejilla actual = paciente.RejillaInicial;

    string carpeta = "SimulacionesPasoAPaso";
    if (!Directory.Exists(carpeta))
        Directory.CreateDirectory(carpeta);

    estados.Agregar(actual, 0);

    int periodo = 0;

    while (periodo <= paciente.Periodos)
    {
        Console.WriteLine($"\n===== PERIODO {periodo} =====");

        int infectadas = motor.ContarInfectadas(actual);
        int sanas = motor.ContarSanas(actual);

        Console.WriteLine($"Infectadas: {infectadas}");
        Console.WriteLine($"Sanas: {sanas}");

        string rutaDot = $"{carpeta}/{paciente.Nombre}_periodo_{periodo}.dot";
        generador.GenerarDot(actual, rutaDot);
        generador.GenerarImagen(rutaDot);

        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();

        if (periodo == paciente.Periodos)
            break;

        Rejilla siguiente = motor.GenerarSiguientePeriodo(actual);

        NodoEstado repetido = BuscarEstadoRepetido(estados, siguiente);

        if (repetido != null)
        {
            int N = periodo + 1;
            int N1 = (periodo + 1) - repetido.Periodo;

            paciente.N = N;
            paciente.N1 = N1;

            if (repetido.Periodo == 0)
                paciente.Resultado = (N == 1) ? "mortal" : "grave";
            else
                paciente.Resultado = (N1 == 1) ? "mortal" : "grave";

            Console.WriteLine("Repetición detectada.");
            Console.WriteLine($"Resultado: {paciente.Resultado}");
            return;
        }

        estados.Agregar(siguiente, periodo + 1);
        actual = siguiente;
        periodo++;
    }

    paciente.Resultado = "leve";
    Console.WriteLine("No hubo repetición dentro del límite.");
}
}