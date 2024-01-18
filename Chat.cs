using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServidor
{
    public class Chat
    {
        public List<string> missatges = new List<string>();
        public void clear()
        {
            missatges.Clear();
        }
        public void afegirMissatge(string missatge)
        {
            missatges.Add(missatge);
        }
        public string obtenirMissatges()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var missatge in missatges)
            {
                sb.AppendLine(missatge);
            }
            return sb.ToString();
        }
    }
}
