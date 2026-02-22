namespace Layouts_K2
{
    partial class Bienvenida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bienvenida));
            button1 = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            btnNom = new Button();
            btnPmx = new Button();
            btnACM = new Button();
            btnAltIB = new Button();
            btnExit = new Button();
            btnHelp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(248, 132);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(150, 56);
            button1.TabIndex = 0;
            button1.Text = "Altas intenacionales";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(122, 27);
            label1.Name = "label1";
            label1.Size = new Size(249, 26);
            label1.TabIndex = 1;
            label1.Text = "Bienvenido a FINCORE";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(343, 287);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(116, 86);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(284, 375);
            label2.Name = "label2";
            label2.Size = new Size(175, 15);
            label2.TabIndex = 3;
            label2.Text = "Elaborado por Octavio Martinez";
            // 
            // btnNom
            // 
            btnNom.Cursor = Cursors.Hand;
            btnNom.FlatAppearance.BorderSize = 0;
            btnNom.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNom.Location = new Point(70, 72);
            btnNom.Margin = new Padding(3, 2, 3, 2);
            btnNom.Name = "btnNom";
            btnNom.Size = new Size(150, 56);
            btnNom.TabIndex = 4;
            btnNom.Text = "Dispersión de Nóminas";
            btnNom.UseVisualStyleBackColor = true;
            btnNom.Click += btnNom_Click;
            // 
            // btnPmx
            // 
            btnPmx.Cursor = Cursors.Hand;
            btnPmx.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPmx.Location = new Point(70, 132);
            btnPmx.Margin = new Padding(3, 2, 3, 2);
            btnPmx.Name = "btnPmx";
            btnPmx.Size = new Size(150, 56);
            btnPmx.TabIndex = 5;
            btnPmx.Text = "Pagos mixtos";
            btnPmx.UseVisualStyleBackColor = true;
            btnPmx.Click += btnPmx_Click;
            // 
            // btnACM
            // 
            btnACM.Cursor = Cursors.Hand;
            btnACM.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnACM.Location = new Point(70, 201);
            btnACM.Margin = new Padding(3, 2, 3, 2);
            btnACM.Name = "btnACM";
            btnACM.Size = new Size(150, 56);
            btnACM.TabIndex = 6;
            btnACM.Text = "Altas mismo banco";
            btnACM.UseVisualStyleBackColor = true;
            btnACM.Click += btnACM_Click;
            // 
            // btnAltIB
            // 
            btnAltIB.Cursor = Cursors.Hand;
            btnAltIB.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAltIB.Location = new Point(248, 72);
            btnAltIB.Margin = new Padding(3, 2, 3, 2);
            btnAltIB.Name = "btnAltIB";
            btnAltIB.Size = new Size(150, 56);
            btnAltIB.TabIndex = 7;
            btnAltIB.Text = "Altas interbancarias";
            btnAltIB.UseVisualStyleBackColor = true;
            btnAltIB.Click += btnAltIB_Click;
            // 
            // btnExit
            // 
            btnExit.Cursor = Cursors.Hand;
            btnExit.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(24, 347);
            btnExit.Margin = new Padding(3, 2, 3, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(95, 36);
            btnExit.TabIndex = 9;
            btnExit.Text = "Salir";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnHelp
            // 
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHelp.Location = new Point(248, 201);
            btnHelp.Margin = new Padding(3, 2, 3, 2);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(150, 56);
            btnHelp.TabIndex = 10;
            btnHelp.Text = "Manual";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // Bienvenida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(471, 400);
            ControlBox = false;
            Controls.Add(btnHelp);
            Controls.Add(btnExit);
            Controls.Add(btnAltIB);
            Controls.Add(btnACM);
            Controls.Add(btnPmx);
            Controls.Add(btnNom);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Bienvenida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bienvenid@";
            FormClosing += Bienvenida_FormClosing;
            FormClosed += Bienvenida_FormClosed;
            Load += Bienvenida_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button btnNom;
        private Button btnPmx;
        private Button btnACM;
        private Button btnAltIB;
        private Button btnExit;
        private Button btnHelp;
    }
}
