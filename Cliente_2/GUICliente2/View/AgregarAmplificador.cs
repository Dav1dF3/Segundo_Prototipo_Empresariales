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
    public partial class AgregarAmplificador : Form
    {
        private readonly ServicioAmplificador _servicio;
        private readonly string url = "http://localhost:8090/";

        public AgregarAmplificador()
        {
            InitializeComponent();
            _servicio = new ServicioAmplificador(url);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(textCodigo.Text) ||
                string.IsNullOrWhiteSpace(textMarca.Text) ||
                string.IsNullOrWhiteSpace(textModelo.Text) ||
                string.IsNullOrWhiteSpace(textPotencia.Text) ||
                string.IsNullOrWhiteSpace(textTipoTubo.Text))
            {
                MessageBox.Show("Completa todos los campos obligatorios.", "Campos incompletos",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textCodigo.Text.Trim(), out int id))
            {
                MessageBox.Show("El ID/código debe ser un número entero válido.",
                                "Error de ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(textPotencia.Text.Trim(), out double potencia))
            {
                MessageBox.Show("La potencia debe ser un número válido.",
                                "Error de potencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fechaFabricacion = dateFabricacion.Value.ToString("yyyy-MM-dd");

            var amplificador = new AmplificadorDTO
            {
                Id = id,
                Marca = textMarca.Text.Trim(),
                Modelo = textModelo.Text.Trim(),
                Potencia = potencia,
                TipoTubo = textTipoTubo.Text.Trim(),
                FechaFabricacion = fechaFabricacion
            };

            try
            {
                var respuesta = await _servicio.CrearAmplificador(amplificador);
                if (respuesta != null)
                {
                    MessageBox.Show("Amplificador agregado correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el amplificador.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("409"))
                {
                    MessageBox.Show("Ya existe un amplificador con este ID/código. Por favor ingresa uno diferente.",
                                    "ID duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Error de conexión con el servidor:\n{ex.Message}",
                                "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el amplificador:\n{ex.Message}",
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
            dateFabricacion.Value = DateTime.Now;
        }
    }
}