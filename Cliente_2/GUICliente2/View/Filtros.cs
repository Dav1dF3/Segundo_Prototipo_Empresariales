using GUICliente2.Service.ClienteInstrumentos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2.View
{
    public partial class Filtros : Form
    {
        public event Action<FiltroInstrumentoDTO> OnFiltrosAplicados;
        public Filtros()
        {
            InitializeComponent();
            numPrecioMin.Minimum = 0;
            numPrecioMin.Maximum = decimal.MaxValue;

            numPrecioMax.Minimum = 0;
            numPrecioMax.Maximum = decimal.MaxValue;

            numStockMin.Minimum = 0;
            numStockMin.Maximum = decimal.MaxValue;

            numStockMax.Minimum = 0;
            numStockMax.Maximum = decimal.MaxValue;
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            var filtro = new FiltroInstrumentoDTO
            {
                Nombre = cbNombre.Checked ? textNombre.Text.Trim() : null,
                Marca = cbMarca.Checked ? textMarca.Text.Trim() : null,
                PrecioMin = cbPrecioMin.Checked ? (double?)numPrecioMin.Value : null,
                PrecioMax = cbPrecioMax.Checked ? (double?)numPrecioMax.Value : null,
                StockMin = cbStockMin.Checked ? (int?)numStockMin.Value : null,
                StockMax = cbStockMax.Checked ? (int?)numStockMax.Value : null,

                TipoGuitarra = null,
                Sensibilidad = null
            };
            OnFiltrosAplicados?.Invoke(filtro);
        }

        private void cbNombre_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroTexto(cbNombre, textNombre);
        }


        private void cbMarca_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroTexto(cbMarca, textMarca);
        }

        private void cbPrecioMin_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbPrecioMin, numPrecioMin);
        }

        private void cbPrecioMax_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbPrecioMax, numPrecioMax);
        }

        private void cbStockMin_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbStockMin, numStockMin);
        }

        private void cbStockMax_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbStockMax, numStockMax);
        }

        private void hablitarFiltroTexto(CheckBox cbFiltro, TextBox txtFiltro)
        {
            if (!cbFiltro.Checked)
            {
                txtFiltro.Enabled = false;
            }
            else
            {
                txtFiltro.Enabled = true;
            }
        }

        private void hablitarFiltroNumero(CheckBox cbFiltro, NumericUpDown numFiltro)
        {
            if (!cbFiltro.Checked)
            {
                numFiltro.Enabled = false;
            }
            else
            {
                numFiltro.Enabled = true;
            }
        }
    }
}
