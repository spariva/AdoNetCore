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
    public partial class Form3Delete : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form3Delete()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.CargarPacientes();
        }

        private void CargarPacientes()
        {
            string sql = "SELECT * FROM ENFERMO";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.listBox1.Items.Clear();

            while (this.reader.Read())
            {
                string inscripcion = this.reader["INSCRIPCION"].ToString();
                string apellido = this.reader["APELLIDO"].ToString();
                this.listBox1.Items.Add(inscripcion + " - " + apellido);
            }

            this.reader.Close();
            this.cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM ENFERMO WHERE INSCRIPCION=" + this.textBox1.Text;
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();
            MessageBox.Show("Filas afectadas: " + afectados);
            this.CargarPacientes();
        }
    }
}
