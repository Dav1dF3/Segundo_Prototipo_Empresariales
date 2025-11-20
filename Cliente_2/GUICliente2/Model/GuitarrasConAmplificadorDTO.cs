using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class GuitarrasConAmplificadorDTO
    {
        
        public string? MarcaGuitarra { get; set; }

        
        public string? MaterialCuerpoGuitarra { get; set; }

        
        public List<AmplificadorDTO>? Amplificadores { get; set; }
    }
}
