using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class ProjectMasterModel
    {
        public tblProject_Master _Project_Master { get; set; }
        public List<Project_Dept_PI> pdi { get; set; }
        public List<Project_Coordinator_Details> pcd { get; set; }
        public List<PI_Master> DEPT_PI { get; set; }
        public string mode { get; set; }
    }
}
