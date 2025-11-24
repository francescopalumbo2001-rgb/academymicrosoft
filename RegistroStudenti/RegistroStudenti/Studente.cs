namespace RegistroStudenti;

public class Studente
{
    private string nome;
    private string cognome;
    private string matricola;
    private List<int> voti;

    public Studente(string nome, string cognome, string matricola)
    {
        this.nome = nome;
        this.cognome = cognome;
        this.matricola = matricola;
        voti = new List<int>();
    }

    public string Nome => nome;
    public string Cognome => cognome;
    public string Matricola => matricola;

    public double Media
    {
        get
        {
            if (voti.Count == 0) return 0;
            int somma = 0;
            foreach (int v in voti)
                somma += v;

            return (double)somma / voti.Count;
        }
    }

    public int NumeroVoti => voti.Count;

    public void AggiungiVoto(int voto)
    {
        if (voto < 1 || voto > 30)
        {
            Console.WriteLine("Voto non valido.");
            return;
        }
        voti.Add(voto);
    }

    public void RimuoviUltimoVoto()
    {
        if (voti.Count > 0)
            voti.RemoveAt(voti.Count - 1);
        else
            Console.WriteLine("Non ci sono voti da rimuovere.");
    }

    public void StampaLibretto()
    {
        Console.WriteLine($"Libretto di {Nome} {Cognome}, matricola {Matricola}");
        if (voti.Count == 0)
        {
            Console.WriteLine("Nessun voto registrato.");
            return;
        }

        Console.Write("Voti: ");
        foreach (int v in voti)
            Console.Write(v + " ");

        Console.WriteLine($"\nMedia: {Media:F2}");
    }

    public override string ToString()
    {
        return $"{Nome} {Cognome} - Matricola: {Matricola} - Media: {Media:F2}";
    }
}