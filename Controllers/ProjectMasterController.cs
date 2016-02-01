using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication4.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class ProjectMasterController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<WebApplication4.Models.tblProject_Master> Get()
        {
            return new tblProject_Master[] { };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public tblProject_Master Get(int id)
        {
            //return new tblProject_Master();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            //sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select Top 1 * from tblProject_Master where i_Id=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            tblProject_Master project_Master = null;
            while (reader.Read())
            {
                project_Master = new tblProject_Master();
                project_Master.i_ID= Int32.Parse(reader.GetValue(0).ToString());
                project_Master.s_Display_Project_ID = reader.GetValue(1).ToString();
                project_Master.s_Project_Title = reader.GetValue(2).ToString(); ;
                project_Master.s_Short_Title = reader.GetValue(3).ToString();
            }
            myConnection.Close();
            return project_Master;

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
