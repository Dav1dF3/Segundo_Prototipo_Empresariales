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
    public partial class ActualizarTeclado : Form
    {
        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";

        public ActualizarTeclado()
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
                    textMaterial.Text = teclado.Sensibilidad; // Sensibilidad se muestra en 'Material'
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

                if (!(instrumento is Teclado tecladoActual))
                {
                    MessageBox.Show("No se puede actualizar: este código no corresponde a un teclado.",
                                    "Tipo incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarCampos();
                    return;
                }

                Teclado teclado = new Teclado
                {
                    Type = "teclado",
                    Codigo = tecladoActual.Codigo,
                    Nombre = textNombre.Text.Trim(),
                    Marca = textMarca.Text.Trim(),
                    PrecioBase = double.Parse(textPrecio.Text.Trim()),
                    Stock = int.Parse(textStock.Text.Trim()),
                    FechaIngreso = tecladoActual.FechaIngreso,
                    NumeroTeclas = int.Parse(textTipo.Text.Trim()),
                    Digital = ObtenerValorDigital(textDigiAna.Text.Trim()),
                    Sensibilidad = textMaterial.Text.Trim()
                };

                bool response = await _servicio.EditarInstrumento(codigo, teclado);
                LimpiarCampos();
                if (response)
                {
                    MessageBox.Show("Teclado actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el teclado.",
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
                MessageBox.Show($"Ocurrió un error al actualizar el teclado:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ObtenerValorDigital(string valor)
        {
            return valor.Equals("digital", StringComparison.OrdinalIgnoreCase) || valor.Equals("true", StringComparison.OrdinalIgnoreCase);
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
            textDigiAna.Clear();
        }
    }
}
