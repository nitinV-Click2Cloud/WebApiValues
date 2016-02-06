using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication4.Models;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class ProjectMasterController : Controller
    {
        // GET: api/values
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<WebApplication4.Models.tblProject_Master> Get()
        {
            List<tblProject_Master> pm = new List<tblProject_Master>();

            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spProjectMasterDML";
                sqlCmd.Parameters.Add("@StatementType", SqlDbType.VarChar);
                sqlCmd.Parameters["@StatementType"].Value= "select";
                sqlCmd.Connection = myConnection;
                myConnection.Open();

                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    pm.Add(new tblProject_Master
                    {
                        i_ID = Convert.ToInt32(reader["i_ID"]),
                        s_Display_Project_ID = Convert.ToString(reader["s_Display_Project_ID"]),
                        s_Project_Title = Convert.ToString(reader["s_Project_Title"]),
                        Project_Category_Name = Convert.ToString(reader["Project_Category_Name"]),
                        s_IRB_No = Convert.ToString(reader["s_IRB_No"]),
                        Project_Type = Convert.ToString(reader["Project_Type"]),
                        PI_NAME = Convert.ToString(reader["PI_NAME"]),
                        s_CreatedBy_ID = Convert.ToString(reader["s_CreatedBy_ID"])

                    });
                }
            }
            catch (Exception)
            {


            }

            return pm;
        }
       
        // GET api/values/5
        [HttpGet("{id}")]
        public ProjectMasterModel Get(int id)
        {
            List<PI_Master> piList = new List<PI_Master>();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "spProjectMasterDML";
            sqlCmd.Parameters.Add("@StatementType", SqlDbType.VarChar);
            sqlCmd.Parameters["@StatementType"].Value = "select";
            sqlCmd.Parameters.Add("@i_ID", SqlDbType.Int);
            sqlCmd.Parameters["@i_ID"].Value = id;

            sqlCmd.Connection = myConnection;
            myConnection.Open();


            reader = sqlCmd.ExecuteReader();
            ProjectMasterModel project_Master = null;
            project_Master = new ProjectMasterModel();
            project_Master.DEPT_PI = new List<PI_Master>();
            project_Master.pcd = new List<Project_Coordinator_Details>();
            while (reader.Read())
            {
                if(reader["i_Dept_ID"] != DBNull.Value) { 
                    project_Master.DEPT_PI.Add(new PI_Master(){
                        i_Dept_ID = (reader["i_Dept_ID"] != DBNull.Value)? Convert.ToInt32(reader["i_Dept_ID"]):0,
                       i_ID = (reader["PI_ID"] != DBNull.Value)? Convert.ToInt32(reader["PI_ID"]):0,
                    s_Email = (reader["s_Email"] != DBNull.Value) ? reader["s_Email"].ToString() : "",
                    s_Phone_no = (reader["s_Phone_no"] != DBNull.Value) ?reader["s_Phone_no"].ToString():"",
                     s_MCR_No = (reader["s_MCR_No"] != DBNull.Value ? reader["s_MCR_No"].ToString():""),
                     s_DeptName = (reader["Dept_Name"] != DBNull.Value ? reader["Dept_Name"].ToString():""),
                        s_PIName= (reader["s_PI_Name"] != DBNull.Value ? reader["s_PI_Name"].ToString() : "")
                    });
                }
                if(reader["i_Coordinator_ID"] != DBNull.Value) { 
                    project_Master.pcd.Add(new Project_Coordinator_Details() {
                        i_Coordinator_ID = reader["i_Coordinator_ID"] != DBNull.Value ? reader["i_Coordinator_ID"].ToString() : "",
                        s_Coordinator_name = reader["s_Coordinator_name"] != DBNull.Value ? reader["s_Coordinator_name"].ToString() : ""
                    });
                }
                project_Master._Project_Master = new tblProject_Master();
                project_Master._Project_Master.i_ID = (reader["i_ID"] != DBNull.Value ? Convert.ToInt32(reader["i_ID"]) : 0);
                project_Master._Project_Master.s_Display_Project_ID = (reader["s_Display_Project_ID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Display_Project_ID"]));
                project_Master._Project_Master.s_Project_Title = (reader["s_Project_Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Project_Title"]));
                project_Master._Project_Master.s_Short_Title = (reader["s_Short_Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Short_Title"]));
                project_Master._Project_Master.i_Project_Category_ID = (reader["i_Project_Category_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["i_Project_Category_ID"]));
                project_Master._Project_Master.i_Project_Type_ID = (reader["i_Project_Type_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["i_Project_Type_ID"]));
                project_Master._Project_Master.i_Project_Subtype_ID = (reader["i_Project_Subtype_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["i_Project_Subtype_ID"]));
                project_Master._Project_Master.b_Collaboration_Involved = (reader["b_Collaboration_Involved"] == DBNull.Value ? false : Convert.ToBoolean(reader["b_Collaboration_Involved"]));
                project_Master._Project_Master.b_StartBy_TTSH = (reader["b_StartBy_TTSH"] == DBNull.Value ? false : Convert.ToBoolean(reader["b_StartBy_TTSH"]));
                project_Master._Project_Master.b_Funding_req = (reader["b_Funding_req"] == DBNull.Value ? false : Convert.ToBoolean(reader["b_Funding_req"]));
                project_Master._Project_Master.b_Ischild = (reader["b_Ischild"] == DBNull.Value ? false : Convert.ToBoolean(reader["b_Ischild"]));
                project_Master._Project_Master.i_Parent_ProjectID = (reader["i_Parent_ProjectID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["i_Parent_ProjectID"]));
                project_Master._Project_Master.s_Project_Alias1 = (reader["s_Project_Alias1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Project_Alias1"]));
                project_Master._Project_Master.s_Project_Alias2 = (reader["s_Project_Alias2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Project_Alias2"]));
                project_Master._Project_Master.s_Project_Desc = (reader["s_Project_Desc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Project_Desc"]));
                project_Master._Project_Master.Project_Category_Name = (reader["Project_Category_Name"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Project_Category_Name"]));
                project_Master._Project_Master.b_IsFeasible = (reader["b_IsFeasible"]== DBNull.Value ? 0 : Convert.ToInt32(reader["b_IsFeasible"]));
                project_Master._Project_Master.b_Isselected_project = (reader["b_Isselected_project"] == DBNull.Value ? false : Convert.ToBoolean(reader["b_Isselected_project"]));
                project_Master._Project_Master.s_IRB_No = (reader["s_IRB_No"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_IRB_No"]));
                project_Master._Project_Master.s_Research_IO = (reader["s_Research_IO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Research_IO"]));
                project_Master._Project_Master.s_Research_IP = (reader["s_Research_IP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["s_Research_IP"]));
                project_Master._Project_Master.Project_StartDate = reader["dt_ProjectStartDate"] == DBNull.Value? string.Empty : Convert.ToString(reader["dt_ProjectStartDate"]);
                project_Master._Project_Master.i_ProjectStatus = (reader["i_ProjectStatusID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["i_ProjectStatusID"]));
                project_Master._Project_Master.Dt_ProjectEndDate =  reader["dt_ProjectEndDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["dt_ProjectEndDate"]);
                //project_Master._Project_Master.s_Coinvestigator = (reader["s_Coinvestigator") ? string.Empty : Convert.ToString(reader["s_Coinvestigator"]));
                //project_Master._Project_Master.s_Contract_DataOwner = (reader["DO_Contract") ? string.Empty : Convert.ToString(reader["DO_Contract"]));
                //project_Master._Project_Master.s_Ethics_DataOwner = (reader["DO_Ethics") ? string.Empty : Convert.ToString(reader["DO_Ethics"]));
                //project_Master._Project_Master.s_Grant_DataOwner = (reader["DO_Grant") ? string.Empty : Convert.ToString(reader["DO_Grant"]));
                //project_Master._Project_Master.s_Feasibility_DataOwner = (reader["DO_Feasibility") ? string.Empty : Convert.ToString(reader["DO_Feasibility"]));
                //project_Master._Project_Master.s_Regulatory_DataOwner = (reader["DO_Regulatory") ? string.Empty : Convert.ToString(reader["DO_Regulatory"]));
                // project_Master._Project_Master.s_Selected_DataOwner = (reader["DO_Selected") ? string.Empty : Convert.ToString(reader["DO_Selected"]));


                //project_Master._Project_Master.Dt_ProjectEndDate = (reader["dt_ProjectEndDate") ? string.Empty : Convert.ToString(reader["dt_ProjectEndDate"]));
                //project_Master._Project_Master.b_EthicsNeeded = (reader["b_EthicsNeeded") ? false : Convert.ToBoolean(reader["b_EthicsNeeded"]));






            }
            myConnection.Close();
            return project_Master;

        }

        // POST api/values
        [HttpPost]
        //public void Post([FromBody]string value)
       // public string Post(tblProject_Master _Project_Master, List<Project_Dept_PI> pdi, List<Project_Coordinator_Details> pcd, string mode)
        public string Post([FromBody]ProjectMasterModel pmm)
        {

            string result = string.Empty;
            try { 
            ProjectMasterModel pmm1= pmm as ProjectMasterModel; //Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectMasterModel>(parameters);
            tblProject_Master _Project_Master = pmm1._Project_Master; //Newtonsoft.Json.JsonConvert.DeserializeObject<tblProject_Master>(parameters[0].ToString());
            string mode = pmm.mode;//Newtonsoft.Json.JsonConvert.DeserializeObject(parameters[4].ToString()).ToString();

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=192.168.0.110;Initial Catalog=TTSHTemp;User ID=sa;Password=ROOT#123";
            SqlCommand sqlCmd = new SqlCommand();
            myConnection.Open();
            sqlCmd.Connection = myConnection;
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "spProjectMasterDML";
            sqlCmd.Parameters.Add("@StatementType", SqlDbType.VarChar);
            sqlCmd.Parameters["@StatementType"].Value = mode;

            sqlCmd.Parameters.Add("@i_ID", SqlDbType.Int);
            sqlCmd.Parameters["@i_ID"].Value = _Project_Master.i_ID;

            if (mode.ToString() != "Delete")
            {
                sqlCmd.Parameters.Add("@s_Display_Project_ID", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Display_Project_ID"].Value = _Project_Master.s_Display_Project_ID;

                sqlCmd.Parameters.Add("@s_Project_Title", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Project_Title"].Value = _Project_Master.s_Project_Title;

                sqlCmd.Parameters.Add("@s_Short_Title", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Short_Title"].Value = _Project_Master.s_Short_Title;

                sqlCmd.Parameters.Add("@i_Project_Category_ID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Project_Category_ID"].Value = _Project_Master.i_Project_Category_ID;

                sqlCmd.Parameters.Add("@i_Project_Subtype_ID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Project_Subtype_ID"].Value = _Project_Master.i_Project_Subtype_ID;

                sqlCmd.Parameters.Add("@b_Collaboration_Involved", SqlDbType.Bit);
                sqlCmd.Parameters["@b_Collaboration_Involved"].Value = _Project_Master.b_Collaboration_Involved;

                sqlCmd.Parameters.Add("@b_StartBy_TTSH", SqlDbType.Bit);
                sqlCmd.Parameters["@b_StartBy_TTSH"].Value = _Project_Master.b_StartBy_TTSH;

                sqlCmd.Parameters.Add("@i_Project_Type_ID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Project_Type_ID"].Value = _Project_Master.i_Project_Type_ID;

                sqlCmd.Parameters.Add("@b_Funding_req", SqlDbType.Bit);
                sqlCmd.Parameters["@b_Funding_req"].Value = _Project_Master.b_Funding_req;

                sqlCmd.Parameters.Add("@b_Ischild", SqlDbType.Bit);
                sqlCmd.Parameters["@b_Ischild"].Value = _Project_Master.b_Ischild;

                sqlCmd.Parameters.Add("@i_Parent_ProjectID", SqlDbType.Int);
                sqlCmd.Parameters["@i_Parent_ProjectID"].Value = _Project_Master.i_Parent_ProjectID;

                sqlCmd.Parameters.Add("@s_Project_Alias1", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Project_Alias1"].Value = _Project_Master.s_Project_Alias1;

                sqlCmd.Parameters.Add("@s_Project_Alias2", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Project_Alias2"].Value = _Project_Master.s_Project_Alias2;

                sqlCmd.Parameters.Add("@s_Project_Desc", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Project_Desc"].Value = _Project_Master.s_Project_Desc;

                sqlCmd.Parameters.Add("@b_IsFeasible", SqlDbType.Int);
                sqlCmd.Parameters["@b_IsFeasible"].Value = _Project_Master.b_IsFeasible;

                sqlCmd.Parameters.Add("@b_Isselected_project", SqlDbType.Bit);
                sqlCmd.Parameters["@b_Isselected_project"].Value = _Project_Master.b_Isselected_project;

                sqlCmd.Parameters.Add("@s_IRB_No", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_IRB_No"].Value = _Project_Master.s_IRB_No;

                sqlCmd.Parameters.Add("@s_Research_IO", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Research_IO"].Value = _Project_Master.s_Research_IO;

                sqlCmd.Parameters.Add("@s_Research_IP", SqlDbType.VarChar);
                sqlCmd.Parameters["@s_Research_IP"].Value = _Project_Master.s_Research_IP;

                sqlCmd.Parameters.Add("@Project_Dept_PI1", SqlDbType.VarChar);
                sqlCmd.Parameters["@Project_Dept_PI1"].Value =  string.Join(",", pmm1.pdi.Select(x => x.i_PI_ID)).ToString();

                sqlCmd.Parameters.Add("@i_ProjectStatusId", SqlDbType.Int);
                sqlCmd.Parameters["@i_ProjectStatusId"].Value = _Project_Master.i_ProjectStatus;


                    sqlCmd.Parameters.Add("@Dt_ProjectEndDate", SqlDbType.VarChar);
                    sqlCmd.Parameters["@Dt_ProjectEndDate"].Value = _Project_Master.Dt_ProjectEndDate;

                    sqlCmd.Parameters.Add("@dt_ProjectStartDate1", SqlDbType.VarChar);
                    sqlCmd.Parameters["@dt_ProjectStartDate1"].Value = _Project_Master.Project_StartDate;
       
                    //parameter[parameter.Count - 1].Value = _Project_Master.Project_StartDate;
                    //parameter[parameter.Count - 1].ParameterName = "@Dt_ProjectEndDate";
                    //parameter[parameter.Count - 1].Value = _Project_Master.Dt_ProjectEndDate;

                    //----------Added by Atul
                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@UserCID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.UID;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@Username";
                    //parameter[parameter.Count - 1].Value = _Project_Master.UName;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@i_ProjectStatusId";
                    //parameter[parameter.Count - 1].Value = _Project_Master.i_ProjectStatus;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@Dt_ProjectEndDate";
                    //parameter[parameter.Count - 1].Value = _Project_Master.Dt_ProjectEndDate;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@b_EthicsNeeded";
                    //parameter[parameter.Count - 1].Value = _Project_Master.b_EthicsNeeded;
                    ////----------End by Atul
                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@dt_ProjectStartDate ";
                    //parameter[parameter.Count - 1].Value = _Project_Master.Project_StartDate;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                    //parameter[parameter.Count - 1].Value = pdi.ListToDatatable().getColumns(1);

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@Project_Coordinator_Details";
                    //parameter[parameter.Count - 1].Value = pcd.ListToDatatable();

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Ethics_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Ethics_DataOwner;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Feasibility_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Feasibility_DataOwner;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Selected_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Selected_DataOwner;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Regulatory_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Regulatory_DataOwner;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Contract_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Contract_DataOwner;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_Grant_DO_ID";
                    //parameter[parameter.Count - 1].Value = _Project_Master.s_Grant_DataOwner;

                }
                sqlCmd.Parameters.Add("@Ret_Parameter", SqlDbType.VarChar);
            sqlCmd.Parameters["@Ret_Parameter"].Size = 500;
            sqlCmd.Parameters["@Ret_Parameter"].Direction = ParameterDirection.Output;
            sqlCmd.ExecuteNonQuery();
            if (Int32.Parse(sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString())>0)
            {
                result = "Success" + " | " + sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            else
            {
                result = sqlCmd.Parameters[sqlCmd.Parameters.Count - 1].Value.ToString();
            }
            myConnection.Close();
            }
            catch(Exception ex)
            {

            }
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
        //public  DataTable ListToDatatable<T>(this List<T> inputlist)
        //{
            
        //    DataTable dt = new DataTable();
        //    foreach (PropertyInfo item in typeof(T).GetProperties())
        //    {
        //        dt.Columns.Add(new DataColumn(item.Name, item.PropertyType));
        //    }
        //    foreach (T t in inputlist)
        //    {
        //        DataRow dr = dt.NewRow();
        //        foreach (PropertyInfo item in typeof(T).GetProperties())
        //        {
        //            dr[item.Name] = item.GetValue(t, null);
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
    }
}
