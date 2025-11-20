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
using GUICliente2.Service.ClienteInstrumentos.Models;
using GUICliente2.View;

namespace GUICliente2
{
    public partial class ListarAmplificador : Form
    {
        private readonly ServicioAmplificador _servicio;
        private string url = "http://localhost:8090/";

        public ListarAmplificador()
        {
            InitializeComponent();
            _servicio = new ServicioAmplificador(url);
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            btnListar.Enabled = false;
            try
            {
                var amplificadores = await _servicio.ListarAmplificadores();

                if (amplificadores == null || amplificadores.Count == 0)
                {
                    MessageBox.Show("No se encontraron amplificadores.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }
                ConfigurarDataGridView(amplificadores);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}", "Error de red",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los amplificadores:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }

        private void ConfigurarDataGridView(List<AmplificadorDTO> amplificadores)
        {
            dataGridView1.DataSource = null;

            var datos = amplificadores.Select(a => new
            {
                Código = a.Id,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Potencia = a.Potencia,
                TipoTubo = a.TipoTubo,
                FechaFabricacion = a.FechaFabricacion
            }).ToList();

            dataGridView1.DataSource = datos;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();

            dataGridView1.Columns["FechaFabricacion"].HeaderText = "Fecha de fabricación";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }



        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
