using CuoiKi.Areas.admin.Convert;
using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CuoiKi.Areas.admin.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: admin/DanhMuc
        public ActionResult Index(string SearchString)
        {
            if (!Directory.Exists(Server.MapPath("~/App_Data/DanhMuc.xml")))
            {
                ConvertDanhMucToXml();
            }
            
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
            List<DanhMuc> List = new List<DanhMuc>();
            foreach(XmlElement ele in xml.GetElementsByTagName("DanhMuc"))
            {
                DanhMuc danhMuc = new DanhMuc();
                danhMuc.id = ele.GetAttribute("id");
                danhMuc.tenDanhMuc = ele.GetAttribute("tenDanhMuc");
                List.Add(danhMuc);
            }
            List<DanhMuc> ListFind = new List<DanhMuc>();
            if (SearchString != null)
            {
                foreach (DanhMuc dm in List)
                {
                    if (dm.tenDanhMuc.ToLower().Contains(SearchString.ToLower()))
                    {
                        ListFind.Add(dm);
                    }
                }
                return View(ListFind);
            }
            return View(List);
        }
        public void ConvertDanhMucToXml()
        {
            DanhMucConverter quyenConverter = new DanhMucConverter();
            String xml = quyenConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/DanhMuc.xml"));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
                XmlElement ele = xml.CreateElement("DanhMuc");
                ele.SetAttribute("id", "");
                ele.SetAttribute("tenDanhMuc", danhMuc.tenDanhMuc);
                xml.DocumentElement.AppendChild(ele);
                xml.Save(Server.MapPath("~/App_Data/DanhMuc.xml"));
                ToSQL();
                return RedirectToAction("Index", "DanhMuc");
                
               
            }
            return View();
        }

        public void ToSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/DanhMuc.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == "")
                {
                    sql = "insert into DanhMuc(tenDanhMuc) values ('" + dataRow["tenDanhMuc"] + "')";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }

        public ActionResult Edit(string id)
        {
            DanhMuc danhMuc = new DanhMuc();
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
            foreach (XmlElement ele in xml.GetElementsByTagName("DanhMuc"))
            {
                if (ele.GetAttribute("id") == id)
                {
                    danhMuc.id = id;
                    danhMuc.tenDanhMuc = ele.GetAttribute("tenDanhMuc");
                }
            }
            return View(danhMuc);
        }

        [HttpPost]
        public ActionResult Edit(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
                foreach (XmlElement ele in xml.GetElementsByTagName("DanhMuc"))
                {
                    if (ele.GetAttribute("id") == danhMuc.id)
                    {
                        ele.SetAttribute("tenDanhMuc", danhMuc.tenDanhMuc);
                        xml.Save(Server.MapPath("~/App_Data/DanhMuc.xml"));
                        UpdateToSQL(danhMuc.id);
                        break;
                    }
                }
                return RedirectToAction("Index", "DanhMuc");
            }
            return View();
        }

        void UpdateToSQL(string id)
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/DanhMuc.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == id)
                {
                    sql = "update DanhMuc set tenDanhMuc = N'" + dataRow["tenDanhMuc"]  + "'where id = " + id;
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }


        }
    }
}