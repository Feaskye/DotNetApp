using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BarCodeEncoder _Code = new BarCodeEncoder();
            _Code.ValueFont = new Font("宋体", 20);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage("PH201604191233106501", BarCodeEncoder.Encode.Code128B);
            var imgPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + "BarCode.gif";
            imgTemp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Gif);
            return View();
        }



        public ActionResult About()
        {
            Code39 _Code39 = new Code39();

            _Code39.Height = 120;
            _Code39.Magnify = 1;
            _Code39.ViewFont = new Font("宋体", 20);


            System.Drawing.Image _CodeImage = _Code39.GetCodeImage("PH201604191233106501", Code39.Code39Model.Code39Normal, true);

            System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
            _CodeImage.Save(_Stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _CodeImage.Save(Server.MapPath(@"\1.jpeg"));
            //Response.ContentType = "image/jpeg";
            //Response.Clear();
            //Response.BufferOutput = true;
            //Response.BinaryWrite(_Stream.GetBuffer());
            //Response.Flush();

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}