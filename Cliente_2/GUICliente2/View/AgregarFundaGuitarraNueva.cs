using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GUICliente2.Model;

namespace GUICliente2
{
    public partial class AgregarFundaGuitarraNueva : Form
    {
        // Lista interna que se acumula con cada nueva funda
        private List<Funda> fundasCreadas = new List<Funda>();

        // 🔹 Evento para notificar al formulario de guitarra
        public event Action<List<Funda>> FundasActualizadas;

        public AgregarFundaGuitarraNueva()
        {
            InitializeComponent();
            textCGuitarra.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void setTxtCodigo(string codigo)
        {
            textCGuitarra.Text = codigo;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textCFunda.Text) ||
                string.IsNullOrWhiteSpace(textNombre.Text) ||
                !double.TryParse(textPrecio.Text, out double precio))
            {
                MessageBox.Show("Por favor completa todos los campos correctamente.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nueva = new Funda
            {
                Codigo = textCFunda.Text.Trim(),
                Nombre = textNombre.Text.Trim(),
                Precio = precio
            };

            fundasCreadas.Add(nueva);

            FundasActualizadas?.Invoke(fundasCreadas);

            textNombre.Clear();
            textPrecio.Clear();
        }
    }
}
