namespace Layouts_K2
{
    public partial class Bienvenida : Form
    {
        public Bienvenida()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm1 = new Altas_int();
            frm1.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNom_Click(object sender, EventArgs e)
        {
            Form frm1 = new Nominas();
            frm1.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Bienvenida_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Bienvenida_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Verificamos si el cierre fue iniciado por el usuario (X, Alt+F4, etc.)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea salir del programa?",
                    "Confirmar Salida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    // Detenemos el cierre
                    e.Cancel = true;
                }
            }
        }

        private void btnPmx_Click(object sender, EventArgs e)
        {
            Form frm1 = new Pagos_mixtos();
            frm1.Show();
            this.Hide();
        }
    }
}
