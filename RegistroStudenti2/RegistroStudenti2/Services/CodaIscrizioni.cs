using System.Collections.Generic;

namespace RegistroStudenti
{
    public class CodaIscrizioni
    {
        private Queue<Studente> coda = new Queue<Studente>();

        public int NumeroRichieste => coda.Count;

        public void AggiungiRichiesta(Studente s)
        {
            coda.Enqueue(s);
        }

        public Studente ApprovaProssima()
        {
            return coda.Count > 0 ? coda.Dequeue() : null;
        }

        public Studente ProssimoInCoda()
        {
            return coda.Count > 0 ? coda.Peek() : null;
        }

        public List<Studente> OttieniRichiesteInAttesa()
        {
            return new List<Studente>(coda);
        }
    }
}