using CuoiKi.Areas.admin.Convert;
using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CuoiKi.Areas.admin.Controllers
{
    public class NhapHangController : Controller
    {
        static string idNCC = "";
        static string idNhap = "";
        // GET: admin/NhapHang
        public ActionResult Index()
        {
            ConvertNCCToXml();
            ConvertChiTietNhapToXml();
            ConvertNhapHangToXml();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/NhaCungCap.xml"));
            List<NhaCungCap> list = new List<NhaCungCap>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/App_Data/NhaCungCap.xml"));

            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("NhaCungCap"))
            {
                NhaCungCap nhaCungCap = new NhaCungCap();
                nhaCungCap.id = ele.GetAttribute("id");
                nhaCungCap.tenNhaCungCap = ele.GetAttribute("tenNhaCungCap");
                nhaCungCap.diaChi = ele.GetAttribute("diaChi");
                nhaCungCap.SDT = ele.GetAttribute("SDT");
                nhaCungCap.email = ele.GetAttribute("email");
               
                list.Add(nhaCungCap);
            }
            return View(list);
        }

        public void ConvertNCCToXml()
        {
            NhaCungCapConverter nhaCungCapConverter = new NhaCungCapConverter();
            String xml = nhaCungCapConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/NhaCungCap.xml"));

        }

        public void ConvertChiTietNhapToXml()
        {
            ChiTietNhapConverter chiTietNhapConverter = new ChiTietNhapConverter();
            String xml = chiTietNhapConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/ChiTietNhap.xml"));

        }
        public void ConvertNhapHangToXml()
        {
            NhapHangConverter nhapHangConverter = new NhapHangConverter();
            String xml = nhapHangConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/NhapHang.xml"));

        }

        public ActionResult Details(string id)
        {
            NhapHangController.idNCC = id;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/NhapHang.xml"));
            List<NhapHang> list = new List<NhapHang>();
            
            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("NhapHang"))
            {
                NhapHang nhapHang = new NhapHang();
                if (ele.GetAttribute("maNCC") == id)
                {
                    nhapHang.id = ele.GetAttribute("id");
                    nhapHang.maNCC = ele.GetAttribute("maNCC");
                    nhapHang.ngayNhap = DateTime.Parse(ele.GetAttribute("ngayNhap"));
                    
                    list.Add(nhapHang);
                }
               
            }

            //lấy nhà cung cấp
            XmlDocument xmlDocNCC = new XmlDocument();
            xmlDocNCC.Load(Server.MapPath("~/App_Data/NhaCungCap.xml"));
            string ncc = "";
            foreach (XmlElement ele in xmlDocNCC.GetElementsByTagName("NhaCungCap"))
            {
                if (ele.GetAttribute("id") == id)
                {
                    ncc = ele.GetAttribute("tenNhaCungCap");
                }
            }
            ViewBag.ncc = ncc;
            
            return View(list);
        }

        public ActionResult Detail_Nhap(string id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/ChiTietNhap.xml"));

            XmlDocument xmlDocSP = new XmlDocument();
            xmlDocSP.Load(Server.MapPath("~/App_Data/SanPham.xml"));

            List<ChiTietNhap> list = new List<ChiTietNhap>();

            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("ChiTietNhap"))
            {
                ChiTietNhap chiTietNhap = new ChiTietNhap();
                if (ele.GetAttribute("maNhap") == id)
                {
                    chiTietNhap.id = ele.GetAttribute("id");
                    chiTietNhap.maNhap = ele.GetAttribute("maNhap");

                    foreach (XmlElement e in xmlDocSP.GetElementsByTagName("SanPham"))
                    {
                        if(e.GetAttribute("id")== ele.GetAttribute("maSP"))
                        {
                            chiTietNhap.tenSP = e.GetAttribute("tenSanPham");
                            break;
                        }
                    }
                    chiTietNhap.maSP = ele.GetAttribute("maSP");
                    chiTietNhap.soLuong = int.Parse(ele.GetAttribute("soLuong"));
                    chiTietNhap.giaNhap = int.Parse(ele.GetAttribute("giaNhap"));
                    chiTietNhap.thanhTien = chiTietNhap.soLuong * chiTietNhap.giaNhap;

    
                    list.Add(chiTietNhap);
                }

            }

            //lấy ngày nhập
            XmlDocument xmlDocNhap = new XmlDocument();
            xmlDocNhap.Load(Server.MapPath("~/App_Data/NhapHang.xml"));
            string ngay = "1";
            foreach (XmlElement ele in xmlDocNhap.GetElementsByTagName("NhapHang"))
            {
                if (ele.GetAttribute("id") == id)
                {
                     ngay = ele.GetAttribute("ngayNhap");
                }
            }
            ViewBag.ngay = ngay;
            ViewBag.ncc = NhapHangController.idNCC;
            return View(list);
        }

        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/NhaCungCap.xml"));
                XmlElement ele = xmlDoc.CreateElement("NhaCungCap");
                ele.SetAttribute("id", "");
                ele.SetAttribute("tenNhaCungCap", ncc.tenNhaCungCap);
                ele.SetAttribute("diaChi", ncc.diaChi);
                ele.SetAttribute("SDT", ncc.SDT);
                ele.SetAttribute("email", ncc.email);
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(Server.MapPath("~/App_Data/NhaCungCap.xml"));
                NhaCungCapToSQL();
                return RedirectToAction("Index", "NhapHang");
            }
            return View();
        }

        public void NhaCungCapToSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/NhaCungCap.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == "")
                {
                    sql = "insert into NhaCungCap(tenNhaCungCap, diaChi, SDT, email) values ('" + dataRow["tenNhaCungCap"] + "', N'" + dataRow["diaChi"] + "','" + dataRow["SDT"] + "','" + dataRow["email"] + "')";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }


        public ActionResult Create_Nhap()
        {
            var today = DateTime.Today.Date;
            var date = today.ToString().Split(' ')[0];
            if (ModelState.IsValid) 
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/NhapHang.xml"));
                XmlElement ele = xmlDoc.CreateElement("NhapHang");
                ele.SetAttribute("id", "");
                ele.SetAttribute("ngayNhap", date + "");
                ele.SetAttribute("maNCC", NhapHangController.idNCC);
               
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(Server.MapPath("~/App_Data/NhapHang.xml"));
                NhapHangToSQL();
                return RedirectToAction("Details", "NhapHang", new { @id = NhapHangController.idNCC });
            }
            return RedirectToAction("Details", "NhapHang", new { @id = NhapHangController.idNCC });
        }

        public void NhapHangToSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/NhapHang.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == "")
                {
                    sql = "insert into NhapHang(ngayNhap, maNCC) values ('" + dataRow["ngayNhap"] + "', " + dataRow["maNCC"] + ")";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }

        public ActionResult Create_chiTietNhap(string id)
        {
            NhapHangController.idNhap = id;
            ViewBag.ct = id;
            ViewBag.ncc = NhapHangController.idNCC;
            return View();
        }
        [HttpPost]
        public ActionResult Create_chiTietNhap(ChiTietNhap chiTietNhap)
        {
            if (ModelState.IsValid)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/App_Data/ChiTietNhap.xml"));
                XmlElement ele = xmlDoc.CreateElement("ChiTietNhap");
                ele.SetAttribute("id", "");
                ele.SetAttribute("maNhap", NhapHangController.idNhap);
                ele.SetAttribute("maSP", chiTietNhap.maSP);
                ele.SetAttribute("soLuong", chiTietNhap.soLuong+"");
                ele.SetAttribute("giaNhap", chiTietNhap.giaNhap+"");

                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(Server.MapPath("~/App_Data/ChiTietNhap.xml"));
                ChiTietNhapToSQL();
                return RedirectToAction("Details", "NhapHang", new { @id = NhapHangController.idNCC });
            }
            return View();
        }

        [HttpPost]
        public JsonResult LoadDanhMuc()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/DanhMuc.xml"));
            List<DanhMuc> listDM = new List<DanhMuc>();
            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("DanhMuc"))
            {
                DanhMuc danhMuc = new DanhMuc();
                danhMuc.id = ele.GetAttribute("id").ToString();
                danhMuc.tenDanhMuc = ele.GetAttribute("tenDanhMuc").ToString();
                listDM.Add(danhMuc);
            }
            return Json(new{
                data = listDM,
                status = true
            });
        }

        [HttpPost]
        public JsonResult LoadSanPham(int maDM)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/App_Data/SanPham.xml"));
            List<SanPham> listSP = new List<SanPham>();
            foreach (XmlElement ele in xmlDoc.GetElementsByTagName("SanPham"))
            {
                if (ele.GetAttribute("maDM") == maDM + "")
                {
                    SanPham sanPham = new SanPham();
                    sanPham.id = ele.GetAttribute("id").ToString();
                    sanPham.tenSanPham = ele.GetAttribute("tenSanPham").ToString();
                    sanPham.maDM = maDM+"";
                    listSP.Add(sanPham);
                }
                
            }
            return Json(new {
                data = listSP,
                status = true
            });
        }

        public void ChiTietNhapToSQL()
        {
            DataTable dt = new DataTable();
            string filepath = Server.MapPath("~/App_Data/ChiTietNhap.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(filepath);
            DataView dv = new DataView(ds.Tables[0]);
            dt = dv.Table;
            string sql;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["id"].ToString() == "")
                {
                    sql = "insert into ChiTietNhap(maNhap, maSP, soLuong, giaNhap) values (" + dataRow["maNhap"] + ", " + dataRow["maSP"] + ", " 
                        + dataRow["soLuong"] + ", " + dataRow["giaNhap"] + ")";
                    XmlToSQL.InsertOrUpDateSQL(sql);
                }
            }
        }

    }
}