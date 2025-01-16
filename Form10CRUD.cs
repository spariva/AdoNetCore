using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdoNetCore.Models;
using AdoNetCore.Repositories;

namespace AdoNetCore
{
    public partial class Form10CRUD : Form
    {
        RepositoryUpdateEmpleados repo;

        public Form10CRUD()
        {
            InitializeComponent();
            this.repo = new RepositoryUpdateEmpleados();
            this.LoadOficios();
        }

        private async void LoadOficios()
        {
            List<string> oficios = await this.repo.GetOficiosAsync();
            this.listBox2.Items.Clear();
            this.listBox1.Items.Clear();
            foreach (string ofi in oficios)
            {
                this.listBox1.Items.Add(ofi);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                string oficio = this.listBox1.SelectedItem.ToString();
                this.LoadEmpleados(oficio);
            }
        }

        private async void LoadEmpleados(string oficio)
        {
            List<string> empleados = await this.repo.GetEmpleadosOficioAsync(oficio);
            this.listBox2.Items.Clear();
            foreach (string ape in empleados)
            {
                this.listBox2.Items.Add(ape);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int incremento = int.Parse(this.textBox1.Text);
            string oficio = this.listBox1.SelectedItem.ToString();
            string newOficio = this.textBox2.Text;
            List<int> datos = await this.repo.UpdateSalarioEmpleadosOficio(oficio, incremento, newOficio);

            int max = datos[0];
            int media = datos[1];
            int total = datos[2];
            int modificados = datos[3];
            this.label1.Text = "Max: " + max;
            this.label2.Text = "Media: " + media;
            this.label3.Text = "Total: " + total;
            MessageBox.Show("Empleados modificados: " + modificados);
            this.LoadOficios();
        }
    }
}
