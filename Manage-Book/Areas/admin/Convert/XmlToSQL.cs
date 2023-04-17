using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Convert
{
    public class XmlToSQL
    {
        public static void InsertOrUpDateSQL(string sql)
        {
            SqlConnection con = Connect.connect();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}