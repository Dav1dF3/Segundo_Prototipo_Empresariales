using GUICliente2.Model;
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
    public partial class FiltrosFunda : Form
    {
        public event Action<FiltroFundaDTO> OnFiltrosAplicados;
        public FiltrosFunda()
        {
            InitializeComponent();
            numPrecioMin.Minimum = 0;
            numPrecioMin.Maximum = decimal.MaxValue;

            numPrecioMax.Minimum = 0;
            numPrecioMax.Maximum = decimal.MaxValue;
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            var filtro = new FiltroFundaDTO
            {
                Nombre = cbNombre.Checked ? textNombre.Text.Trim() : null,
                PrecioMin = cbPrecioMin.Checked ? (double?)numPrecioMin.Value : null,
                PrecioMax = cbPrecioMax.Checked ? (double?)numPrecioMax.Value : null,
                codigoGuitarra = cbCGuitarra.Checked && long.TryParse(textCGuitarra.Text.Trim(), out long codigoGuitarra) ? codigoGuitarra : (long?)null,
            };

            OnFiltrosAplicados?.Invoke(filtro);
        }
        private void cbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroTexto(cbCGuitarra, textCGuitarra);
        }

        private void cbNombre_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroTexto(cbNombre, textNombre);
        }

        private void cbPrecioMin_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbPrecioMin, numPrecioMin);
        }

        private void cbPrecioMax_CheckedChanged(object sender, EventArgs e)
        {
            hablitarFiltroNumero(cbPrecioMax, numPrecioMax);
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
