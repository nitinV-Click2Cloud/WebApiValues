using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApplication4.Models;
using System.Data.SqlClient;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/nitin?&passWord=abc
        [HttpGet("{userName}")]
        //[HttpPost]
        public tbl_User Get(string userName,string passWord)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            //sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText =string.Format("Select Top 1 * from tbl_User where userName='{0}' and PassWord='{1}'",userName,passWord);
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            tbl_User user = null;
            while (reader.Read())
            {
                user = new tbl_User();
                user.ID = Int32.Parse(reader.GetValue(0).ToString());
                user.UserName = userName;
                user.PassWord = passWord;   
            }
            myConnection.Close();
            return user;
        }

        //[ActionName("GetMenus")]
        [HttpGet]
        public List<AdUserDetail> GetMenus()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "SpGetMenuItems"; 
            sqlCmd.Connection = myConnection;
            myConnection.Open();
           
            reader = sqlCmd.ExecuteReader();
            List<AdUserDetail> menus = new List<AdUserDetail>();
            while (reader.Read())
            {
                menus.Add(new AdUserDetail { 
                  MenuId = (string.IsNullOrEmpty(reader["MenuId"].ToString())? 0 : Convert.ToInt32(reader["MenuId"])),
                          MenuName = (string.IsNullOrEmpty(reader["MenuName"].ToString())) ? "" :reader["MenuName"].ToString(),
                    Parent = (string.IsNullOrEmpty(reader["Parent"].ToString()) ? 0 : Convert.ToInt32(reader["Parent"])),
                         Child = (string.IsNullOrEmpty(reader["Child"].ToString()) ? 0 : Convert.ToInt32(reader["Child"])),
                            Url = (string.IsNullOrEmpty(reader["Url"].ToString())) ? "" : reader["Url"].ToString()
                });
            }
            myConnection.Close();
            return menus;
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
