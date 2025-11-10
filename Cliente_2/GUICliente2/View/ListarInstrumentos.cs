using GUICliente2.Model;
using GUICliente2.Service;
using GUICliente2.Service.ClienteInstrumentos.Models;
using GUICliente2.View;
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
    public partial class ListarInstrumentos : Form
    {
        
        private readonly ServicioInstrumento _servicio;
        private string url = "http://localhost:8090/";
        private Filtros _ventanaFiltros;


        public ListarInstrumentos()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            try
            {
                var instrumentos = await _servicio.ListarInstrumentos();

                if (instrumentos == null || instrumentos.Count == 0)
                {
                    MessageBox.Show("No se encontraron instrumentos.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                ConfigurarDataGridView(instrumentos);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor:\n{httpEx.Message}", "Error de red",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                MessageBox.Show($"Error al obtener los instrumentos:\n{ex.ToString}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            if (_ventanaFiltros == null || _ventanaFiltros.IsDisposed)
            {
                _ventanaFiltros = new Filtros();

                _ventanaFiltros.OnFiltrosAplicados += async (filtro) =>
                {
                    Console.WriteLine(filtro.Nombre);
                    await AplicarFiltrosAsync(filtro);
                };

                this.FormClosed += (s, args) =>
                {
                    if (_ventanaFiltros != null && !_ventanaFiltros.IsDisposed)
                        _ventanaFiltros.Close();
                };

                _ventanaFiltros.FormClosed += (s, args) => _ventanaFiltros = null;

                _ventanaFiltros.StartPosition = FormStartPosition.CenterParent;
                _ventanaFiltros.Show(this);
            }
            else
            {
                if (_ventanaFiltros.WindowState == FormWindowState.Minimized)
                    _ventanaFiltros.WindowState = FormWindowState.Normal;

                _ventanaFiltros.BringToFront();
                _ventanaFiltros.Focus();
            }
        }

        private async Task AplicarFiltrosAsync(FiltroInstrumentoDTO filtro)
        {
            try
            {
                var instrumentos = await _servicio.FiltrarInstrumentos(filtro);

                if (instrumentos == null || instrumentos.Count == 0)
                {
                    MessageBox.Show("No se encontraron instrumentos con esos filtros.",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                ConfigurarDataGridView(instrumentos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigurarDataGridView(List<Instrumento> instrumentos)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = instrumentos;
            dataGridView1.RowHeadersVisible = false;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();

            if (dataGridView1.Columns.Contains("PrecioBase"))
                dataGridView1.Columns["PrecioBase"].HeaderText = "Precio";

            if (dataGridView1.Columns.Contains("FechaIngreso"))
                dataGridView1.Columns["FechaIngreso"].HeaderText = "Fecha";

            if (dataGridView1.Columns.Contains("Type"))
            {
                var col = dataGridView1.Columns["Type"];
                col.HeaderText = "Instrumento";
                col.DisplayIndex = dataGridView1.Columns.Count - 1;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

    }
}
