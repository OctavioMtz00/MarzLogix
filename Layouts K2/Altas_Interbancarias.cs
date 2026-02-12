using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Altas_Interbancarias : Form
    {
        public Altas_Interbancarias()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                   "¿Está seguro de que desea salir?",
                   "Confirmar Salida",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
                this.Close();
            Form frm1 = new Bienvenida();
            frm1.Show();
        }
    }
}
