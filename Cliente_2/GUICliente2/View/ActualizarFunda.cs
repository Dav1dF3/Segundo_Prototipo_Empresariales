using GUICliente2.Model;
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
    public partial class ActualizarFunda : Form
    {

        private readonly ServicioInstrumento servicio;
        private string url = "http://localhost:8090/";

        public ActualizarFunda()
        {
            InitializeComponent();
            servicio = new ServicioInstrumento(url);
        }

        private void AgregarFunda_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            string codigoGuitarra = textCGuitarra.Text.Trim();
            string codigoFunda = textCFunda.Text.Trim();
            string nombre = textNombre.Text.Trim();
            string precioText = textPrecio.Text.Trim();

            if (string.IsNullOrEmpty(codigoGuitarra) ||
                string.IsNullOrEmpty(codigoFunda) ||
                string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(precioText) ||
                !double.TryParse(precioText, out double precio))
            {
                MessageBox.Show("Por favor completa todos los campos correctamente.",
                                "Campos incompletos / inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var funda = new Funda
            {
                Codigo = long.TryParse(codigoFunda, out long codigoFundaLong) ? codigoFundaLong : 0, // Ajusta según tipo
                Nombre = nombre,
                Precio = precio,
                Codigo_Guitarra = long.TryParse(codigoGuitarra, out long codigoGuitarraLong) ? codigoGuitarraLong : 0
            };

            try
            {
                bool actualizado = await servicio.EditarFunda(codigoGuitarra, codigoFunda, funda);

                if (actualizado)
                {
                    MessageBox.Show("Funda actualizada correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la funda.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la funda: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCampos();
            }
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
                    LimpiarCampos();
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
        private void LimpiarCampos()
        {
            textCGuitarra.Clear();
            textCFunda.Clear();
            textNombre.Clear();
            textPrecio.Clear();
        }


        private void textCFunda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
