using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Convert
{
    public class NguoiDungConverter
    {
        SqlDataAdapter da;
        public string toXMl()
        {
            SqlConnection con = Connect.connect();
            string sql = "select * from NguoiDung for xml auto";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string xml = "<?xml version='1.0'?><NguoiDungs>";
                xml += dt.Rows[0].ItemArray[0].ToString().Trim() + "</NguoiDungs>";
                return xml;
            }
            return "<?xml version='1.0'?><NguoiDungs></NguoiDungs>";
            

        }
    }
}