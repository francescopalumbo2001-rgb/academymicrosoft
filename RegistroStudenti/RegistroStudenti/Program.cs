using System;
using System.Collections.Generic;
using System.Linq;
using RegistroStudenti;
using RegistroStudenti.Services;
using RegistroStudenti.Repository;

namespace RegistroStudenti
{
    class Program
    {
        static void Main(string[] args)
        {
            var studenti = new List<Studente>();
            var storico = new StoricoOperazioni();
            var codaIscrizioni = new CodaIscrizioni();
            var repoStudenti = new RepositoryGenerico<Studente>();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== MENU GESTIONE STUDENTI =====");
                Console.WriteLine("1. Aggiungi studente");
                Console.WriteLine("2. Cerca studente per matricola");
                Console.WriteLine("3. Aggiungi voto a studente");
                Console.WriteLine("4. Visualizza tutti gli studenti");
                Console.WriteLine("5. Trova studente con media più alta (uso Sort)");
                Console.WriteLine("6. Rimuovi ultimo voto da studente");
                Console.WriteLine("7. Registra richiesta iscrizione (Queue)");
                Console.WriteLine("8. Approva prossima iscrizione");
                Console.WriteLine("9. Mostra coda iscrizioni");
                Console.WriteLine("10. Mostra storico operazioni (Stack)");
                Console.WriteLine("11. Usa repository generico: aggiungi studente al repo");
                Console.WriteLine("12. Esci");
                Console.Write("Scelta: ");

                var line = Console.ReadLine();
                if (!int.TryParse(line, out int choice))
                {
                    Console.WriteLine("Scelta non valida.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Write("Nome: "); string nome = Console.ReadLine();
                                Console.Write("Cognome: "); string cognome = Console.ReadLine();
                                Console.Write("Matricola: "); string mat = Console.ReadLine();
                                var s = new Studente(nome, cognome, mat);
                                studenti.Add(s);
                                repoStudenti.Aggiungi(s);
                                storico.Registra($"Aggiunto studente {mat}");
                                Console.WriteLine("Studente aggiunto.");
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Matricola da cercare: "); string mat = Console.ReadLine();
                                var s = studenti.FirstOrDefault(x => x.Matricola == mat);
                                if (s != null) Console.WriteLine(s);
                                else Console.WriteLine("Studente non trovato.");
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Matricola: "); string mat = Console.ReadLine();
                                var s = studenti.FirstOrDefault(x => x.Matricola == mat);
                                if (s == null) { Console.WriteLine("Studente non trovato."); break; }
                                Console.Write("Voto da aggiungere (0-30): ");
                                if (int.TryParse(Console.ReadLine(), out int v))
                                {
                                    s.AggiungiVoto(v);
                                    storico.Registra($"Aggiunto voto {v} a {mat}");
                                    Console.WriteLine("Voto aggiunto.");
                                }
                                else Console.WriteLine("Voto non valido.");
                                break;
                            }
                        case 4:
                            {
                                if (studenti.Count == 0) { Console.WriteLine("Nessuno studente presente."); break; }
                                foreach (var s in studenti) Console.WriteLine(s);
                                break;
                            }
                        case 5:
                            {
                                if (studenti.Count == 0) { Console.WriteLine("Nessuno studente presente."); break; }
                                // usa IComparable
                                var copia = new List<Studente>(studenti);
                                copia.Sort(); // usa CompareTo
                                Console.WriteLine("Studenti ordinati (media decrescente):");
                                foreach (var s in copia) Console.WriteLine(s);
                                break;
                            }
                        case 6:
                            {
                                Console.Write("Matricola: "); string mat = Console.ReadLine();
                                var s = studenti.FirstOrDefault(x => x.Matricola == mat);
                                if (s == null) { Console.WriteLine("Studente non trovato."); break; }
                                try
                                {
                                    s.RimuoviUltimoVoto();
                                    storico.Registra($"Rimosso ultimo voto a {mat}");
                                    Console.WriteLine("Ultimo voto rimosso.");
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                                break;
                            }
                        case 7:
                            {
                                Console.Write("Matricola da inserire in coda: "); string mat = Console.ReadLine();
                                var s = studenti.FirstOrDefault(x => x.Matricola == mat);
                                if (s == null) { Console.WriteLine("Studente non trovato."); break; }
                                codaIscrizioni.AggiungiRichiesta(s);
                                storico.Registra($"Richiesta iscrizione aggiunta per {mat}");
                                Console.WriteLine("Richiesta aggiunta alla coda.");
                                break;
                            }
                        case 8:
                            {
                                var aprovato = codaIscrizioni.ApprovaProssima();
                                if (aprovato == null) Console.WriteLine("Nessuna richiesta in coda.");
                                else
                                {
                                    storico.Registra($"Approvatа iscrizione per {aprovato.Matricola}");
                                    Console.WriteLine($"Iscrizione approvata per: {aprovato}");
                                }
                                break;
                            }
                        case 9:
                            {
                                var lista = codaIscrizioni.OttieniRichiesteInAttesa();
                                if (lista.Count == 0) Console.WriteLine("Coda vuota.");
                                else
                                {
                                    Console.WriteLine("Richieste in attesa:");
                                    foreach (var s in lista) Console.WriteLine(s);
                                }
                                break;
                            }
                        case 10:
                            {
                                var all = storico.OttieniTutte();
                                if (all.Count == 0) Console.WriteLine("Nessuna operazione registrata.");
                                else
                                {
                                    Console.WriteLine("Storico operazioni (ultima in cima):");
                                    foreach (var item in all) Console.WriteLine(item);
                                    Console.WriteLine($"Totale operazioni: {storico.Count}");
                                }
                                break;
                            }
                        case 11:
                            {
                                Console.WriteLine("Repo: elenco studenti in repository:");
                                var all = repoStudenti.GetAll();
                                foreach (var s in all) Console.WriteLine(s);
                                break;
                            }
                        case 12:
                            exit = true;
                            Console.WriteLine("Uscita...");
                            break;
                        default:
                            Console.WriteLine("Scelta non valida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }
    }
}
