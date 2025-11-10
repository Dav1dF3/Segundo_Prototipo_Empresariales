using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class FiltroFundaDTO
    {   public string Nombre { get; set; }
        public double? PrecioMin { get; set; }
        public double? PrecioMax { get; set; }
        public long? codigoGuitarra { get; set; }
    }
}
