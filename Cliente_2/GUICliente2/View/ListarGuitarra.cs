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

namespace GUICliente2
{
    public partial class ListarGuitarra : Form
    {
        private readonly ServicioInstrumento _servicio;
        private string url = "http://localhost:8090/";
        public ListarGuitarra()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            btnListar.Enabled = false;
            try
            {
                var guitarras = await _servicio.ListarGuitarras();

                if (guitarras == null || guitarras.Count == 0)
                {
                    MessageBox.Show("No se encontraron guitarras.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }
                ConfigurarDataGridView(guitarras);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}", "Error de red",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las guitarras:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }

        private void ConfigurarDataGridView(List<Guitarra> guitarras)
        {
            dataGridView1.DataSource = null;

            var datos = guitarras.Select(g => new
            {
                Código = g.Codigo,
                Nombre = g.Nombre,
                Marca = g.Marca,
                Precio = g.PrecioBase,
                Stock = g.Stock,
                FechaCreacion = g.FechaIngreso.ToString(),
                Tipo = g.Tipo.ToString(),
                Material = g.MaterialCuerpo,
                Fundas = (g.Fundas != null && g.Fundas.Any()) ? "Sí" : "No"
            }).ToList();

            dataGridView1.DataSource = datos;
            dataGridView1.RowHeadersVisible = false;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();

             dataGridView1.Columns["FechaCreacion"].HeaderText = "Fecha de creación";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

    }
}
