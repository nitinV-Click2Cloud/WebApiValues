using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApplication4.Models;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class DropDownDataController : Controller
    {
        // GET: api/values
        [HttpGet("{dropDownName}")]
        public IEnumerable<clsDropDown> Get(string dropDownName, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            List<clsDropDown> listDDL = new List<clsDropDown>();

            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();

                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "SP_POPDDL";

                sqlCmd.Parameters.Add("@MODULENAME", SqlDbType.VarChar);
                sqlCmd.Parameters["@MODULENAME"].Value = dropDownName.ToString();

                sqlCmd.Parameters.Add("@A", SqlDbType.VarChar);
                sqlCmd.Parameters["@A"].Value = string.IsNullOrEmpty(param1) ? "" : param1;

                //sqlCmd.Parameters.Add("@B", SqlDbType.VarChar);
                //sqlCmd.Parameters["@B"].Value = param2.ToString();
                //sqlCmd.Parameters.Add("@C", SqlDbType.VarChar);
                //sqlCmd.Parameters["@C"].Value = param3.ToString();
                //sqlCmd.Parameters.Add("@D", SqlDbType.VarChar);
                //sqlCmd.Parameters["@D"].Value = param4.ToString();
                //sqlCmd.Parameters.Add("@E", SqlDbType.VarChar);
                //sqlCmd.Parameters["@E"].Value = param5.ToString();

                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    listDDL.Add(new clsDropDown()
                    {
                        DisplayField = reader.GetValue(0).ToString(),
                        ValueField = reader.GetValue(1).ToString()
                    });
                }
                myConnection.Close();
            }
            catch(Exception ex) { }
                return listDDL;
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
