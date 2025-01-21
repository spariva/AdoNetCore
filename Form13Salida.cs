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
    public partial class Form13Salida : Form
    {
        RepositoryDepartamentos repo;

        public Form13Salida()
        {
            InitializeComponent();
            this.repo = new RepositoryDepartamentos();
            this.LoadDepartamentos();
        }

        public async Task LoadDepartamentos()
        {
            List<Departamento> departamentos = await this.repo.GetDepartamentosAsync();
            foreach (Departamento dep in departamentos)
            {
                this.comboBox1.Items.Add(dep.Nombre);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string dep = this.comboBox1.Text;
            EmpleadoDeptInfo info = await this.repo.GetEmpleadosSpOut(dep);

            this.listBox1.Items.Clear();
            foreach (var empleado in info.Empleados)
            {
                this.listBox1.Items.Add(empleado);
            }
            this.textBox1.Text = info.SumaSalarial.ToString();
            this.textBox2.Text = info.MediaSalarial.ToString();
            this.textBox3.Text = info.Personas.ToString();
        }

        //public async Task LoadDepartamentos()
        //{
        //    string sql = "SP_ALL_DEPARTAMENTOS";
        //    this.com.CommandType = CommandType.StoredProcedure;
        //    this.com.CommandText = sql;
        //    await this.cn.OpenAsync();
        //    this.comboBox1.Items.Clear();
        //    this.reader = await this.com.ExecuteReaderAsync();
        //    while (await this.reader.ReadAsync())
        //    {
        //        string nombre = this.reader["DNOMBRE"].ToString();
        //        this.comboBox1.Items.Add(nombre);
        //    }
        //    await this.reader.CloseAsync();
        //    await this.cn.CloseAsync();
        //}



        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    string sql = "SP_EMPLEADOS_DEPT_OUT";
        //    string nombre = this.comboBox1.SelectedItem.ToString();

        //    this.com.Parameters.AddWithValue("@nombre", nombre);
        //    SqlParameter pamSuma = new SqlParameter();
        //    SqlParameter pamMedia = new SqlParameter();
        //    SqlParameter pamCount = new SqlParameter();
        //    pamSuma.ParameterName = "@suma";
        //    pamMedia.ParameterName = "@media";
        //    pamCount.ParameterName = "@personas";
        //    pamSuma.Direction = ParameterDirection.Output;
        //    pamMedia.Direction = ParameterDirection.Output;
        //    pamCount.Direction = ParameterDirection.Output;
        //    pamSuma.Value = 0;
        //    pamMedia.Value = 0;
        //    pamCount.Value = 0;
        //    this.com.Parameters.Add(pamSuma);
        //    this.com.Parameters.Add(pamMedia);
        //    this.com.Parameters.Add(pamCount);

        //    this.com.CommandType = CommandType.StoredProcedure;
        //    this.com.CommandText = sql;

        //    await this.cn.OpenAsync();
        //    this.reader = await this.com.ExecuteReaderAsync();
        //    this.listBox1.Items.Clear();
        //    while (await this.reader.ReadAsync())
        //    {
        //        string apellido = this.reader["APELLIDO"].ToString();
        //        this.listBox1.Items.Add(apellido);
        //    }

        //    await this.reader.CloseAsync();

        //    this.textBox1.Text = pamSuma.Value.ToString();
        //    this.textBox2.Text = pamMedia.Value.ToString();
        //    this.textBox3.Text = pamCount.Value.ToString();

        //    await this.cn.CloseAsync();
        //    this.com.Parameters.Clear();
        //}
    }
}
