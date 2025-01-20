using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using AdoNetCore.Models;
using AdoNetCore.Repositories;

namespace AdoNetCore
{
    public partial class Form13Salida : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form13Salida()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.LoadDepartamentos();
        }

        public async Task LoadDepartamentos()
        {
            string sql = "SP_ALL_DEPARTAMENTOS";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.comboBox1.Items.Clear();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                string nombre = this.reader["DNOMBRE"].ToString();
                this.comboBox1.Items.Add(nombre);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string sql = "SP_EMPLEADOS_DEPT_OUT";
            string nombre = this.comboBox1.SelectedItem.ToString();

            this.com.Parameters.AddWithValue("@nombre", nombre);
            SqlParameter pamSuma = new SqlParameter();
            SqlParameter pamMedia = new SqlParameter();
            SqlParameter pamCount = new SqlParameter();
            pamSuma.ParameterName = "@suma";
            pamMedia.ParameterName = "@media";
            pamCount.ParameterName = "@personas";
            pamSuma.Direction = ParameterDirection.Output;
            pamMedia.Direction = ParameterDirection.Output;
            pamCount.Direction = ParameterDirection.Output;
            pamSuma.Value = 0;
            pamMedia.Value = 0;
            pamCount.Value = 0;
            this.com.Parameters.Add(pamSuma);
            this.com.Parameters.Add(pamMedia);
            this.com.Parameters.Add(pamCount);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            this.listBox1.Items.Clear();
            while (await this.reader.ReadAsync())
            {
                string apellido = this.reader["APELLIDO"].ToString();
                this.listBox1.Items.Add(apellido);
            }

            await this.reader.CloseAsync();

            this.textBox1.Text = pamSuma.Value.ToString();
            this.textBox2.Text = pamMedia.Value.ToString();
            this.textBox3.Text = pamCount.Value.ToString();

            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
