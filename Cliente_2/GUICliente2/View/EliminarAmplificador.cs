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
    public partial class EliminarAmplificador : Form
    {
        private readonly ServicioAmplificador _servicio;
        private readonly string url = "http://localhost:8090/";

        public EliminarAmplificador()
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


        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigoTexto = textCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigoTexto))
            {
                MessageBox.Show("Por favor, ingresa el código del amplificador a eliminar.",
                                "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(codigoTexto, out int id))
            {
                MessageBox.Show("El código debe ser un número entero válido.",
                                "Código inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación antes de eliminar
            var confirm = MessageBox.Show($"¿Seguro que deseas eliminar el amplificador con código {id}?",
                                         "Confirmar eliminación",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                bool response = await _servicio.EliminarAmplificador(id);
                LimpiarCampos();
                if (response)
                {
                    MessageBox.Show("Amplificador eliminado correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el amplificador (puede que no exista o hubo un error).",
                                    "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se pudo conectar con el servidor. Verifica la URL o si el backend está en ejecución.",
                                "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al eliminar el amplificador:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}