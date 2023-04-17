using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{

    public class Connect
    {
        public static SqlConnection connect()
        {
            string strCon = @"Data Source=DESKTOP-8GKPO1M\SQLEXPRESS;Initial Catalog=QuanLySach;Integrated Security=True";
            SqlConnection con = new SqlConnection(strCon);
            return con;
        }
        
    }
}