using System;

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
}