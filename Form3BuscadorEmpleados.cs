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
    public partial class Form3BuscadorEmpleados : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        string connectionString;

        public Form3BuscadorEmpleados()
        {
            InitializeComponent();
            this.connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(this.connectionString);
            this.com = new SqlCommand();
            this.cn.StateChange += Cn_StateChange;
        }

        private void Cn_StateChange(object sender, StateChangeEventArgs e)
        {
            string mensaje = "La conexión está pasando de " + e.OriginalState + " a " + e.CurrentState;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string salario = this.txtSalario.Text;
            string sql = "SELECT * FROM EMP where salario >= " + salario;
            //conexión
            this.com.Connection = this.cn;
            //tipo de consulta
            this.com.CommandType = CommandType.Text;
            //consulta  
            this.com.CommandText = sql;
            //abrir conexión
            this.cn.Open();
            //entrar y salir
            this.reader = this.com.ExecuteReader();
            //leer
            while (this.reader.Read())
            {
                string ape = this.reader["APELLIDO"].ToString();
                string sal = this.reader["SALARIO"].ToString();
                this.lstEmpleados.Items.Add(ape + " - " + sal);
            }
            //salir
            this.reader.Close();
            //cerrar conexión
            this.cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM EMP where OFICIO = '" + this.txtOficio.Text + "'";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.lstEmpleados.Items.Clear();

            while (this.reader.Read())
            {
                string ape = this.reader["APELLIDO"].ToString();
                string ofi = this.reader["OFICIO"].ToString();
                this.lstEmpleados.Items.Add(ape + " - " + ofi);
            }

            this.reader.Close();
            this.cn.Close();
        }
    }
}
