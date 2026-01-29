namespace Layouts_K2
{
    partial class Altas_int
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Altas_int));
            dataGridView1 = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            buildLayoutInt = new Button();
            DelAll = new Button();
            lblContador = new Label();
            btnCloseAI = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new Font("Calibri", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.Menu;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11, Column12, Column14 });
            dataGridView1.Cursor = Cursors.IBeam;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.AliceBlue;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = SystemColors.WindowText;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.InactiveCaption;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1587, 583);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded_1;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved_1;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Column2
            // 
            Column2.HeaderText = "CUENTA BENEFICIARIA";
            Column2.MaxInputLength = 35;
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 200;
            // 
            // Column3
            // 
            Column3.HeaderText = "DIVISA";
            Column3.MaxInputLength = 3;
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 80;
            // 
            // Column4
            // 
            Column4.HeaderText = "IMPORTE MAXIMO DE PAGO";
            Column4.MaxInputLength = 16;
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "NOMBRE DEL BENEFICIARIO";
            Column5.MaxInputLength = 140;
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Width = 125;
            // 
            // Column6
            // 
            Column6.HeaderText = "ALIAS DE LA CUENTA";
            Column6.MaxInputLength = 30;
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.Width = 125;
            // 
            // Column7
            // 
            Column7.HeaderText = "CODIGO: ABA/BIC";
            Column7.MaxInputLength = 15;
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.Width = 125;
            // 
            // Column8
            // 
            Column8.HeaderText = "CALLE";
            Column8.MaxInputLength = 70;
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.Width = 125;
            // 
            // Column9
            // 
            Column9.HeaderText = "NUMERO EDIFICIO";
            Column9.MaxInputLength = 16;
            Column9.MinimumWidth = 6;
            Column9.Name = "Column9";
            Column9.Width = 125;
            // 
            // Column10
            // 
            Column10.HeaderText = "CODIGO POSTAL";
            Column10.MaxInputLength = 16;
            Column10.MinimumWidth = 6;
            Column10.Name = "Column10";
            Column10.Width = 125;
            // 
            // Column11
            // 
            Column11.HeaderText = "CIUDAD";
            Column11.MaxInputLength = 35;
            Column11.MinimumWidth = 6;
            Column11.Name = "Column11";
            Column11.Width = 125;
            // 
            // Column12
            // 
            Column12.HeaderText = "REGION";
            Column12.MaxInputLength = 35;
            Column12.MinimumWidth = 6;
            Column12.Name = "Column12";
            Column12.Width = 125;
            // 
            // Column14
            // 
            Column14.HeaderText = "PAIS";
            Column14.MaxInputLength = 30;
            Column14.MinimumWidth = 6;
            Column14.Name = "Column14";
            Column14.Width = 125;
            // 
            // buildLayoutInt
            // 
            buildLayoutInt.Location = new Point(1475, 601);
            buildLayoutInt.Name = "buildLayoutInt";
            buildLayoutInt.Size = new Size(124, 29);
            buildLayoutInt.TabIndex = 1;
            buildLayoutInt.Text = "Generar Lay Out";
            buildLayoutInt.UseVisualStyleBackColor = true;
            buildLayoutInt.Click += buildLayoutInt_Click;
            // 
            // DelAll
            // 
            DelAll.Location = new Point(1475, 636);
            DelAll.Name = "DelAll";
            DelAll.Size = new Size(124, 29);
            DelAll.TabIndex = 2;
            DelAll.Text = "Limpiar tabla";
            DelAll.UseVisualStyleBackColor = true;
            DelAll.Click += DelAll_Click;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblContador.Location = new Point(671, 613);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(238, 35);
            lblContador.TabIndex = 5;
            lblContador.Text = "Total de registros: 0";
            // 
            // btnCloseAI
            // 
            btnCloseAI.Location = new Point(12, 601);
            btnCloseAI.Name = "btnCloseAI";
            btnCloseAI.Size = new Size(122, 64);
            btnCloseAI.TabIndex = 6;
            btnCloseAI.Text = "Salir";
            btnCloseAI.UseVisualStyleBackColor = true;
            btnCloseAI.Click += btnCloseAI_Click;
            // 
            // Altas_int
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1614, 675);
            Controls.Add(btnCloseAI);
            Controls.Add(lblContador);
            Controls.Add(DelAll);
            Controls.Add(buildLayoutInt);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(1632, 722);
            Name = "Altas_int";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Altas internacionales";
            FormClosing += Altas_int_FormClosing;
            FormClosed += Altas_int_FormClosed;
            Load += Altas_int_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button buildLayoutInt;
        private Button DelAll;
        private Label lblContador;
        private Button btnCloseAI;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column14;
    }
}