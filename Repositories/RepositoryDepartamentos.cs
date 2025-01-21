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
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA; Trust Server Certificate=True";
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

        public async Task<List<string>> GetDepartamentosSpAsync()
        {
            string sql = "SP_ALL_DEPARTAMENTOS";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();

            List<string> departamentos = new List<string>();

            while (await this.reader.ReadAsync())
            {
                string nombre = this.reader["DNOMBRE"].ToString();
                departamentos.Add(nombre);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();

            return departamentos;
        }

        public async Task<EmpleadoDeptInfo> GetEmpleadosSpOut(string dep)
        {
            string sql = "SP_EMPLEADOS_DEPT_OUT";
            this.com.Parameters.AddWithValue("@nombre", dep);

            SqlParameter pamSuma = new SqlParameter();
            pamSuma.ParameterName = "@suma";
            pamSuma.Value = 0;
            pamSuma.Direction = ParameterDirection.Output;
            this.com.Parameters.Add(pamSuma);

            SqlParameter pamMedia = new SqlParameter();
            pamMedia.ParameterName = "@media";
            pamMedia.Value = 0;
            pamMedia.Direction = ParameterDirection.Output;
            this.com.Parameters.Add(pamMedia);

            SqlParameter pamPersonas = new SqlParameter();
            pamPersonas.ParameterName = "@personas";
            pamPersonas.Value = 0;
            pamPersonas.Direction = ParameterDirection.Output;
            this.com.Parameters.Add(pamPersonas);

            this.com.CommandType = CommandType.StoredProcedure;
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

            decimal sumaSalarial = Convert.ToDecimal(this.com.Parameters["@suma"].Value);
            decimal mediaSalarial = Convert.ToDecimal(this.com.Parameters["@media"].Value);
            int cantidadPersonas = Convert.ToInt32(this.com.Parameters["@personas"].Value);

            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

            return new EmpleadoDeptInfo
            {
                Empleados = empleados,
                SumaSalarial = sumaSalarial,
                MediaSalarial = mediaSalarial,
                Personas = cantidadPersonas
            };
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