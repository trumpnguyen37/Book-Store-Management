using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CuoiKi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult tessssst()
        {
            //string medalsXML = System.IO.File.ReadAllText(Server.MapPath("App_Data/test.xml"));
            //ViewBag.Medals = medalsXML;
            List<test> str = new List<test>();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/App_Data/test.xml"));
            foreach(XmlElement ele in xdoc.GetElementsByTagName("BBB"))
            {
                test t = new test();
                t.s = ele.InnerText;
                str.Add(t);
            }

            return View(str);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}