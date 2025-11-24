using System;
using System.Collections.Generic;
using System.Linq;

namespace RegistroStudenti
{
    class Program
    {
        static void Main()
        {
            List<Studente> studenti = new List<Studente>();
            int scelta = 0;

            do
            {
                Console.WriteLine("\n===== MENU GESTIONE STUDENTI =====");
                Console.WriteLine("1. Aggiungi studente");
                Console.WriteLine("2. Cerca studente per matricola");
                Console.WriteLine("3. Aggiungi voto a studente");
                Console.WriteLine("4. Visualizza tutti");
                Console.WriteLine("5. Studente con media più alta");
                Console.WriteLine("6. Esci");
                Console.Write("Scelta: ");

                int.TryParse(Console.ReadLine(), out scelta);

                switch (scelta)
                {
                    case 1:
                        AggiungiStudente(studenti);
                        break;
                    case 2:
                        CercaStudente(studenti);
                        break;
                    case 3:
                        AggiungiVotoStudente(studenti);
                        break;
                    case 4:
                        VisualizzaTutti(studenti);
                        break;
                    case 5:
                        TrovaStudenteMigliore(studenti);
                        break;
                }

            } while (scelta != 6);
        }

        static void AggiungiStudente(List<Studente> lista)
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Cognome: ");
            string cognome = Console.ReadLine();
            Console.Write("Matricola: ");
            string matricola = Console.ReadLine();

            lista.Add(new Studente(nome, cognome, matricola));
            Console.WriteLine("Studente aggiunto!");
        }

        static Studente TrovaPerMatricola(List<Studente> lista, string matricola)
        {
            return lista.FirstOrDefault(s => s.Matricola == matricola);
        }

        static void CercaStudente(List<Studente> lista)
        {
            Console.Write("Inserisci matricola: ");
            string mat = Console.ReadLine();

            var stud = TrovaPerMatricola(lista, mat);

            if (stud != null)
                Console.WriteLine(stud);
            else
                Console.WriteLine("Studente non trovato.");
        }

        static void AggiungiVotoStudente(List<Studente> lista)
        {
            Console.Write("Matricola: ");
            string mat = Console.ReadLine();

            var stud = TrovaPerMatricola(lista, mat);

            if (stud == null)
            {
                Console.WriteLine("Studente non trovato.");
                return;
            }

            Console.Write("Voto da aggiungere (1-30): ");
            int voto;
            if (int.TryParse(Console.ReadLine(), out voto))
            {
                stud.AggiungiVoto(voto);
                Console.WriteLine("Voto aggiunto!");
            }
            else
            {
                Console.WriteLine("Input non valido!");
            }
        }

        static void VisualizzaTutti(List<Studente> lista)
        {
            if (!lista.Any())
            {
                Console.WriteLine("Nessuno studente registrato.");
                return;
            }

            Console.WriteLine("\n--- Elenco studenti ---");
            foreach (var s in lista)
                Console.WriteLine(s);
        }

        static void TrovaStudenteMigliore(List<Studente> lista)
        {
            if (!lista.Any())
            {
                Console.WriteLine("Nessuno studente registrato.");
                return;
            }

            var migliore = lista.OrderByDescending(s => s.Media).First();
            Console.WriteLine("\nStudente con media più alta:");
            Console.WriteLine(migliore);
        }
    }
}
