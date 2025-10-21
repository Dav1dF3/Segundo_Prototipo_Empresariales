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
    public partial class BuscarGuitarra : Form
    {
        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";

        public BuscarGuitarra()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void LimpiarCampos()
        {
            textNombre.Clear();
            textMarca.Clear();
            textPrecio.Clear();
            textStock.Clear();
            textFechaCreacion.Clear();
            textTipo.Clear();
            textMaterial.Clear();
            textFunda.Clear();
        }

        private async void btnBuscar_Click_1(object sender, EventArgs e)
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

                if (instrumento is Guitarra guitarra)
                {
                    textNombre.Text = guitarra.Nombre;
                    textMarca.Text = guitarra.Marca;
                    textPrecio.Text = guitarra.PrecioBase.ToString();
                    textStock.Text = guitarra.Stock.ToString();
                    textFechaCreacion.Text = guitarra.FechaIngreso?.ToString();
                    textTipo.Text = guitarra.Tipo.ToString();
                    textMaterial.Text = guitarra.MaterialCuerpo;
                    textFunda.Text = string.Join(", ", guitarra.Fundas.Select(f => f.Nombre));
                }
                else
                {
                    MessageBox.Show("El código ingresado no pertenece a una guitarra.",
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
                MessageBox.Show($"Ocurrió un error al buscar el teclado:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
