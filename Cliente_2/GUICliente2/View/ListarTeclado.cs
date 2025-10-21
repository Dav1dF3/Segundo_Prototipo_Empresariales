using GUICliente2.Model;
using GUICliente2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class ListarTeclado : Form
    {
        private readonly ServicioInstrumento _servicio;
        private string url = "http://localhost:8090/";

        public ListarTeclado()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

      
        private void ConfigurarDataGridView(List<Teclado> teclados)
        {
            dataGridView1.DataSource = null;

            var datos = teclados.Select(t => new
            {
                Código = t.Codigo,
                Nombre = t.Nombre,
                Marca = t.Marca,
                Precio = t.PrecioBase,
                Stock = t.Stock,
                FechaCreacion = t.FechaIngreso.ToString(),
                NTeclas = t.NumeroTeclas,
                Digital = t.Digital ? "Sí" : "No",
                Sensibilidad = t.Sensibilidad.ToString()
            }).ToList();

            dataGridView1.DataSource = datos;

            // 🔹 Configuración visual
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns["FechaCreacion"].HeaderText = "Fecha de creación";
            dataGridView1.Columns["NTeclas"].HeaderText = "N Teclas";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private async void btnListar_Click_1(object sender, EventArgs e)
        {
            btnListar.Enabled = false;
            try
            {
                var teclados = await _servicio.ListarTeclados();

                if (teclados == null || teclados.Count == 0)
                {
                    MessageBox.Show("No se encontraron teclados.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                ConfigurarDataGridView(teclados);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}", "Error de red",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los teclados:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }
    }
}
