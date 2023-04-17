using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Convert
{
    public class SanPhamConverter
    {
        SqlDataAdapter da;
        public string toXMl()
        {
            SqlConnection con = Connect.connect();
            string sql = "select * from SanPham for xml auto";
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string xml = "<?xml version='1.0' ?><SanPhams>";
                xml += dt.Rows[0].ItemArray[0].ToString().Trim() + "</SanPhams>";
                return xml;
            }
            return "<?xml version='1.0' ?><SanPhams></SanPhams>";

        }
    }
}