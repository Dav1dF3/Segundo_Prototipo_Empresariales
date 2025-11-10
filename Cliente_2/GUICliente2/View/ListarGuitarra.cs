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
    public partial class ListarGuitarra : Form
    {
        private readonly ServicioInstrumento _servicio;
        private string url = "http://localhost:8090/";
        private Filtros ventanaFiltros;
        public ListarGuitarra()
        {
            InitializeComponent();
            _servicio = new ServicioInstrumento(url);
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
                FundasNombres = g.Fundas != null && g.Fundas.Any() ? string.Join(", ", g.Fundas.Select(f => f.Nombre)) : "",
                FundasCodigos = g.Fundas != null && g.Fundas.Any() ? string.Join(", ", g.Fundas.Select(f => f.Codigo)) : ""
            }).ToList();

            dataGridView1.DataSource = datos;
            dataGridView1.RowHeadersVisible = false;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();

            dataGridView1.Columns["FechaCreacion"].HeaderText = "Fecha de creación";
            dataGridView1.Columns["FundasNombres"].HeaderText = "Nombre Fundas";
            dataGridView1.Columns["FundasCodigos"].HeaderText = "Código Fundas";


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }


        private void btnFiltros_Click(object sender, EventArgs e)
        {
            if (ventanaFiltros == null || ventanaFiltros.IsDisposed)
            {
                ventanaFiltros = new Filtros();
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

        private async Task AplicarFiltrosAsync(FiltroInstrumentoDTO filtro)
        {
            btnListar.Enabled = false;
            try
            {
                // Llamas al servicio que filtra directo en backend
                var guitarras = await _servicio.FiltrarGuitarras(filtro);


                if (guitarras == null || guitarras.Count == 0)
                {
                    MessageBox.Show("No se encontraron guitarras con esos filtros.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }
                ConfigurarDataGridView(guitarras);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
