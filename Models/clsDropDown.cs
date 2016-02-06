using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class clsDropDown
    {
            public string DisplayField { get; set; }

            public string ValueField { get; set; }

    }
    public enum DropDownName
    {

        PI = 1,

        PI_Department = 2,

        Project_Status = 3,

        IRB_Type = 4,

        IRB_Status = 5,

        Project_Category = 6,

        Project_Type = 7,

        Fesibility_Status = 8,

        Drug_Location = 9,

        Project_Sub_Types = 10,

        Mode_of_Notification = 11,

        Ethics_Review = 12,
       
        Clinical_Research = 13,
       
        Study_Status = 14,
       
        PI_Details = 15,

        Country = 16,
       
        Collaborator = 17,
       
        FillSubType,
       
        Project_Name,
       
        FillClauses,
       
        FillCollaborators,
       
        Contract_Category,
       
        Contract_Status,
       
        GetAllCollaboratorForProject,
       
        ProjectFeasibility,
       
        Coordinators,
       
        Cupboad_Number,
       
        LeadSponsor,
       
        CTCstatus,
       
        RegularoryStatusReportFor,
       
        RegulatoryIPStorage,
       
        AuditModules,
       
        AuditModulesFields,
       
        MenuMapping,
       
        GetYear,
        DocumentCategory,
       
        DataOwner,
       
        GrantType,
       
        GrantSubType1,
       
        GrantSubType2,
       
        GrantSubType3,
       
        GrantSubmissionStatus,
       
        GrantOutCome,
       
        GrantAwardingOrganization,
       
        GrantDuration,

        Selected_Years,

        Period_of_Insurance,
       
        GrantDetailStatus


    }
}
