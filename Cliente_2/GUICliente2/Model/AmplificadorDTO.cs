using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GUICliente2.Model
{
    public class AmplificadorDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("marca")]
        public string Marca { get; set; }

        [JsonPropertyName("modelo")]
        public string Modelo { get; set; }

        [JsonPropertyName("potencia")]
        public double Potencia { get; set; }

        [JsonPropertyName("tipo_tubo")]
        public string TipoTubo { get; set; }

        [JsonPropertyName("fecha_fabricacion")]
        public string FechaFabricacion { get; set; }
    }
}
