using GUICliente2.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class BuscarFunda : Form
    {
        private readonly ServicioInstrumento servicio;
        private string url = "http://localhost:8090/";
        public BuscarFunda()
        {
            InitializeComponent();
            servicio = new ServicioInstrumento(url);
        }

        private void AgregarFunda_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigoGuitarra = textCGuitarra.Text.Trim();
            string codigoFunda = textCFunda.Text.Trim();

            if (string.IsNullOrEmpty(codigoGuitarra) || string.IsNullOrEmpty(codigoFunda))
            {
                MessageBox.Show("Por favor, ingresa ambos códigos (guitarra y funda).",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var funda = await servicio.buscarFunda(codigoGuitarra, codigoFunda);

                if (funda == null)
                {
                    MessageBox.Show("No se encontró la funda especificada.", "Sin resultados",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textNombre.Text = "";
                    textPrecio.Text = "";
                    return;
                }

                textNombre.Text = funda.Nombre;
                textPrecio.Text = funda.Precio.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar la funda: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
