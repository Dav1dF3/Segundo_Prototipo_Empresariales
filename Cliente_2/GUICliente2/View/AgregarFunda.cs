using GUICliente2.Model;
using GUICliente2.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class AgregarFunda : Form
    {

        private readonly ServicioInstrumento _servicio;
        private string url = "http://localhost:8090/";

        public AgregarFunda()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void AgregarFunda_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textCFunda.Text) ||
                string.IsNullOrWhiteSpace(textNombre.Text) ||
                !double.TryParse(textPrecio.Text, out double precio) ||
                !long.TryParse(textCFunda.Text, out long codigoFunda))
            {
                MessageBox.Show("Por favor completa todos los campos correctamente.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nueva = new Funda
            {
                Codigo = codigoFunda,
                Nombre = textNombre.Text.Trim(),
                Precio = precio
            };

            var fundas = new List<Funda> { nueva };

            try
            {
                bool success = await _servicio.AgregarFundas(textCGuitarra.Text.Trim(), fundas);

                if (success)
                {
                    MessageBox.Show("Funda agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textNombre.Clear();
                    textPrecio.Clear();
                    textCFunda.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la funda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.Message.Contains("409"))
                {
                    MessageBox.Show("Ya existe una funda con ese código.",
                                    "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}",
                                "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar funda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
