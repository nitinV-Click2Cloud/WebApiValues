using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication4.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class AutoCompleteController : Controller
    {
        // GET: api/values
        [HttpGet("{Prefix}")]
        public string[] GetText(string Prefix, int count, string ContextKey)
        {
            List<string> lst = new List<string>();

            try
            {
                string[] isval = ContextKey.Split('~');
                string _modulename = (isval.Length > 0) ? isval[0] : "";
                string _SpName = (isval.Length > 1) ? isval[1] : "";
                string _condition = (isval.Length > 2) ? isval[2] : "";

                count = count == 0 ? 10 : count;
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();

                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = _SpName.ToString();

                sqlCmd.Parameters.Add("@MODULENAME", SqlDbType.VarChar);
                sqlCmd.Parameters["@MODULENAME"].Value = _modulename;

                sqlCmd.Parameters.Add("@A", SqlDbType.VarChar);
                sqlCmd.Parameters["@A"].Value = Prefix;
                sqlCmd.Parameters.Add("@B", SqlDbType.VarChar);
                sqlCmd.Parameters["@B"].Value = count.ToString();
                sqlCmd.Parameters.Add("@C", SqlDbType.VarChar);
                sqlCmd.Parameters["@C"].Value = _condition.ToString();

                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(string.Format("{0}^{1}", reader[0].ToString(), reader[1].ToString()));
                }
            }
            catch (Exception ex)
            {

            }
            return lst.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

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
