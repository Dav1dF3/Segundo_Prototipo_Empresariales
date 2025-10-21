using GUICliente2.Model;
using GUICliente2.Service;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUICliente2
{
    public partial class AgregarGuitarra : Form
    {

        private readonly ServicioInstrumento _servicio;
        private readonly string url = "http://localhost:8090/";
        private AgregarFundaGuitarraNueva gui;
        private List<Funda> fundasSeleccionadas = new List<Funda>();
        public AgregarGuitarra()
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
            if (string.IsNullOrWhiteSpace(textCodigo.Text)   ||
                string.IsNullOrWhiteSpace(textNombre.Text)   ||
                string.IsNullOrWhiteSpace(textMarca.Text)    ||
                string.IsNullOrWhiteSpace(textPrecio.Text)   ||
                string.IsNullOrWhiteSpace(textStock.Text)    ||
                string.IsNullOrWhiteSpace(textMaterial.Text) ||
                comboBoxTipo.SelectedIndex == -1)

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
            DateTime fechaIngreso = dateCreacion.Value;
            string tipo = comboBoxTipo.SelectedItem.ToString();
            string material = textMaterial.Text.Trim();



            var guitarra = new Guitarra
            {
                Type = "guitarra",
                Codigo = codigo,
                Nombre = nombre,
                Marca = marca,
                PrecioBase = precio,
                Stock = stock,
                FechaIngreso = fechaIngreso.ToString("yyyy-MM-dd"),
                Tipo = tipo,
                MaterialCuerpo = material,
                Fundas = fundasSeleccionadas
            };
            try
            {
                bool creado = await _servicio.AgregarInstrumento(guitarra);

                if (creado)
                {
                    MessageBox.Show("Guitarra agregada correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fundasSeleccionadas.Clear();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la guitarra.",
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
           
                    MessageBox.Show($"Error al agregar la guitarra:\n{ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }


        private void rbtnSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSi.Checked)
            {
                if (!string.IsNullOrWhiteSpace(textCodigo.Text)) 
                {
                    if (gui != null && !gui.IsDisposed)
                        gui.Close();
                    gui = new AgregarFundaGuitarraNueva();
                    gui.setTxtCodigo(textCodigo.Text.Trim());
                    gui.FundasActualizadas += (listaActualizada) =>
                    {
                        fundasSeleccionadas = new List<Funda>(listaActualizada);
                        MessageBox.Show($"Se han agregado {fundasSeleccionadas.Count} fundas.",
                                        "Fundas actualizadas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    gui.Show(); 
                }
                else
                {
                    MessageBox.Show("Por favor, escribe el codigo de la guitarra primero",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            gui = new AgregarFundaGuitarraNueva();
            gui.Close();
        }



        private void LimpiarCampos()
        {
            textCodigo.Clear();
            textNombre.Clear();
            textMarca.Clear();
            textPrecio.Clear();
            textStock.Clear();
            textMaterial.Clear();
            dateCreacion.Value = DateTime.Now;
            comboBoxTipo.SelectedIndex = -1;
            rbtnSi.Checked = false;
            rbtnNo.Checked = false;
        }
    }
}
