using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class Guitarra : Instrumento
    {
        public string Tipo { get; set; }               // acústica, eléctrica, etc.
        public string MaterialCuerpo { get; set; }
        public List<Funda> Fundas { get; set; } = new List<Funda>();
    }

    public class Funda
    {
        public long Codigo { get; set; }
        public string? Nombre { get; set; }
        public double Precio { get; set; }
        public long Codigo_Guitarra { get; set; }
    }
}
