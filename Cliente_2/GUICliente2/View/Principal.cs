using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listarInstrumentos_Click(object sender, EventArgs e)
        {
            ListarInstrumentos gui = new ListarInstrumentos();
            gui.Show();
        }

        private void listarGuitarra_Click(object sender, EventArgs e)
        {
            ListarGuitarra gui = new ListarGuitarra();
            gui.Show();
        }

        private void listarTeclado_Click(object sender, EventArgs e)
        {
            ListarTeclado gui = new ListarTeclado();
            gui.Show();
        }

        private void buscarGuitarra_Click(object sender, EventArgs e)
        {
            BuscarGuitarra gui = new BuscarGuitarra();
            gui.Show();
        }

        private void agregarGuitarra_Click(object sender, EventArgs e)
        {
            AgregarGuitarra gui = new AgregarGuitarra();
            gui.Show();
        }

        private void agregarFunda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La función solicitada aún no está implementada. Por favor, intente nuevamente más tarde.",
                "Función no implementada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void actualizarFunda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La función solicitada aún no está implementada. Por favor, intente nuevamente más tarde.",
                "Función no implementada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void eliminarFunda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La función solicitada aún no está implementada. Por favor, intente nuevamente más tarde.",
                "Función no implementada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void actualizarGuitarra_Click(object sender, EventArgs e)
        {
            ActualizarGuitarra gui = new ActualizarGuitarra();
            gui.Show();
        }

        private void eliminarGuitarra_Click(object sender, EventArgs e)
        {
            EliminarGuitarra gui = new EliminarGuitarra();
            gui.Show();
        }

        private void agregarTeclado_Click(object sender, EventArgs e)
        {
            AgregarTeclado gui = new AgregarTeclado();
            gui.Show();
        }

        private void buscarTeclado_Click(object sender, EventArgs e)
        {
            BuscarTeclado gui = new BuscarTeclado();
            gui.Show();
        }

        private void actualizartTeclado_Click(object sender, EventArgs e)
        {
            ActualizarTeclado gui = new ActualizarTeclado();
            gui.Show();
        }

        private void eliminarTeclado_Click(object sender, EventArgs e)
        {
            EliminarTeclado gui = new EliminarTeclado();
            gui.Show();
        }

        private void guardarPreset_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La función solicitada aún no está implementada. Por favor, intente nuevamente más tarde.",
                "Función no implementada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void cargarPreset_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La función solicitada aún no está implementada. Por favor, intente nuevamente más tarde.",
                "Función no implementada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void acercaDe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollado por:\nGerman Castellanos\nDavid Orjuela\nJorge Rodriguez",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void instrumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
