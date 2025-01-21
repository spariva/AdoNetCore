using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetCore
{
    public partial class Form14JsonSettings : Form
    {
        public Form14JsonSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("SqlTajamar");
            string imagen1 = configuration.GetSection("Imagenes:imagen1").Value;
            string imagen2 = configuration.GetSection("Imagenes:imagen2").Value;
            string backColor = configuration.GetSection("Colores:letra").Value;
            string letraColor = configuration.GetSection("Colores:fondo").Value;
            this.label1.Text = connectionString;
            this.pictureBox1.Load(imagen1);
            this.pictureBox2.Load(imagen2);
            this.button1.BackColor = Color.FromName(backColor);
            this.button1.ForeColor = Color.FromName(letraColor);

        }
    }
}
