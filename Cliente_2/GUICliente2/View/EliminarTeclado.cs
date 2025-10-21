using GUICliente2.Model;
using GUICliente2.Service;
using System;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class EliminarTeclado : Form
    {
        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";

        public EliminarTeclado()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = textCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código para buscar.",
                                "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var instrumento = await _servicio.BuscarInstrumento(codigo);

                if (instrumento == null)
                {
                    MessageBox.Show("No se encontró ningún instrumento con ese código.",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    return;
                }

                if (instrumento is Teclado teclado)
                {
                    textNombre.Text = teclado.Nombre;
                    textMarca.Text = teclado.Marca;
                    textPrecio.Text = teclado.PrecioBase.ToString();
                    textStock.Text = teclado.Stock.ToString();
                    textCreacion.Text = teclado.FechaIngreso?.ToString();
                    textTipo.Text = teclado.NumeroTeclas.ToString();
                    textMaterial.Text = teclado.Sensibilidad;
                    textDigiAna.Text = teclado.Digital ? "digital" : "analógico";
                }
                else
                {
                    MessageBox.Show("El código ingresado no pertenece a un teclado.",
                                    "Tipo incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se pudo conectar con el servidor. Verifica la URL o si el backend está en ejecución.",
                                "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al buscar el instrumento:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigo = textCodigo.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, ingrese un código para buscar.",
                                "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este teclado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool response = await _servicio.EliminarInstrumento(codigo);
                if (response)
                {
                    LimpiarCampos();
                    MessageBox.Show("Teclado eliminado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el teclado.",
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
                MessageBox.Show($"Ocurrió un error al eliminar el teclado:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            textCodigo.Clear();
            textNombre.Clear();
            textMarca.Clear();
            textPrecio.Clear();
            textStock.Clear();
            textCreacion.Clear();
            textTipo.Clear();
            textMaterial.Clear();
            textDigiAna.Clear();
        }
    }
}
