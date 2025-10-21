namespace GUICliente2.Service
{
    using GUICliente2.Model.Enums;

    namespace ClienteInstrumentos.Models
    {
        public class FiltroInstrumentoDTO
        {
            // Nombre o modelo parcial a filtrar
            public string? Nombre { get; set; }

            // Marca del instrumento
            public string? Marca { get; set; }

            // Precio mínimo y máximo
            public double? PrecioMin { get; set; }
            public double? PrecioMax { get; set; }

            // Stock mínimo y máximo
            public int? StockMin { get; set; }
            public int? StockMax { get; set; }

            // Tipo de guitarra (enum equivalente al backend)
            public TipoGuitarra? TipoGuitarra { get; set; }

            // Sensibilidad del teclado (enum equivalente al backend)
            public SensibilidadTeclado? Sensibilidad { get; set; }
        }
    }

}