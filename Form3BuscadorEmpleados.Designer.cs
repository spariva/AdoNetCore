namespace AdoNetCore
{
    partial class Form3BuscadorEmpleados
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
            lstEmpleados = new ListBox();
            txtSalario = new TextBox();
            button1 = new Button();
            button2 = new Button();
            txtOficio = new TextBox();
            SuspendLayout();
            // 
            // lstEmpleados
            // 
            lstEmpleados.FormattingEnabled = true;
            lstEmpleados.Location = new Point(54, 149);
            lstEmpleados.Name = "lstEmpleados";
            lstEmpleados.Size = new Size(703, 199);
            lstEmpleados.TabIndex = 0;
            // 
            // txtSalario
            // 
            txtSalario.Location = new Point(54, 44);
            txtSalario.Name = "txtSalario";
            txtSalario.Size = new Size(254, 23);
            txtSalario.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(54, 96);
            button1.Name = "button1";
            button1.Size = new Size(213, 23);
            button1.TabIndex = 2;
            button1.Text = "Buscar empleados";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(512, 96);
            button2.Name = "button2";
            button2.Size = new Size(213, 23);
            button2.TabIndex = 3;
            button2.Text = "Buscar por Oficio";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // txtOficio
            // 
            txtOficio.Location = new Point(456, 44);
            txtOficio.Name = "txtOficio";
            txtOficio.Size = new Size(254, 23);
            txtOficio.TabIndex = 4;
            // 
            // Form3BuscadorEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtOficio);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtSalario);
            Controls.Add(lstEmpleados);
            Name = "Form3BuscadorEmpleados";
            Text = "Form3BuscadorEmpleados";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstEmpleados;
        private TextBox txtSalario;
        private Button button1;
        private Button button2;
        private TextBox txtOficio;
    }
}