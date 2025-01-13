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

    public partial class Form5 : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public Form5()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();

            this.CargarSalas();
        }

        private async void CargarSalas()
        {
            string sql = "SELECT DISTINCT NOMBRE FROM SALA";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            this.listBox1.Items.Clear();

            while (await this.reader.ReadAsync())
            {
                string nombre = this.reader["NOMBRE"].ToString();
                this.listBox1.Items.Add(nombre);
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();    

            //for (int i = 0; i < reader.FieldCount; i++)
            //{
            //this.listBox1.Items.Add(reader.GetName(i));
            //}
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a sala to update.");
                return;
            }

            string selectedSala = this.listBox1.SelectedItem.ToString();
            string newNombre = this.textBox1.Text;

            string sql = "UPDATE SALA SET NOMBRE=@newNombre WHERE NOMBRE=@selectedNombre";
            SqlParameter pamNewNombre = new SqlParameter("@newNombre", newNombre);
            SqlParameter pamSelectedNombre = new SqlParameter("@selectedNombre", selectedSala);

            this.com.Parameters.Add(pamNewNombre);
            this.com.Parameters.Add(pamSelectedNombre);

            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            int afectados = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();

            this.com.Parameters.Clear();
            MessageBox.Show("Sala actualizada, " + afectados);
            this.CargarSalas();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
