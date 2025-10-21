using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class Instrumento
    {
        public string Type { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public double PrecioBase { get; set; }
        public int Stock { get; set; }
        public string FechaIngreso { get; set; }
    }
}
