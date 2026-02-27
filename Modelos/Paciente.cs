public class Paciente
{
    public string Nombre;
    public int Edad;
    public int Periodos;   // máximo a evaluar (del XML)
    public int M;          // tamaño rejilla (del XML)

    public Rejilla RejillaInicial;

    // resultados
    public string Resultado; // "leve" | "grave" | "mortal"
    public int N;            // periodo donde aparece repetición relevante
    public int N1;           // periodo de repetición secundaria (si aplica)

    public Paciente(string nombre, int edad, int periodos, int m)
    {
        Nombre = nombre;
        Edad = edad;
        Periodos = periodos;
        M = m;

        RejillaInicial = new Rejilla(m);

        Resultado = "leve";
        N = 0;
        N1 = 0;
    }
}