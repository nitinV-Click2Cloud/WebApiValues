using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class ValidateController : Controller
    {
        // GET: api/values
        [HttpGet("{_ModuleName}")]
        public string GetProjectId(string _ModuleName, string _A, string _B, string _C, string _D)
        {
            string result = string.Empty;
            try
            {

                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
                SqlCommand sqlCmd = new SqlCommand();
                myConnection.Open();
                sqlCmd.Connection = myConnection;
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "CheckValidate";

                sqlCmd.Parameters.Add("@MODULENAME", SqlDbType.VarChar);
                sqlCmd.Parameters["@MODULENAME"].Value = _ModuleName;

                sqlCmd.Parameters.Add("@A", SqlDbType.VarChar);
                sqlCmd.Parameters["@A"].Value = string.IsNullOrEmpty(_A) ? "" : _A;

                sqlCmd.Parameters.Add("@B", SqlDbType.VarChar);
                sqlCmd.Parameters["@B"].Value = string.IsNullOrEmpty(_B) ? "" : _B;

                sqlCmd.Parameters.Add("@C", SqlDbType.VarChar);
                sqlCmd.Parameters["@C"].Value = string.IsNullOrEmpty(_C) ? "" : _C;
                sqlCmd.Parameters.Add("@D", SqlDbType.VarChar);
                sqlCmd.Parameters["@D"].Value = string.IsNullOrEmpty(_D) ? "" : _D;
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader[0].ToString();
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
