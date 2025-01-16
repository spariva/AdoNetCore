using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AdoNetCore.Repositories
{
    internal class RepositoryUpdateEmpleados
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryUpdateEmpleados()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string sql = "SELECT DISTINCT OFICIO FROM EMP";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<string> oficios = new List<string>();
            while (await this.reader.ReadAsync())
            {
                string oficio = this.reader["OFICIO"].ToString();
                oficios.Add(oficio);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return oficios;
        }

        public async Task<List<string>> GetEmpleadosOficioAsync(string oficio)
        {
            string sql = "SELECT * FROM EMP WHERE OFICIO =@oficio";
            this.com.Parameters.AddWithValue("@oficio", oficio);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<string> empleados = new List<string>();
            while (await this.reader.ReadAsync())
            {
                string apellido = this.reader["APELLIDO"].ToString();
                empleados.Add(apellido);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return empleados;
        }

        public async Task<int> UpdateSalarioEmpleadosOficio(string oficio, int incremento)
        {
            string sql = "UPDATE EMP SET SALARIO=SALARIO+@incremento WHERE OFICIO=@oficio";
            this.com.Parameters.AddWithValue("@incremento", incremento);
            this.com.Parameters.AddWithValue("@oficio", oficio);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            int afectados = await this.com.ExecuteNonQueryAsync();
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return afectados;
        }

        public async Task<List<int>> UpdateSalarioEmpleadosOficio(string oficio, int incremento, string newOficio)
        {
            int afectados = await UpdateSalarioEmpleadosOficio(oficio, incremento);
            string sql = "UPDATE EMP SET OFICIO=@newOficio WHERE OFICIO=@oficio";
            this.com.Parameters.AddWithValue("@newOficio", newOficio);
            this.com.Parameters.AddWithValue("@oficio", oficio);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            List<int> datos = await CalculateMaxAvgSumSalarios(newOficio);
            datos.Add(afectados);
            return datos;
        }

        public async Task<List<int>> CalculateMaxAvgSumSalarios(string oficio)
        {
            string sql = "SELECT MAX(SALARIO) AS MAXIMO, AVG(SALARIO) AS MEDIA, SUM(SALARIO) AS TOTAL FROM EMP WHERE OFICIO=@oficio";
            this.com.Parameters.AddWithValue("@oficio", oficio);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<int> datos = new List<int>();
            if (await this.reader.ReadAsync())
            {
                int maximo = int.Parse(this.reader["MAXIMO"].ToString());
                int mediaa = int.Parse(this.reader["MEDIA"].ToString());
                int total = int.Parse(this.reader["TOTAL"].ToString());
                datos.Add(maximo);
                datos.Add(mediaa);
                datos.Add(total);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return datos;
        }
    }
}
