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
    public partial class ActualizarGuitarra : Form
    {
        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";

        public ActualizarGuitarra()
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

                if (instrumento is Guitarra guitarra)
                {
                    textNombre.Text = guitarra.Nombre;
                    textMarca.Text = guitarra.Marca;
                    textPrecio.Text = guitarra.PrecioBase.ToString();
                    textStock.Text = guitarra.Stock.ToString();
                    textCreacion.Text = guitarra.FechaIngreso?.ToString();
                    textTipo.Text = guitarra.Tipo.ToString();
                    textMaterial.Text = guitarra.MaterialCuerpo;
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

        private async void btnActualizar_Click(object sender, EventArgs e)
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
                Guitarra guitarraActual = (Guitarra)instrumento;
                Guitarra guitarra = new Guitarra{
                    Type = "guitarra",
                    Codigo = guitarraActual.Codigo,
                    Nombre = textNombre.Text.Trim(),
                    Marca = textMarca.Text.Trim(),
                    PrecioBase = double.Parse(textPrecio.Text.Trim()),
                    Stock = int.Parse(textStock.Text.Trim()),
                    FechaIngreso = textCreacion.Text.Trim(),
                    Tipo = textTipo.Text.Trim(),
                    MaterialCuerpo = textMaterial.Text.Trim(),
                    Fundas = guitarraActual.Fundas
                };   
                bool response = await _servicio.EditarInstrumento(codigo, guitarra);
                LimpiarCampos();
                if(response)
                {
                    MessageBox.Show("Guitarra actualizada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la guitarra.",
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
                MessageBox.Show($"Ocurrió un error al actualizar la guitarra:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            textNombre.Clear();
            textMarca.Clear();
            textPrecio.Clear();
            textStock.Clear();
            textCreacion.Clear();
            textTipo.Clear();
            textMaterial.Clear();
        }
    }
}
