using CuoiKi.Areas.admin.Convert;
using CuoiKi.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CuoiKi.Areas.admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: admin/DonHang
        public ActionResult Index()
        {
            if (!Directory.Exists(Server.MapPath("~/App_Data/HoaDon.xml")))
            {
                ConvertHoaDonToXml();
                ConvertNguoiDungToXml();
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/HoaDon.xml"));

            //lấy danh sách người dùng để lấy tên KH
            XmlDocument xmlNguoiDung = new XmlDocument();
            xmlNguoiDung.Load(Server.MapPath("~/App_Data/NguoiDung.xml"));
            string tenKH = "";
            

                List<HoaDon> List = new List<HoaDon>();
            foreach (XmlElement ele in xml.GetElementsByTagName("HoaDon"))
            {
                HoaDon hoaDon = new HoaDon();
                hoaDon.id = int.Parse(ele.GetAttribute("id"));
                foreach (XmlElement e in xmlNguoiDung.GetElementsByTagName("NguoiDung"))
                {
                    if (e.GetAttribute("id") == ele.GetAttribute("maKH"))
                    {
                        hoaDon.tenKH = e.GetAttribute("ten");
                        break;
                    }
                }
                hoaDon.maKH = int.Parse(ele.GetAttribute("maKH"));
                hoaDon.ngayNhap = DateTime.Parse(ele.GetAttribute("ngayNhap"));
                List.Add(hoaDon);
            }
            return View(List);
        }

        public void ConvertHoaDonToXml()
        {
            HoaDonConverter hoaDonConverter = new HoaDonConverter();
            String xml = hoaDonConverter.toXMl();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(Server.MapPath("~/App_Data/HoaDon.xml"));

        }

        public void ConvertNguoiDungToXml()
        {
            NguoiDungConverter nguoiDungConverter = new NguoiDungConverter();
            string xml = nguoiDungConverter.toXMl();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(Server.MapPath("~/App_Data/NguoiDung.xml"));
        }

        public void ConvertChiTietHoaDonToXml()
        {
            ChiTietHoaDonConverter chiTietHoaDonConverter = new ChiTietHoaDonConverter();
            string xml = chiTietHoaDonConverter.toXMl();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(Server.MapPath("~/App_Data/ChiTietHoaDon.xml"));
        }

        public ActionResult Details(int id)
        {
            ConvertChiTietHoaDonToXml();
            List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/ChiTietHoaDon.xml"));

            XmlDocument xmlDocSP = new XmlDocument();
            xmlDocSP.Load(Server.MapPath("~/App_Data/SanPham.xml"));

            foreach (XmlElement ele in xml.GetElementsByTagName("ChiTietHoaDon"))
            {
               
                if(ele.GetAttribute("maHD") == id + "")
                {
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                    foreach (XmlElement e in xmlDocSP.GetElementsByTagName("SanPham"))
                    {
                        if (e.GetAttribute("id") == ele.GetAttribute("maSP"))
                        {
                            chiTietHoaDon.tenSP = e.GetAttribute("tenSanPham");
                            break;
                        }
                    }
                    chiTietHoaDon.id = int.Parse(ele.GetAttribute("id"));
                    chiTietHoaDon.maHD = id;
                    chiTietHoaDon.maSP = int.Parse(ele.GetAttribute("maSP"));
                    chiTietHoaDon.soLuong = int.Parse(ele.GetAttribute("soLuong"));
                    chiTietHoaDon.giaBan = int.Parse(ele.GetAttribute("giaBan"));
                    chiTietHoaDon.thanhTien = chiTietHoaDon.soLuong * chiTietHoaDon.giaBan;
                    list.Add(chiTietHoaDon);
                }
               
            }
            return View(list);
        }
    }
}