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
    public partial class Form4EliminarPlantilla : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form4EliminarPlantilla()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();

            this.CargarPlantilla();
        }

        private void CargarPlantilla()
        {
            string sql = "SELECT * FROM PLANTILLA";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.listBox1.Items.Clear();

            for (int i = 0; i < this.reader.FieldCount; i++)
            {
                string columna = this.reader.GetName(i);
                this.listBox1.Items.Add(columna);
            }

            while (this.reader.Read())
            {
                string empno = this.reader["EMPLEADO_NO"].ToString();
                string apellido = this.reader["APELLIDO"].ToString();
                string funcion = this.reader["FUNCION"].ToString();
                this.listBox1.Items.Add(apellido + " - " + funcion + " -- " + empno);
            }

            this.reader.Close();
            this.cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM PLANTILLA WHERE EMP_NO=" + this.textBox1.Text;
            this.com.Connection.Open();
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();

            MessageBox.Show("Filas borradas: " + afectados);
            this.CargarPlantilla();
        }
    }
}
