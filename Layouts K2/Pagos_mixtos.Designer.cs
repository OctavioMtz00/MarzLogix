namespace Layouts_K2
{
    partial class Pagos_mixtos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pagos_mixtos));
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            CUENTA = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column18 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column17 = new DataGridViewTextBoxColumn();
            btnExit = new Button();
            btnLOMX = new Button();
            btnCln = new Button();
            lblContador = new Label();
            lblSumaImportes = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(192, 255, 192);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSlateGray;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, CUENTA, Column2, Column3, Column6, Column4, Column5, Column7, Column8, Column9, Column14, Column12, Column18, Column11, Column13, Column10, Column15, Column16, Column17 });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle4.SelectionBackColor = Color.Lavender;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.Size = new Size(1508, 644);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellValidated += dataGridView1_CellValidated;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Column1
            // 
            Column1.HeaderText = "TIPO DE OPERACION";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.True;
            Column1.Width = 110;
            // 
            // CUENTA
            // 
            CUENTA.HeaderText = "CUENTA CARGO";
            CUENTA.MaxInputLength = 18;
            CUENTA.MinimumWidth = 6;
            CUENTA.Name = "CUENTA";
            CUENTA.Width = 105;
            // 
            // Column2
            // 
            Column2.HeaderText = "CTA. ABONO O CONVENIO CIE";
            Column2.MaxInputLength = 35;
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "IMPORTE";
            Column3.MaxInputLength = 16;
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 90;
            // 
            // Column6
            // 
            Column6.HeaderText = "DIVISA";
            Column6.MaxInputLength = 3;
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.Width = 80;
            // 
            // Column4
            // 
            Column4.HeaderText = "CONCEPTO";
            Column4.MaxInputLength = 50;
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 127;
            // 
            // Column5
            // 
            Column5.HeaderText = "REFERENCIA CIE, NUMERICA O ABA/BIC";
            Column5.MaxInputLength = 15;
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Width = 127;
            // 
            // Column7
            // 
            Column7.HeaderText = "TITULAR O BENEFICIARIO";
            Column7.MaxInputLength = 140;
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.Width = 127;
            // 
            // Column8
            // 
            Column8.HeaderText = "PAIS DEL BENEFICIARIO";
            Column8.MaxInputLength = 30;
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.Width = 127;
            // 
            // Column9
            // 
            Column9.HeaderText = "DIRECCION DEL BENEFICIARIO";
            Column9.MaxInputLength = 40;
            Column9.MinimumWidth = 6;
            Column9.Name = "Column9";
            Column9.Width = 127;
            // 
            // Column14
            // 
            Column14.HeaderText = "TELEFONO DEL BENEFICIARIO";
            Column14.MaxInputLength = 12;
            Column14.MinimumWidth = 6;
            Column14.Name = "Column14";
            Column14.Width = 125;
            // 
            // Column12
            // 
            Column12.HeaderText = "BANCO BENEFICIARIO";
            Column12.MaxInputLength = 30;
            Column12.MinimumWidth = 6;
            Column12.Name = "Column12";
            Column12.Width = 127;
            // 
            // Column18
            // 
            Column18.HeaderText = "PAIS BANCO BENEFICIARIO";
            Column18.MinimumWidth = 6;
            Column18.Name = "Column18";
            Column18.Width = 125;
            // 
            // Column11
            // 
            Column11.HeaderText = "DIRECCION BANCO BENFICIARIO";
            Column11.MinimumWidth = 6;
            Column11.Name = "Column11";
            Column11.Width = 125;
            // 
            // Column13
            // 
            Column13.HeaderText = "CALLE";
            Column13.MaxInputLength = 70;
            Column13.MinimumWidth = 6;
            Column13.Name = "Column13";
            Column13.Width = 127;
            // 
            // Column10
            // 
            Column10.HeaderText = "EDIFICIO";
            Column10.MaxInputLength = 16;
            Column10.MinimumWidth = 6;
            Column10.Name = "Column10";
            Column10.Width = 125;
            // 
            // Column15
            // 
            Column15.HeaderText = "CODIGO POSTAL";
            Column15.MaxInputLength = 16;
            Column15.MinimumWidth = 6;
            Column15.Name = "Column15";
            Column15.Width = 125;
            // 
            // Column16
            // 
            Column16.HeaderText = "CIUDAD";
            Column16.MaxInputLength = 35;
            Column16.MinimumWidth = 6;
            Column16.Name = "Column16";
            Column16.Width = 125;
            // 
            // Column17
            // 
            Column17.HeaderText = "REGION";
            Column17.MaxInputLength = 35;
            Column17.MinimumWidth = 6;
            Column17.Name = "Column17";
            Column17.Width = 125;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(12, 693);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(139, 51);
            btnExit.TabIndex = 1;
            btnExit.Text = "Salir";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnLOMX
            // 
            btnLOMX.Location = new Point(1391, 662);
            btnLOMX.Name = "btnLOMX";
            btnLOMX.Size = new Size(129, 44);
            btnLOMX.TabIndex = 2;
            btnLOMX.Text = "Generar Layout";
            btnLOMX.UseVisualStyleBackColor = true;
            btnLOMX.Click += btnLOMX_Click;
            // 
            // btnCln
            // 
            btnCln.Location = new Point(1391, 712);
            btnCln.Name = "btnCln";
            btnCln.Size = new Size(129, 44);
            btnCln.TabIndex = 3;
            btnCln.Text = "Limpiar tabla";
            btnCln.UseVisualStyleBackColor = true;
            btnCln.Click += btnCln_Click;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblContador.Location = new Point(222, 693);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(277, 41);
            lblContador.TabIndex = 4;
            lblContador.Text = "Total de registros: 0";
            // 
            // lblSumaImportes
            // 
            lblSumaImportes.AutoSize = true;
            lblSumaImportes.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSumaImportes.Location = new Point(696, 693);
            lblSumaImportes.Name = "lblSumaImportes";
            lblSumaImportes.Size = new Size(293, 41);
            lblSumaImportes.TabIndex = 5;
            lblSumaImportes.Text = "Total importes: $0.00";
            // 
            // Pagos_mixtos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1532, 768);
            ControlBox = false;
            Controls.Add(lblSumaImportes);
            Controls.Add(lblContador);
            Controls.Add(btnCln);
            Controls.Add(btnLOMX);
            Controls.Add(btnExit);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1550, 815);
            MinimumSize = new Size(1550, 815);
            Name = "Pagos_mixtos";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pagos mixtos";
            Load += Pagos_mixtos_Load;
            KeyDown += Pagos_mixtos_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnExit;
        private Button btnLOMX;
        private Button btnCln;
        private Label lblContador;
        private Label lblSumaImportes;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn CUENTA;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
    }
}