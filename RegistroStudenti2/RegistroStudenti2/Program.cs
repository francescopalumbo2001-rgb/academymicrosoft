// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

namespace RegistroStudenti
{
    class Program
    {
        static void Main()
        {
            List<Studente> studenti = new List<Studente>();
            StoricoOperazioni storico = new StoricoOperazioni();
            CodaIscrizioni coda = new CodaIscrizioni();

            int scelta = -1;

            do
            {
                Console.WriteLine("\n===== REGISTRO STUDENTI =====");
                Console.WriteLine("1. Aggiungi studente");
                Console.WriteLine("2. Cerca per matricola");
                Console.WriteLine("3. Aggiungi voto a studente");
                Console.WriteLine("4. Visualizza tutti");
                Console.WriteLine("5. Studente con media più alta");
                Console.WriteLine("6. Ordinamento studenti");
                Console.WriteLine("7. Storico operazioni");
                Console.WriteLine("8. Coda iscrizioni");
                Console.WriteLine("0. Esci");
                Console.Write("Scelta: ");

                int.TryParse(Console.ReadLine(), out scelta);

                switch (scelta)
                {
                    case 1:
                        Console.Write("Nome: ");
                        string n = Console.ReadLine();
                        Console.Write("Cognome: ");
                        string c = Console.ReadLine();
                        Console.Write("Matricola: ");
                        string m = Console.ReadLine();

                        studenti.Add(new Studente(n, c, m));
                        storico.Registra($"Aggiunto studente {m}");
                        break;

                    case 2:
                        Console.Write("Inserisci matricola: ");
                        string mc = Console.ReadLine();
                        var st = studenti.Find(s => s.Matricola == mc);
                        Console.WriteLine(st != null ? st.ToString() : "Non trovato");
                        break;

                    case 3:
                        Console.Write("Matricola: ");
                        string mat = Console.ReadLine();
                        var s2 = studenti.Find(s => s.Matricola == mat);
                        if (s2 != null)
                        {
                            Console.Write("Voto: ");
                            int v = int.Parse(Console.ReadLine());
                            s2.AggiungiVoto(v);
                            storico.Registra($"Aggiunto voto {v} a {mat}");
                        }
                        else Console.WriteLine("Studente non trovato");
                        break;

                    case 4:
                        studenti.ForEach(Console.WriteLine);
                        break;

                    case 5:
                        if (studenti.Count > 0)
                        {
                            var best = studenti.MaxBy(s => s.Media);
                            Console.WriteLine("Migliore: " + best);
                        }
                        else Console.WriteLine("Nessuno studente presente.");
                        break;

                    case 6:
                        studenti.Sort();
                        Console.WriteLine("Studenti ordinati!");
                        break;

                    case 7:
                        Console.WriteLine("Storico operazioni:");
                        foreach (var op in storico.OttieniTutte())
                            Console.WriteLine("- " + op);
                        break;

                    case 8:
                        Console.WriteLine("1. Aggiungi richiesta");
                        Console.WriteLine("2. Approva prossima");
                        Console.WriteLine("3. Vedi prossimo");
                        Console.WriteLine("4. Vedi tutte");
                        int sc = int.Parse(Console.ReadLine());

                        if (sc == 1)
                        {
                            Console.Write("Matricola richiesta: ");
                            string r = Console.ReadLine();
                            var ss = studenti.Find(s => s.Matricola == r);
                            if (ss != null)
                                coda.AggiungiRichiesta(ss);
                        }
                        else if (sc == 2) Console.WriteLine(coda.ApprovaProssima());
                        else if (sc == 3) Console.WriteLine(coda.ProssimoInCoda());
                        else if (sc == 4) coda.OttieniRichiesteInAttesa().ForEach(Console.WriteLine);

                        break;
                }

            } while (scelta != 0);
        }
    }
}
