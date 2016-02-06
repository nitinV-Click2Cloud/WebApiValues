using System;
using System.Collections.Generic;
using WebApplication4.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class PIMasterController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get(PI_Master _PI_Master, string mode)
        {
            string result = string.Empty;
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "spPI_MasterDML";
            sqlCmd.Connection = myConnection;
            myConnection.Open();

            sqlCmd.Parameters.Add("@StatementType", SqlDbType.VarChar);
            sqlCmd.Parameters["@StatementType"].Value = mode;


            sqlCmd.Parameters.Add("@i_ID", SqlDbType.Int);
            sqlCmd.Parameters["@i_ID"].Value = _PI_Master.i_ID;
            
            if (mode.ToString() != "Delete")
            {

                sqlCmd.Parameters.Add("@i_Dept_ID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Dept_ID"].Value = _PI_Master.i_Dept_ID;

                sqlCmd.Parameters.Add("@s_Firstname", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Firstname"].Value = _PI_Master.s_Firstname;

                sqlCmd.Parameters.Add("@s_Lastname", SqlDbType.VarChar);
                sqlCmd.Parameters["@StatementType"].Value = _PI_Master.s_Lastname;

                sqlCmd.Parameters.Add("@s_Email", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Email"].Value = _PI_Master.s_Email;

                sqlCmd.Parameters.Add("@s_Phone_no", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Phone_no"].Value = _PI_Master.s_Phone_no;

                sqlCmd.Parameters.Add("@s_MCR_No", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_MCR_No"].Value = _PI_Master.s_MCR_No;

                sqlCmd.Parameters.Add("@UserCID", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserCID"].Value = _PI_Master.s_CreatedBy_ID;

            }
            sqlCmd.Parameters.Add("@Ret_Parameter", SqlDbType.VarChar);
            sqlCmd.Parameters["@Ret_Parameter"].Size = 500;
            sqlCmd.Parameters["@Ret_Parameter"].Direction = ParameterDirection.Output;
            sqlCmd.ExecuteNonQuery();
            if (string.IsNullOrEmpty(sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString()))
            {
                result = "Success" + " | " + sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            else
            {
                result = sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]

        public string Post([FromBody]PI_Master pi_Master,string mode="Insert")
        {
            string result = string.Empty;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            myConnection.Open();
            sqlCmd.Connection = myConnection;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "spPI_MasterDML";
            sqlCmd.Parameters.Add("@StatementType", SqlDbType.VarChar);
            sqlCmd.Parameters["@StatementType"].Value = mode;
            sqlCmd.Parameters.Add("@i_ID", SqlDbType.Int);
            sqlCmd.Parameters["@i_ID"].Value = pi_Master.i_ID;

            if (mode.ToString() != "Delete")
            {
                sqlCmd.Parameters.Add("@i_Dept_ID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Dept_ID"].Value = pi_Master.i_Dept_ID;
                sqlCmd.Parameters.Add("@s_Firstname", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Firstname"].Value = pi_Master.s_Firstname;
                sqlCmd.Parameters.Add("@s_Lastname", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Lastname"].Value = pi_Master.s_Lastname;
                sqlCmd.Parameters.Add("@s_Email", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Email"].Value = pi_Master.s_Email;
                sqlCmd.Parameters.Add("@s_Phone_no", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Phone_no"].Value = pi_Master.s_Phone_no;
                sqlCmd.Parameters.Add("@s_MCR_No", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_MCR_No"].Value = pi_Master.s_MCR_No;
                sqlCmd.Parameters.Add("@UserCID", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserCID"].Value = pi_Master.s_CreatedBy_ID;
            }
            sqlCmd.Parameters.Add("@Ret_Parameter", SqlDbType.VarChar);
            sqlCmd.Parameters["@Ret_Parameter"].Size = 500;
            sqlCmd.Parameters["@Ret_Parameter"].Direction = ParameterDirection.Output;
            sqlCmd.ExecuteNonQuery();
            if (string.IsNullOrEmpty(sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString()))
            {
                result = "Success" + " | " + sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            else
            {
                result = sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            myConnection.Close();
            return result;
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
