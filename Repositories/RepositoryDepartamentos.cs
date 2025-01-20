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
    public class RepositoryDepartamentos
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryDepartamentos()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        //CREATE, READ, UPDATE, DELETE
        //DEVOLVER TODOS LOS DEPARTAMENTOS
        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string sql = "select * from DEPT";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Departamento> departamentos = new List<Departamento>();
            while (await this.reader.ReadAsync())
            {
                int id = int.Parse(this.reader["DEPT_NO"].ToString());
                string nombre = this.reader["DNOMBRE"].ToString();
                string localidad = this.reader["LOC"].ToString();
                Departamento dept = new Departamento();
                dept.IdDepartamento = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                departamentos.Add(dept);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return departamentos;
        }

        public async Task<List<Departamento>> GetDepartamentosSpAsync()
        {
            string sql = "SP_ALL_DEPARTAMENTOS";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Departamento> departamentos = new List<Departamento>();
            while (await this.reader.ReadAsync())
            {
                int id = int.Parse(this.reader["DEPT_NO"].ToString());
                string nombre = this.reader["DNOMBRE"].ToString();
                string localidad = this.reader["LOC"].ToString();
                Departamento dept = new Departamento();
                dept.IdDepartamento = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                departamentos.Add(dept);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return departamentos;
        }

        public async Task InsertDepartamentoAsync(int id, string nombre, string localidad)
        {
            string sql = "insert into DEPT values (@id, @nombre, @localidad)";
            SqlParameter pamId = new SqlParameter("@id", id);
            this.com.Parameters.Add(pamId);
            SqlParameter pamNombre = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamNombre);
            SqlParameter pamLocalidad = new SqlParameter("@localidad", localidad);
            this.com.Parameters.Add(pamLocalidad);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            string sql = "update DEPT set DNOMBRE=@nombre, LOC=@localidad where DEPT_NO=@id";
            //TENEMOS UN METOOD PARA ALMACENAR PARAMENTROS DIRECTAMENTE
            //EN EL COMMAND SIN CREAR OBJETOS
            //ESTE METODO SOLAMENTE LO UTILIZAMOS CUANDO LOS PARAMETROS 
            //SEAN TIPADOS PRIMITIVOS
            this.com.Parameters.AddWithValue("@id", id);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@localidad", localidad);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            string sql = "delete from DEPT where DEPT_NO=@id";
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