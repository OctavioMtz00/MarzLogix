namespace Layouts_K2
{
    partial class Altas_MB
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
            dataGridView1 = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            CUENTA = new DataGridViewTextBoxColumn();
            IMPORTE = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            lblContador = new Label();
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
            dataGridView1.ColumnHeadersHeight = 25;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, CUENTA, IMPORTE, Column1, Column2 });
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
            dataGridView1.Size = new Size(976, 511);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Nombre
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Nombre.DefaultCellStyle = dataGridViewCellStyle3;
            Nombre.Frozen = true;
            Nombre.HeaderText = "CUENTA";
            Nombre.MaxInputLength = 40;
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Resizable = DataGridViewTriState.False;
            Nombre.Width = 180;
            // 
            // CUENTA
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CUENTA.DefaultCellStyle = dataGridViewCellStyle4;
            CUENTA.HeaderText = "DIVISA";
            CUENTA.MaxInputLength = 18;
            CUENTA.MinimumWidth = 6;
            CUENTA.Name = "CUENTA";
            CUENTA.Resizable = DataGridViewTriState.False;
            CUENTA.Width = 80;
            // 
            // IMPORTE
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            IMPORTE.DefaultCellStyle = dataGridViewCellStyle5;
            IMPORTE.HeaderText = "IMPORTE MAX.";
            IMPORTE.MaxInputLength = 15;
            IMPORTE.MinimumWidth = 6;
            IMPORTE.Name = "IMPORTE";
            IMPORTE.Resizable = DataGridViewTriState.False;
            IMPORTE.Width = 125;
            // 
            // Column1
            // 
            Column1.HeaderText = "ALIAS";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 250;
            // 
            // Column2
            // 
            Column2.HeaderText = "EMAIL";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 250;
            // 
            // button1
            // 
            button1.Location = new Point(34, 474);
            button1.Name = "button1";
            button1.Size = new Size(8, 8);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(12, 536);
            button2.Name = "button2";
            button2.Size = new Size(122, 67);
            button2.TabIndex = 3;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(868, 529);
            button3.Name = "button3";
            button3.Size = new Size(120, 34);
            button3.TabIndex = 4;
            button3.Text = "Generar Layout";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(868, 569);
            button4.Name = "button4";
            button4.Size = new Size(120, 34);
            button4.TabIndex = 5;
            button4.Text = "Limpiar tabla";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblContador.Location = new Point(322, 554);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(218, 35);
            lblContador.TabIndex = 6;
            lblContador.Text = "Total de registros:";
            // 
            // Altas_MB
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 615);
            Controls.Add(lblContador);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Altas_MB";
            Text = "Altas_MB";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label lblContador;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn CUENTA;
        private DataGridViewTextBoxColumn IMPORTE;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}