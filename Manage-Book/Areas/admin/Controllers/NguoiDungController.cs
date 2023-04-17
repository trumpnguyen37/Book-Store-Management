using CuoiKi.Areas.admin.Convert;
using CuoiKi.Areas.admin.Models;
using CuoiKi.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CuoiKi.Areas.admin.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: admin/NguoiDung
        public ActionResult Index(string SearchString)
        {
            ConvertQuyenToXml();
            ConvertNguoiDungToXml();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/NguoiDung.xml"));
            List<NguoiDung> list = new List<NguoiDung>();

            XmlDocument xmlDocQuyen = new XmlDocument();
            xmlDocQuyen.Load(Server.MapPath("~/App_Data/Quyen.xml"));

            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("NguoiDung"))
            {
                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.id = ele.GetAttribute("id");
                nguoiDung.tenTK = ele.GetAttribute("tenTaiKhoan");
                nguoiDung.matKhau = ele.GetAttribute("matKhau");
                nguoiDung.ten = ele.GetAttribute("ten");
                string maQuyen = ele.GetAttribute("maQuyen");
                nguoiDung.maQuyen = maQuyen;
                foreach (XmlElement e in xmlDocQuyen.GetElementsByTagName("Quyen"))
                {
                    if (e.GetAttribute("id").ToString() == maQuyen)
                    {
                        nguoiDung.quyen = e.GetAttribute("tenQuyen").ToString();
                    }
                }
                if (ele.GetAttribute("trangThai").ToString() == "1")
                {
                    nguoiDung.trangThai = true;
                }
                else
                {
                    nguoiDung.trangThai = false;
                }

                list.Add(nguoiDung);
            }

            List<NguoiDung> ListFind = new List<NguoiDung>();
            if (SearchString != null)
            {
                foreach(NguoiDung nd in list)
                {
                    if (nd.ten.ToLower().Contains(SearchString.ToLower()))
                    {
                        ListFind.Add(nd);
                    }
                }
                return View(ListFind);
            }
            return View(list);
        }
        public void ConvertNguoiDungToXml()
        {
            NguoiDungConverter nguoiDungConverter = new NguoiDungConverter();
            string xml = nguoiDungConverter.toXMl();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(Server.MapPath("~/App_Data/NguoiDung.xml"));
        }
        public void ConvertQuyenToXml()
        {
            QuyenConverter quyenConverter = new QuyenConverter();
            String xml = quyenConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/Quyen.xml"));

        }


        public void setViewBag(string i = null)
        {
            XmlDocument xmlDocQuyen = new XmlDocument();
            xmlDocQuyen.Load(Server.MapPath("~/App_Data/Quyen.xml"));
            var listQuyen = xmlDocQuyen.GetElementsByTagName("Quyen");
            List<Quyen> List = new List<Quyen>();
            foreach (XmlElement ele in listQuyen)
            {
                Quyen quyen = new Quyen();
                quyen.id = ele.GetAttribute("id").ToString();
                quyen.tenQuyen = ele.GetAttribute("tenQuyen").ToString();
                List.Add(quyen);
            }
            ViewBag.quyen = new SelectList(List, "id", "tenQuyen", i);
        }
        public ActionResult Edit(string id)
        {
            
            NguoiDung model = new NguoiDung();
       
            ConvertNguoiDungToXml();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/NguoiDung.xml"));
            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("NguoiDung"))
            {
                if (ele.GetAttribute("id").ToString() == id)
                {
                    model.tenTK = ele.GetAttribute("tenTaiKhoan").ToString();
                    model.matKhau = ele.GetAttribute("matKhau").ToString();
                    model.ten = ele.GetAttribute("ten").ToString();
                    model.diaChi = ele.GetAttribute("diaChi").ToString();
                    model.cmnd = ele.GetAttribute("CMND").ToString(); ;
                    model.email = ele.GetAttribute("email").ToString(); ;
                    model.sdt = ele.GetAttribute("SDT").ToString();
                    model.maQuyen = ele.GetAttribute("maQuyen").ToString(); ;
                    if (ele.GetAttribute("gioiTinh").ToString() == "1")
                    {
                        model.gioiTinh = "Nam";
                    }
                    else
                    {
                        model.gioiTinh = "Nữ";
                    }
                    if(ele.GetAttribute("trangThai").ToString() == "1")
                    {
                        model.trangThai = true;
                    }
                    else{
                        model.trangThai = false;
                    }

                }
            }

            setViewBag();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(NguoiDung nguoiDung)
        {

            if (ModelState.IsValid)
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/NguoiDung.xml"));
                foreach (XmlElement ele in xmlDoc.GetElementsByTagName("NguoiDung"))
                {
                    if (ele.GetAttribute("id").ToString() == nguoiDung.id)
                    {
                        ele.SetAttribute("tenTaiKhoan", nguoiDung.tenTK);
                        ele.SetAttribute("matKhau", nguoiDung.matKhau);
                        ele.SetAttribute("maQuyen", nguoiDung.maQuyen);
                        ele.SetAttribute("ten", nguoiDung.ten);
                        ele.SetAttribute("diaChi", nguoiDung.diaChi);
                        ele.SetAttribute("SDT", nguoiDung.sdt);
                        ele.SetAttribute("email", nguoiDung.email);
                        ele.SetAttribute("CMND", nguoiDung.cmnd);
       
                        if (nguoiDung.trangThai)
                        {
                            ele.SetAttribute("trangThai", "1");
                        }
                        else
                        {
                            ele.SetAttribute("trangThai", "0");
                        }
                        xmlDoc.Save(Server.MapPath("~/App_Data/NguoiDung.xml"));
                        UpdateToSQL(nguoiDung.id);
                        break;
                    }
                }
                return RedirectToAction("Index", "NguoiDung");
            }
            setViewBag();
            return View();
        }
 
      
        void UpdateToSQL(string id)
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/NguoiDung.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == id)
                {
                    sql = "update NguoiDung set ten = N'" + dataRow["ten"] + "', diaChi = N'" + dataRow["diaChi"] + "', SDT = '" + dataRow["SDT"] + "', email = '" + dataRow["email"] + "', " +
                        "CMND = '" + dataRow["CMND"] + "', matKhau = '" + dataRow["matKhau"] + "', maQuyen = " + dataRow["maQuyen"] + ", trangThai = " + dataRow["trangThai"] + "where id = " + id;
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
            

            }

        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(NguoiDung nguoiDung)
        {
            
            if (ModelState.IsValid)
            {

                //thêm tài khoản
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/NguoiDung.xml"));
                XmlElement ele = xmlDoc.CreateElement("NguoiDung");
                ele.SetAttribute("id", "");
                ele.SetAttribute("tenTaiKhoan", nguoiDung.tenTK);
                ele.SetAttribute("matKhau", nguoiDung.matKhau);
                ele.SetAttribute("maQuyen", nguoiDung.maQuyen);
                ele.SetAttribute("ten", nguoiDung.ten);
                ele.SetAttribute("diaChi", nguoiDung.diaChi);
                ele.SetAttribute("SDT", nguoiDung.sdt);
                ele.SetAttribute("email", nguoiDung.email);

                ele.SetAttribute("CMND", nguoiDung.cmnd);
                ele.SetAttribute("trangThai", "1");

                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(Server.MapPath("~/App_Data/NguoiDung.xml"));

                ToNguoiDungSQL();
                return RedirectToAction("Index", "NguoiDung");
            }
            setViewBag();
            return View();

        }

        public void ToNguoiDungSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/NguoiDung.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == "")
                {
                    sql = "insert into NguoiDung(tenTaiKhoan, matKhau, maQuyen, ten, diaChi, SDT,  email, CMND, trangThai)" +
                        " values (N'" + dataRow["tenTaiKhoan"] + "', '" + dataRow["matKhau"] + "', " + dataRow["maQuyen"] +
                        ", N'" + dataRow["ten"] + "', N'" + dataRow["diaChi"] + "', '" + dataRow["SDT"] + "', '" + dataRow["email"] + "', '" + dataRow["CMND"] +
                        "', " + dataRow["trangThai"] + ")";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }

        }


    }
}