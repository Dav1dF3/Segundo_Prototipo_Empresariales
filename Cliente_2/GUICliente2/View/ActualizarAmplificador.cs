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
    public partial class ActualizarAmplificador : Form
    {
        private readonly ServicioAmplificador _servicio;
        private readonly string url = "http://localhost:8090/";

        public ActualizarAmplificador()
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
                    textFabricacion.Text = amplificador.FechaFabricacion;

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
            textFabricacion.Clear();
        }


        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            string codigoTexto = textCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigoTexto))
            {
                MessageBox.Show("Por favor, ingresa un código para buscar.",
                                "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(codigoTexto, out int codigo))
            {
                MessageBox.Show("El código debe ser un número entero válido.",
                                "Código inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Buscar el amplificador actual para obtener sus datos
                var amplificadorActual = await _servicio.ObtenerAmplificador(codigo);
                if (amplificadorActual == null)
                {
                    MessageBox.Show("No se encontró un amplificador con ese código.",
                                    "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construir el objeto actualizado
                AmplificadorDTO amplificador = new AmplificadorDTO
                {
                    Id = amplificadorActual.Id, // Mantén la ID
                    Marca = textMarca.Text.Trim(),
                    Modelo = textModelo.Text.Trim(),
                    Potencia = double.TryParse(textPotencia.Text.Trim(), out double potencia) ? potencia : amplificadorActual.Potencia,
                    TipoTubo = textTipoTubo.Text.Trim(),
                    FechaFabricacion = textFabricacion.Text.Trim()
                };

                // Enviar la actualización
                bool response = await _servicio.ActualizarAmplificador(codigo, amplificador);
                LimpiarCampos();
                if (response)
                {
                    MessageBox.Show("Amplificador actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el amplificador.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se pudo conectar con el servidor. Verifica la URL o si el backend está en ejecución.",
                                "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al actualizar el amplificador:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}