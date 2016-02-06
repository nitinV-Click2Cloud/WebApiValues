using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace WebApplication4.Models
{
    public static class ModCommon
    {
        //public static DataTable ListToDatatable<T>(this List<T> inputlist)
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
