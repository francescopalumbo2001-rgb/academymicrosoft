using System;
using System.Collections.Generic;

namespace RegistroStudenti
{
    public class Studente : IComparable<Studente>, IEntita
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
            this.voti = new List<int>();
        }

        public string Nome => nome;
        public string Cognome => cognome;
        public string Matricola => matricola;

        public string Id => Matricola;

        public IReadOnlyList<int> Voti => voti.AsReadOnly();

        public double Media
        {
            get
            {
                if (voti.Count == 0) return 0;
                double somma = 0;
                foreach (var v in voti) somma += v;
                return somma / voti.Count;
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
        }

        public void StampaLibretto()
        {
            Console.WriteLine($"{Nome} {Cognome} ({Matricola}) - Media: {Media:F2}");
            Console.Write("Voti: ");
            foreach (var v in voti)
                Console.Write(v + " ");
            Console.WriteLine();
        }

        public override string ToString()
        {
            return $"{Nome} {Cognome} - Matricola: {Matricola} - Media: {Media:F2}";
        }

        
        public int CompareTo(Studente other)
        {
            if (other == null) return 1;

            
            int cmp = other.Media.CompareTo(this.Media);
            if (cmp != 0) return cmp;

            
            cmp = this.Cognome.CompareTo(other.Cognome);
            if (cmp != 0) return cmp;

            
            return this.Nome.CompareTo(other.Nome);
        }
    }
}
