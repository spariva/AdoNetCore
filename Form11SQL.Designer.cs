namespace AdoNetCore
{
    partial class Form11SQL
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
            lstPlantilla = new ListBox();
            cmbHospital = new ComboBox();
            txtIncremento = new TextBox();
            btnUpdateSalario = new Button();
            lblIncremento = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lstPlantilla
            // 
            lstPlantilla.FormattingEnabled = true;
            lstPlantilla.Location = new Point(36, 119);
            lstPlantilla.Name = "lstPlantilla";
            lstPlantilla.Size = new Size(444, 214);
            lstPlantilla.TabIndex = 0;
            // 
            // cmbHospital
            // 
            cmbHospital.FormattingEnabled = true;
            cmbHospital.Location = new Point(36, 53);
            cmbHospital.Name = "cmbHospital";
            cmbHospital.Size = new Size(135, 23);
            cmbHospital.TabIndex = 1;
            cmbHospital.SelectedIndexChanged += cmbHospital_SelectedIndexChanged;
            // 
            // txtIncremento
            // 
            txtIncremento.Location = new Point(366, 53);
            txtIncremento.Name = "txtIncremento";
            txtIncremento.Size = new Size(114, 23);
            txtIncremento.TabIndex = 2;
            // 
            // btnUpdateSalario
            // 
            btnUpdateSalario.Location = new Point(182, 370);
            btnUpdateSalario.Name = "btnUpdateSalario";
            btnUpdateSalario.Size = new Size(169, 23);
            btnUpdateSalario.TabIndex = 3;
            btnUpdateSalario.Text = "Modificar Salarios";
            btnUpdateSalario.UseVisualStyleBackColor = true;
            btnUpdateSalario.Click += btnUpdateSalario_Click;
            // 
            // lblIncremento
            // 
            lblIncremento.AutoSize = true;
            lblIncremento.Location = new Point(366, 35);
            lblIncremento.Name = "lblIncremento";
            lblIncremento.Size = new Size(68, 15);
            lblIncremento.TabIndex = 4;
            lblIncremento.Text = "Incremento";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 35);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 5;
            label1.Text = "Hospitales";
            // 
            // Form11SQL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(lblIncremento);
            Controls.Add(btnUpdateSalario);
            Controls.Add(txtIncremento);
            Controls.Add(cmbHospital);
            Controls.Add(lstPlantilla);
            Name = "Form11SQL";
            Text = "Form11SQL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstPlantilla;
        private ComboBox cmbHospital;
        private TextBox txtIncremento;
        private Button btnUpdateSalario;
        private Label lblIncremento;
        private Label label1;
    }
}