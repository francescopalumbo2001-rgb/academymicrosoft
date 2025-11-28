using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityManagement
{
    public class Studente : IEntita, IComparable<Studente>
    {
        // campi privati
        private string nome;
        private string cognome;
        private string matricola;
        private List<int> voti;

        // costruttore
        public Studente(string nome, string cognome, string matricola)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.cognome = cognome ?? throw new ArgumentNullException(nameof(cognome));
            this.matricola = matricola ?? throw new ArgumentNullException(nameof(matricola));
            this.voti = new List<int>();
        }

        // proprietà di sola lettura
        public string Nome => nome;
        public string Cognome => cognome;
        public string Matricola => matricola;

        // implementazione IEntita (Id => Matricola)
        public string Id => Matricola;

        // proprietà calcolate read-only
        public double Media
        {
            get
            {
                if (voti.Count == 0) return 0.0;
                return voti.Average();
            }
        }

        public int NumeroVoti => voti.Count;

        // metodi
        public void AggiungiVoto(int voto)
        {
            if (voto < 0 || voto > 30)
                throw new ArgumentOutOfRangeException(nameof(voto), "Il voto deve essere tra 0 e 30.");
            voti.Add(voto);
        }

        public void RimuoviUltimoVoto()
        {
            if (voti.Count == 0) throw new InvalidOperationException("Non ci sono voti da rimuovere.");
            voti.RemoveAt(voti.Count - 1);
        }

        public void StampaLibretto()
        {
            Console.WriteLine($"Libretto di {Nome} {Cognome} - Matricola: {Matricola}");
            if (voti.Count == 0)
            {
                Console.WriteLine("  Nessun voto registrato.");
                return;
            }

            Console.Write("  Voti: ");
            Console.WriteLine(string.Join(", ", voti));
            Console.WriteLine($"  Media: {Media:F2} ({NumeroVoti} voti)");
        }

        public override string ToString()
        {
            return $"{Cognome} {Nome} - Matricola: {Matricola} - Media: {Media:F2} ({NumeroVoti} voti)";
        }

        // =========================
        // IComparable<Studente>
        // Ordinamento richiesto:
        // 1) Media (DECRESCENTE)
        // 2) Cognome (alfabetico)
        // 3) Nome (alfabetico)
        // =========================
        public int CompareTo(Studente other)
        {
            if (other == null) return -1; // questo viene prima
            // media decrescente
            int cmpMedia = other.Media.CompareTo(this.Media); // other vs this => decrescente
            if (cmpMedia != 0) return cmpMedia;

            // cognome alfabetico (crescente)
            int cmpCognome = string.Compare(this.Cognome, other.Cognome, StringComparison.OrdinalIgnoreCase);
            if (cmpCognome != 0) return cmpCognome;

            // nome alfabetico (crescente)
            return string.Compare(this.Nome, other.Nome, StringComparison.OrdinalIgnoreCase);
        }
    }
}
