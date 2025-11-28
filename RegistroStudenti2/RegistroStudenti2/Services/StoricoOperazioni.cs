using System.Collections.Generic;

namespace RegistroStudenti
{
    public class StoricoOperazioni
    {
        private Stack<string> storico = new Stack<string>();

        public int Count => storico.Count;

        public void Registra(string operazione)
        {
            storico.Push(operazione);
        }

        public string UltimaOperazione()
        {
            return storico.Count > 0 ? storico.Peek() : null;
        }

        public string Annulla()
        {
            return storico.Count > 0 ? storico.Pop() : null;
        }

        public List<string> OttieniTutte()
        {
            return new List<string>(storico);
        }
    }
}