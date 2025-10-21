using GUICliente2.Model;
using GUICliente2.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class AgregarTeclado : Form
    {
        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";

        public AgregarTeclado()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textCodigo.Text) ||
                string.IsNullOrWhiteSpace(textNombre.Text) ||
                string.IsNullOrWhiteSpace(textMarca.Text)  ||
                string.IsNullOrWhiteSpace(textPrecio.Text) ||
                string.IsNullOrWhiteSpace(textStock.Text)  ||
                string.IsNullOrWhiteSpace(textTipo.Text)   ||
                comboBox1.SelectedIndex == -1)

            {
                MessageBox.Show("Por favor, completa los campos obligatorios",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigo = textCodigo.Text.Trim();
            string nombre = textNombre.Text.Trim();
            string marca = textMarca.Text.Trim();
            double precio = double.TryParse(textPrecio.Text, out var p) ? p : 0;
            int stock = int.TryParse(textStock.Text, out var s) ? s : 0;
            int numeroTeclas = int.TryParse(textTipo.Text, out var n) ? n : 0;
            DateTime fechaIngreso = dateCreacion.Value;

            bool digital = false;
            if (rbtnDigi.Checked) digital = true;
            else if (rbtnAna.Checked) digital = false;

            string sensibilidad = comboBox1.SelectedItem.ToString() ?? "Ninguna";

            var teclado = new Teclado
            {
                Type = "teclado",
                Codigo = codigo,
                Nombre = nombre,
                Marca = marca,
                PrecioBase = precio,
                Stock = stock,
                FechaIngreso = fechaIngreso.ToString("yyyy-MM-dd"),
                NumeroTeclas = numeroTeclas,
                Digital = digital,
                Sensibilidad = sensibilidad
            };

            try { 
                bool creado = await _servicio.AgregarInstrumento(teclado);

                if (creado)
                {
                    MessageBox.Show("Teclado agregado correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el teclado.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.Message.Contains("409"))
                {
                    MessageBox.Show("Ya existe un instrumento con ese código.",
                                    "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}",
                                "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al agregar el teclado:\n{ex.Message}",
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
            textTipo.Clear();
            rbtnAna.Checked = false;
            rbtnDigi.Checked = false;
            comboBox1.SelectedIndex = 0;
            dateCreacion.Value = DateTime.Now;
        }
    }
}
