namespace AdoNetCore
{
    partial class Form2
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
            lstApellidos = new ListBox();
            lstColumnas = new ListBox();
            lstTiposDato = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // lstApellidos
            // 
            lstApellidos.FormattingEnabled = true;
            lstApellidos.Location = new Point(260, 31);
            lstApellidos.Name = "lstApellidos";
            lstApellidos.Size = new Size(154, 379);
            lstApellidos.TabIndex = 0;
            // 
            // lstColumnas
            // 
            lstColumnas.FormattingEnabled = true;
            lstColumnas.Location = new Point(430, 31);
            lstColumnas.Name = "lstColumnas";
            lstColumnas.Size = new Size(154, 379);
            lstColumnas.TabIndex = 1;
            // 
            // lstTiposDato
            // 
            lstTiposDato.FormattingEnabled = true;
            lstTiposDato.Location = new Point(606, 31);
            lstTiposDato.Name = "lstTiposDato";
            lstTiposDato.Size = new Size(154, 379);
            lstTiposDato.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(57, 45);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Conectar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(57, 144);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Desconectar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(57, 222);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Leer Datos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(57, 384);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(38, 15);
            lblMensaje.TabIndex = 6;
            lblMensaje.Text = "label1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMensaje);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lstTiposDato);
            Controls.Add(lstColumnas);
            Controls.Add(lstApellidos);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstApellidos;
        private ListBox lstColumnas;
        private ListBox lstTiposDato;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label lblMensaje;
    }
}