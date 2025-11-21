using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public struct Anagrafica
    {
        public string CodID;
        public string Nome;
        public string Cognome;
        public int Eta;
        public string Telefono;
        public string Email;
    }

    static void Main()
    {
        List<Anagrafica> rubrica = new List<Anagrafica>();
        int nextID = 1;
        bool continua = true;

        while (continua)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIONE ANAGRAFICA ===\n");
            Console.WriteLine("1) Inserisci nuova scheda");
            Console.WriteLine("2) Visualizza anagrafica");
            Console.WriteLine("3) Modifica scheda");
            Console.WriteLine("4) Elimina scheda");
            Console.WriteLine("5) Esci");
            Console.Write("\nScelta: ");

            string scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    InserisciScheda(rubrica, ref nextID);
                    break;

                case "2":
                    VisualizzaAnagrafica(rubrica);
                    break;

                case "3":
                    ModificaScheda(rubrica);
                    break;

                case "4":
                    EliminaScheda(rubrica);
                    break;

                case "5":
                    continua = false;
                    break;

                default:
                    Console.WriteLine("Scelta non valida. Premi un tasto per continuare...");
                    Console.ReadKey();
                    break;
            }
        }
    }

  

    static bool EmailEsiste(List<Anagrafica> rubrica, string email, string esclusoCodID = "")
    {
        return rubrica.Any(p => p.Email == email && p.CodID != esclusoCodID);
    }

   

    static void InserisciScheda(List<Anagrafica> rubrica, ref int nextID)
    {
        Console.Clear();
        Console.WriteLine("=== INSERIMENTO NUOVA SCHEDA ===\n");

        Anagrafica p = new Anagrafica();
        p.CodID = $"ID{nextID:D3}";
        nextID++;

        Console.Write("Nome: ");
        p.Nome = Console.ReadLine();

        Console.Write("Cognome: ");
        p.Cognome = Console.ReadLine();

        Console.Write("Età: ");
        p.Eta = int.Parse(Console.ReadLine());

        Console.Write("Telefono: ");
        p.Telefono = Console.ReadLine();

        Console.Write("Email: ");
        p.Email = Console.ReadLine();

        // Controllo unicità email
        while (EmailEsiste(rubrica, p.Email))
        {
            Console.WriteLine("ERRORE: email già presente! Inserisci un'altra email:");
            p.Email = Console.ReadLine();
        }

        rubrica.Add(p);
        Ordina(rubrica);

        Console.WriteLine("\nScheda inserita!");
        Console.ReadKey();
    }

   

    static void VisualizzaAnagrafica(List<Anagrafica> rubrica)
    {
        Console.Clear();
        Console.WriteLine("=== ANAGRAFICA ===\n");

        if (rubrica.Count == 0)
        {
            Console.WriteLine("Nessuna scheda presente.");
        }
        else
        {
            foreach (var p in rubrica)
            {
                Console.WriteLine($"CodID: {p.CodID}");
                Console.WriteLine($"Nome: {p.Nome}");
                Console.WriteLine($"Cognome: {p.Cognome}");
                Console.WriteLine($"Età: {p.Eta}");
                Console.WriteLine($"Telefono: {p.Telefono}");
                Console.WriteLine($"Email: {p.Email}");
                Console.WriteLine("-----------------------------");
            }
        }

        Console.WriteLine("Premi un tasto per continuare...");
        Console.ReadKey();
    }

  

    static void ModificaScheda(List<Anagrafica> rubrica)
    {
        Console.Clear();
        Console.WriteLine("=== MODIFICA SCHEDA ===\n");

        if (rubrica.Count == 0)
        {
            Console.WriteLine("Nessuna scheda da modificare.");
            Console.ReadKey();
            return;
        }

        Console.Write("Inserisci CodID della scheda da modificare (es. ID001): ");
        string id = Console.ReadLine();

        int index = rubrica.FindIndex(p => p.CodID == id);

        if (index == -1)
        {
            Console.WriteLine("Scheda non trovata.");
            Console.ReadKey();
            return;
        }

        Anagrafica p = rubrica[index];

        Console.WriteLine("\nLascia vuoto un campo per non modificarlo.\n");

        Console.Write($"Nome ({p.Nome}): ");
        string tmp = Console.ReadLine();
        if (tmp != "") p.Nome = tmp;

        Console.Write($"Cognome ({p.Cognome}): ");
        tmp = Console.ReadLine();
        if (tmp != "") p.Cognome = tmp;

        Console.Write($"Età ({p.Eta}): ");
        tmp = Console.ReadLine();
        if (tmp != "") p.Eta = int.Parse(tmp);

        Console.Write($"Telefono ({p.Telefono}): ");
        tmp = Console.ReadLine();
        if (tmp != "") p.Telefono = tmp;

        Console.Write($"Email ({p.Email}): ");
        tmp = Console.ReadLine();
        if (tmp != "")
        {
            while (EmailEsiste(rubrica, tmp, p.CodID))
            {
                Console.WriteLine("ERRORE: email già registrata! Inserisci un'altra email:");
                tmp = Console.ReadLine();
            }
            p.Email = tmp;
        }

        rubrica[index] = p;
        Ordina(rubrica);

        Console.WriteLine("\nScheda modificata!");
        Console.ReadKey();
    }

  

    static void EliminaScheda(List<Anagrafica> rubrica)
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINA SCHEDA ===\n");

        if (rubrica.Count == 0)
        {
            Console.WriteLine("Nessuna scheda da eliminare.");
            Console.ReadKey();
            return;
        }

        Console.Write("Inserisci CodID della scheda da eliminare: ");
        string id = Console.ReadLine();

        int index = rubrica.FindIndex(p => p.CodID == id);

        if (index == -1)
        {
            Console.WriteLine("Scheda non trovata.");
            Console.ReadKey();
            return;
        }

        rubrica.RemoveAt(index);

        Console.WriteLine("\nScheda eliminata!");
        Console.ReadKey();
    }

    

    static void Ordina(List<Anagrafica> rubrica)
    {
        rubrica.Sort((a, b) => a.Cognome.CompareTo(b.Cognome));
    }
}
