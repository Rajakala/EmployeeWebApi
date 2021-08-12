using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EmployeeWebApi.Models;

namespace EmployeeWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeModel>> Get()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ToString();
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand = new SqlCommand("GetEmployees", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            using(var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                var OrdinalID = sqlDataReader.GetOrdinal("ID");
                var OrdinalFirstName = sqlDataReader.GetOrdinal("FirstName");
                var OrdinalLastName = sqlDataReader.GetOrdinal("LastName");
                var OrdinalGender = sqlDataReader.GetOrdinal("Gender");
                var OrdinalSalary = sqlDataReader.GetOrdinal("Salary");
                while (sqlDataReader.Read())
                {
                    var employee = new EmployeeModel();
                    employee.ID = Convert.ToInt32(sqlDataReader.GetValue(OrdinalID));
                    employee.FirstName = Convert.ToString(sqlDataReader.GetValue(OrdinalFirstName));
                    employee.LastName = Convert.ToString(sqlDataReader.GetValue(OrdinalLastName));
                    employee.Gender = Convert.ToString(sqlDataReader.GetValue(OrdinalGender));
                    employee.Salary = Convert.ToInt32(sqlDataReader.GetValue(OrdinalSalary));
                    employees.Add(employee);
                }
            }

            return employees;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(EmployeeModel employee)
        {
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ToString();
            var sqlConnection = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand = new SqlCommand("InsertEmployee", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", employee.LastName);
            sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
            sqlCommand.Parameters.AddWithValue("@Salary", employee.Salary);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();
            if(sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateEmployee(EmployeeModel employee)
        {
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ToString();
            var sqlConnection = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand = new SqlCommand("UpdateEmployee", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ID", employee.ID);
            sqlCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", employee.LastName);
            sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
            sqlCommand.Parameters.AddWithValue("@Salary", employee.Salary);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();
            if(sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteEmployee(int id)
        {
            var sqlConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ToString();
            var sqlConnection = new SqlConnection(sqlConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DeleteEmployee", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ID", id);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();
            if(sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

       

       
    }
}