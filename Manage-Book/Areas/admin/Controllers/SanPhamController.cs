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
    public class SanPhamController : Controller
    {
        // GET: admin/SanPham
        public ActionResult Index(string SearchString)
        {
            
            ConvertSanPhamToXml();
            ConvertDanhMucToXml();

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/SanPham.xml"));

            XmlDocument xmlDM = new XmlDocument();
            xmlDM.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));

            List<SanPham> List = new List<SanPham>();
            foreach (XmlElement ele in xml.GetElementsByTagName("SanPham"))
            {
                if (ele.GetAttribute("tinhTrang") == "1")
                {
                    SanPham sanPham = new SanPham();
                    foreach (XmlElement e in xmlDM.GetElementsByTagName("DanhMuc"))
                    {
                        if(e.GetAttribute("id")== ele.GetAttribute("maDM"))
                        {
                            sanPham.tenDanhMuc = e.GetAttribute("tenDanhMuc");
                        }
                    }
                        
                    sanPham.id = ele.GetAttribute("id");
                    sanPham.tenSanPham = ele.GetAttribute("tenSanPham");
                    sanPham.moTa = ele.GetAttribute("mota");
                    sanPham.soLuong = int.Parse(ele.GetAttribute("soLuong"));
                    sanPham.gia = int.Parse(ele.GetAttribute("gia"));
                    sanPham.maDM = ele.GetAttribute("maDM");
                    sanPham.anh = ele.GetAttribute("hinhAnh");

                    List.Add(sanPham);
                }
                
            }
            List<SanPham> ListFind = new List<SanPham>();
            if (SearchString != null)
            {
                foreach (SanPham sp in List)
                {
                    if (sp.tenSanPham.ToLower().Contains(SearchString.ToLower()))
                    {
                        ListFind.Add(sp);
                    }
                }
                return View(ListFind);
            }
            
            return View(List);
        }



        public void ConvertSanPhamToXml()
        {
            SanPhamConverter quyenConverter = new SanPhamConverter();
            String xml = quyenConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/SanPham.xml"));

        }
        public void setViewBag(string i = null)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
            var listDM = xdoc.GetElementsByTagName("DanhMuc");
            List<DanhMuc> List = new List<DanhMuc>();
            foreach (XmlElement ele in listDM)
            {
                DanhMuc danhMuc = new DanhMuc();
                danhMuc.id = ele.GetAttribute("id").ToString();
                danhMuc.tenDanhMuc = ele.GetAttribute("tenDanhMuc").ToString();
                List.Add(danhMuc);
            }
            ViewBag.danhMuc = new SelectList(List, "id", "tenDanhMuc", i);
        }
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham sp, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(Server.MapPath("~/App_Data/SanPham.xml"));
                XmlElement ele = xdoc.CreateElement("SanPham");
                ele.SetAttribute("id", "");
                ele.SetAttribute("tenSanPham", sp.tenSanPham);
                ele.SetAttribute("maDM", sp.maDM);
                ele.SetAttribute("mota", sp.moTa);
                ele.SetAttribute("gia", "" + sp.gia);
                ele.SetAttribute("soLuong", "" + sp.soLuong);
                ele.SetAttribute("tinhTrang", "1");

                string returnImagePath = string.Empty;
                string fileName, fileExtension, imaageSavePath, name = "";
                if (file != null && file.ContentLength > 0)
                {
                    
                    fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileExtension = Path.GetExtension(file.FileName);

                    imaageSavePath = Server.MapPath("~/uploadedImages/") + fileName + fileExtension;
                    //Save file
                    file.SaveAs(imaageSavePath);
                    name = fileName + fileExtension;
                    
                }
                ele.SetAttribute("hinhAnh", name);
                xdoc.DocumentElement.AppendChild(ele);
                xdoc.Save(Server.MapPath("~/App_Data/SanPham.xml"));
                ToSQL();
                return RedirectToAction("Index", "SanPham");
            }
            return View();
        }

        public void ToSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/SanPham.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;

            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"] == "")
                {
                    sql = "insert into SanPham(tenSanPham,mota, maDM, soLuong, gia, tinhTrang, hinhAnh) values (N'" + dataRow["tenSanPham"] + "',N'" + dataRow["moTa"] + "'," + dataRow["maDM"] +","+ dataRow["soLuong"] + ","+ dataRow["gia"] + "," + dataRow["tinhTrang"]+ ",'" + dataRow["hinhAnh"] + "')";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }

        public ActionResult Edit(string id)
        {

            setViewBag();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/App_Data/SanPham.xml"));
            SanPham model = new SanPham();
            foreach(XmlElement ele in xdoc.GetElementsByTagName("SanPham")){
                if (ele.GetAttribute("id") == id)
                {
                    model.id = id;
                    model.maDM = ele.GetAttribute("maDM");
                    model.tenSanPham = ele.GetAttribute("tenSanPham");
                    model.moTa = ele.GetAttribute("mota");
                    model.gia = int.Parse(ele.GetAttribute("gia"));
                    model.soLuong = int.Parse(ele.GetAttribute("soLuong"));
                    model.anh = ele.GetAttribute("hinhAnh");

                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase file)
        {
            setViewBag();
            if (ModelState.IsValid)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(Server.MapPath("~/App_Data/SanPham.xml"));
                foreach (XmlElement ele in xdoc.GetElementsByTagName("SanPham"))
                {
                    if (ele.GetAttribute("id") == sp.id)
                    {
                        
                        ele.SetAttribute("tenSanPham", sp.tenSanPham);
                        ele.SetAttribute("maDM", sp.maDM);
                        ele.SetAttribute("gia", sp.gia + "");
                        ele.SetAttribute("soLuong", sp.soLuong + "");
                        ele.SetAttribute("mota", sp.moTa);

                        string returnImagePath = string.Empty;
                        string fileName, fileExtension, imaageSavePath;
                        string nameedit = "";
                        if (file != null && file.ContentLength > 0)
                        {
                            
                            fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileExtension = Path.GetExtension(file.FileName);

                            imaageSavePath = Server.MapPath("~/uploadedImages/") + fileName + fileExtension;
                            //Save file
                            file.SaveAs(imaageSavePath);
                            nameedit = fileName + fileExtension;
                            ele.SetAttribute("hinhAnh", nameedit);
                        }
                  
                        

                        xdoc.Save(Server.MapPath("~/App_Data/SanPham.xml"));
                        UpdateToSQL(sp.id);
                        break;
                    }
                }
                return RedirectToAction("Index", "SanPham");
            }
            return View();
        }

        public void UpdateToSQL(string id)
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/SanPham.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == id)
                {
                    sql = "update SanPham set tenSanPham = N'"+ dataRow["tenSanPham"] + "',moTa=N'" + dataRow["moTa"] + "'  ,maDM = " + dataRow["maDM"] + ", soLuong = "+ dataRow["soLuong"] + ", gia = "+ dataRow["gia"] + ", tinhTrang = " + dataRow["tinhTrang"] + ", hinhAnh = '" + dataRow["hinhAnh"] + "' where id = " + id ;
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/App_Data/SanPham.xml"));
            foreach (XmlElement ele in xdoc.GetElementsByTagName("SanPham"))
            {
                if (ele.GetAttribute("id") == id)
                {
                    ele.SetAttribute("tinhTrang", "0");
                    xdoc.Save(Server.MapPath("~/App_Data/SanPham.xml"));
                    UpdateToSQL(id);
                    break;
                }
            }
            
            return RedirectToAction("Index", "SanPham");
        }



        //public void UpdateSizeToSQL(string id)
        //{
        //    DataTable dt = new DataTable();
        //    string filepath = Server.MapPath("~/App_Data/Size.xml");
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(filepath);
        //    DataView dv = new DataView(ds.Tables[0]);
        //    dt = dv.Table;
        //    string sql;
        //    foreach (DataRow dataRow in dt.Rows)
        //    {
        //        if (dataRow["id"].ToString() == id)
        //        {
        //            sql = "update Size set tinhTrang = " + dataRow["tinhTrang"] + "where id = " + id;
        //            XmlToSQL.InsertOrUpDateSQL(sql);
        //        }
        //    }
        //}

        public void ConvertDanhMucToXml()
        {
            DanhMucConverter quyenConverter = new DanhMucConverter();
            String xml = quyenConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/DanhMuc.xml"));

        }
    }
}