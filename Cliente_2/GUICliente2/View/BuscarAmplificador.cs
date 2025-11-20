using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUICliente2.Model;
using GUICliente2.Service;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace GUICliente2
{
    public partial class BuscarAmplificador : Form
    {
        private readonly ServicioAmplificador _servicio;
        private readonly string url = "http://localhost:8090/";

        public BuscarAmplificador()
        {
            InitializeComponent();
            _servicio = new ServicioAmplificador(url);
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(textCodigo.Text.Trim(), out int id))
            {
                MessageBox.Show("El código debe ser un número entero válido.",
                                "Error de código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var amplificador = await _servicio.ObtenerAmplificador(id);

                if (amplificador != null)
                {
                    textMarca.Text = amplificador.Marca;
                    textModelo.Text = amplificador.Modelo;
                    textPotencia.Text = amplificador.Potencia.ToString();
                    textTipoTubo.Text = amplificador.TipoTubo;

                    MessageBox.Show("Amplificador encontrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No existe un amplificador con ese código.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el amplificador:\n{ex.Message}",
                                "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void LimpiarCampos()
        {
            textCodigo.Clear();
            textMarca.Clear();
            textModelo.Clear();
            textPotencia.Clear();
            textTipoTubo.Clear();
        }
    }
}