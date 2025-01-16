using AdoNetCore.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetCore.Repositories
{
    public class RepositoryHospitales
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryHospitales()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string sql = "select * from HOSPITAL";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Hospital> hospitales = new List<Hospital>();
            while (await this.reader.ReadAsync())
            {
                int id = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                string nombre = this.reader["NOMBRE"].ToString();
                string direccion = this.reader["DIRECCION"].ToString();
                string telefono = this.reader["TELEFONO"].ToString();
                int numCama = int.Parse(this.reader["NUM_CAMA"].ToString());
                Hospital hos = new Hospital();
                hos.IdHospital = id;
                hos.Nombre = nombre;
                hos.Direccion = direccion;
                hos.Telefono = telefono;
                hos.NumCama = numCama;
                hospitales.Add(hos);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return hospitales;
        }

        public async Task InsertHospitalAsync(int id, string nombre, string direccion, string telefono, int numCama)
        {
            string sql = "insert into HOSPITAL values (@id, @nombre, @direccion, @telefono, @numCama)";
            SqlParameter pamId = new SqlParameter("@id", id);
            this.com.Parameters.Add(pamId);
            SqlParameter pamNombre = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamNombre);
            SqlParameter pamDireccion = new SqlParameter("@direccion", direccion);
            this.com.Parameters.Add(pamDireccion);
            SqlParameter pamTelefono = new SqlParameter("@telefono", telefono);
            this.com.Parameters.Add(pamTelefono);
            SqlParameter pamNumCama = new SqlParameter("@numCama", numCama);
            this.com.Parameters.Add(pamNumCama);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateHospitakAsync(int id, string nombre, string direccion, string telefono, int numCama)
        {
            string sql = "update HOSPITAL set NOMBRE=@nombre, DIRECCION=@direccion, TELEFONO=@telefono, NUM_CAMA=@numCama where HOSPITAL_COD=@id";

            this.com.Parameters.AddWithValue("@id", id);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@numCama", numCama);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteHospitalAsync(int id)
        {
            string sql = "delete from HOSPITAL where HOSPITAL_COD=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }


    }
}
