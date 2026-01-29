namespace Layouts_K2
{
    partial class Nominas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nominas));
            dataGridView1 = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            CUENTA = new DataGridViewTextBoxColumn();
            IMPORTE = new DataGridViewTextBoxColumn();
            btnLONOM = new Button();
            btnCNOM = new Button();
            btnSalir = new Button();
            lblContador = new Label();
            lblSumaImportes = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(192, 255, 192);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = SystemColors.ControlLight;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.Menu;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, CUENTA, IMPORTE });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle7.SelectionBackColor = Color.Lavender;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridView1.Size = new Size(710, 601);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellValidated += dataGridView1_CellValidated;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Nombre
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Nombre.DefaultCellStyle = dataGridViewCellStyle3;
            Nombre.HeaderText = "NOMBRE";
            Nombre.MaxInputLength = 40;
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Width = 350;
            // 
            // CUENTA
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CUENTA.DefaultCellStyle = dataGridViewCellStyle4;
            CUENTA.HeaderText = "CUENTA/CLABE";
            CUENTA.MaxInputLength = 18;
            CUENTA.MinimumWidth = 6;
            CUENTA.Name = "CUENTA";
            CUENTA.Width = 180;
            // 
            // IMPORTE
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            IMPORTE.DefaultCellStyle = dataGridViewCellStyle5;
            IMPORTE.HeaderText = "IMPORTE";
            IMPORTE.MaxInputLength = 15;
            IMPORTE.MinimumWidth = 6;
            IMPORTE.Name = "IMPORTE";
            IMPORTE.Width = 125;
            // 
            // btnLONOM
            // 
            btnLONOM.Location = new Point(598, 619);
            btnLONOM.Name = "btnLONOM";
            btnLONOM.Size = new Size(124, 45);
            btnLONOM.TabIndex = 2;
            btnLONOM.Text = "Generar Lay Out";
            btnLONOM.UseVisualStyleBackColor = true;
            btnLONOM.Click += btnLONOM_Click;
            // 
            // btnCNOM
            // 
            btnCNOM.Location = new Point(598, 670);
            btnCNOM.Name = "btnCNOM";
            btnCNOM.Size = new Size(124, 45);
            btnCNOM.TabIndex = 3;
            btnCNOM.Text = "Limpiar tabla";
            btnCNOM.UseVisualStyleBackColor = true;
            btnCNOM.Click += btnCNOM_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.Location = new Point(12, 628);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(122, 82);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblContador.Location = new Point(286, 628);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(182, 28);
            lblContador.TabIndex = 5;
            lblContador.Text = "Total de registros: 0";
            // 
            // lblSumaImportes
            // 
            lblSumaImportes.AutoSize = true;
            lblSumaImportes.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSumaImportes.Location = new Point(211, 656);
            lblSumaImportes.Name = "lblSumaImportes";
            lblSumaImportes.Size = new Size(293, 41);
            lblSumaImportes.TabIndex = 6;
            lblSumaImportes.Text = "Total importes: $0.00";
            // 
            // Nominas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 728);
            ControlBox = false;
            Controls.Add(lblSumaImportes);
            Controls.Add(lblContador);
            Controls.Add(btnSalir);
            Controls.Add(btnCNOM);
            Controls.Add(btnLONOM);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Nominas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nominas";
            FormClosing += Nominas_FormClosing;
            FormClosed += Nominas_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnLONOM;
        private Button btnCNOM;
        private Button btnSalir;
        private Label lblContador;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn CUENTA;
        private DataGridViewTextBoxColumn IMPORTE;
        private Label lblSumaImportes;
    }
}