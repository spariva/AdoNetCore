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

namespace AdoNetCore
{
    public partial class Form12MensajesServidor : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public Form12MensajesServidor()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.cn.InfoMessage += Cn_InfoMessage;
            this.LoadDepartamentos();
        }

        private void Cn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.label1.Text = e.Message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Insertar departamentos
            this.label1.Text = "";
            int numero = int.Parse(this.textBox1.Text);
            string nombre = this.textBox2.Text;
            string localidad = this.textBox3.Text;
            this.InsertDepartamento(numero, nombre, localidad);
            this.LoadDepartamentos();
        }

        public async void LoadDepartamentos()
        {
            string sql = "SP_ALL_DEPARTAMENTOS";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.listBox1.Items.Clear();

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                string nombre = this.reader["DNOMBRE"].ToString();
                this.listBox1.Items.Add(nombre);
            }
            this.reader.CloseAsync();
            this.cn.CloseAsync();
        }

        public void InsertDepartamento(int numero, string nombre, string localidad)
        {
            string sql = "SP_INSERT_DEPARTAMENTO";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.com.Parameters.AddWithValue("@numero", numero);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@localidad", localidad);
            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            MessageBox.Show("Departamentos insertados: " + afectados);
        }

    }
}
