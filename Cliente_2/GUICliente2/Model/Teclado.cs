using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class Teclado : Instrumento
    {
        public int NumeroTeclas { get; set; }
        public bool Digital { get; set; }
        public string Sensibilidad { get; set; }
    }
}
