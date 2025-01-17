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

#region STORED PROCEDURES 
//create procedure SP_ALL_HOSPITALES
//as
//select * from HOSPITAL
//go



//create procedure SP_UPDATEPLANTILLA_HOSPITAL
//(@nombre nvarchar(50), @incremento int) 
//as
//	declare @hospitalcod int

//	select @hospitalcod=HOSPITAL_COD from HOSPITAL
//	where NOMBRE=@nombre

//	update PLANTILLA set SALARIO=SALARIO + @incremento
//	where HOSPITAL_COD=@hospitalcod

//	select * from plantilla where HOSPITAL_COD=@hospitalcod
//go
#endregion

namespace AdoNetCore
{
    public partial class Form11SQL : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public Form11SQL()
        {
            InitializeComponent();
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.LoadHospitales();
        }

        public async void LoadHospitales()
        {
            string sql = "SP_ALL_HOSPITALES";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.lstPlantilla.Items.Clear();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                string hospital = this.reader["NOMBRE"].ToString();
                this.cmbHospital.Items.Add(hospital);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
        }

        private async void btnUpdateSalario_Click(object sender, EventArgs e)
        {
            string nombre = this.cmbHospital.SelectedItem.ToString();
            int incremento = int.Parse(this.txtIncremento.Text);
            string sql = "SP_UPDATEPLANTILLA_HOSPITAL";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@incremento", incremento);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.lstPlantilla.Items.Clear();
            int afectados = await this.com.ExecuteNonQueryAsync();
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            await this.LoadPlantilla();
            MessageBox.Show("Salarios actualizados: " + afectados);
        }

        private async void cmbHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
           await this.LoadPlantilla(); 
        }

        private async Task LoadPlantilla()
        {
            string nombre = this.cmbHospital.SelectedItem.ToString();
            string sql = "SP_SHOWPLANTILLA_HOSPITAL";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.lstPlantilla.Items.Clear();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                string plantilla = this.reader["APELLIDO"].ToString() + " " + this.reader["SALARIO"].ToString();
                this.lstPlantilla.Items.Add(plantilla);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
