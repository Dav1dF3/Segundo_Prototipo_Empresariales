using GUICliente2.Model;
using GUICliente2.Service;
using GUICliente2.Service.ClienteInstrumentos.Models;
using GUICliente2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class ListarFunda : Form
    {
        private readonly ServicioInstrumento servicio;
        private string url = "http://localhost:8090";
        private FiltrosFunda ventanaFiltros;

        public ListarFunda()
        {
            InitializeComponent();
            servicio = new ServicioInstrumento(url);
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            btnListar.Enabled = false;
            try
            {
                var instrumentos = await servicio.ListarInstrumentos();
                var guitarras = instrumentos.OfType<Guitarra>().ToList();
                if (guitarras == null || guitarras.Count == 0)
                {
                    MessageBox.Show("No se encontraron guitarras.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                // Extraer todas las fundas de todas las guitarras
                var fundas = guitarras.SelectMany(g => g.Fundas).ToList();

                if (fundas.Count == 0)
                {
                    MessageBox.Show("No se encontraron fundas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                ConfigurarDataGridView(fundas);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor: {httpEx.Message}", "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las guitarras/fundas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }

        private void ConfigurarDataGridView(List<Funda> fundas)
        {
            dataGridView1.DataSource = null;
            var datos = fundas.Select(f => new
            {
                Codigo = f.Codigo,
                Codigo_guitarra = f.Codigo_Guitarra,
                Nombre = f.Nombre,
                Precio = f.Precio
            }).ToList();

            dataGridView1.DataSource = datos;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            if (ventanaFiltros == null || ventanaFiltros.IsDisposed)
            {
                ventanaFiltros = new FiltrosFunda();
                ventanaFiltros.OnFiltrosAplicados += async filtro => await AplicarFiltrosAsync(filtro);
                ventanaFiltros.FormClosed += (s, args) => ventanaFiltros = null;
                ventanaFiltros.StartPosition = FormStartPosition.CenterParent;
                ventanaFiltros.Show(this);
            }
            else
            {
                if (ventanaFiltros.WindowState == FormWindowState.Minimized)
                    ventanaFiltros.WindowState = FormWindowState.Normal;
                ventanaFiltros.BringToFront();
                ventanaFiltros.Focus();
            }
        }

        private async Task AplicarFiltrosAsync(FiltroFundaDTO filtro)
        {
            btnListar.Enabled = false;
            try
            {
                var fundasFiltradas = await servicio.FiltrarFundas(filtro);

                if (fundasFiltradas == null || fundasFiltradas.Count == 0)
                {
                    MessageBox.Show("No se encontraron fundas con esos filtros.", "Sin resultados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                ConfigurarDataGridView(fundasFiltradas);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btnListar_Click_1(object sender, EventArgs e)
        {
            btnListar.Enabled = false;

            try
            {
                // Llamada directa al servicio para listar fundas
                var fundas = await servicio.ListarFundas();

                if (fundas == null || fundas.Count == 0)
                {
                    MessageBox.Show("No se encontraron fundas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                // Configurar y mostrar las fundas en la grilla
                ConfigurarDataGridView(fundas);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de conexión con el servidor: {httpEx.Message}", "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las fundas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnListar.Enabled = true;
            }
        }

    }
}
