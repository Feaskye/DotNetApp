using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECIT.TBarCode;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BarCodeEncoder _Code = new BarCodeEncoder();
            _Code.ValueFont = new Font("宋体", 100);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage("PH201606231026418896", BarCodeEncoder.Encode.Code128C);
            var imgPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + "BarCode.gif";
            imgTemp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Gif);

            return View();
        }



        public ActionResult About()
        {
            Code39 _Code39 = new Code39();

            _Code39.Height = 70;


            System.Drawing.Image _CodeImage = _Code39.GetCodeImage("PH201606231026418896", Code39.Code39Model.Code39Normal, true);

            System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
            _CodeImage.Save(_Stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _CodeImage.Save(Server.MapPath(@"\1.jpeg"));

            using (FileStream localFile = new FileStream(Server.MapPath(@"\streamTest.jpeg"),
           FileMode.OpenOrCreate))
            {
                //ms.ToArray()转换为字节数组就是想要的图片源字节

                localFile.Write(_Stream.ToArray(), 0, (int)_Stream.Length);
            }


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
            Barcode barcode = new Barcode();
               barcode.License("John Smith", LicenseType.DeveloperOrWeb, 1, "00000000",
 TBarCodeProduct.Barcode1D); // 1D License
//barcode.License("John Smith", LicenseType.DeveloperOrWeb, 1, "00000000",
// TBarCodeProduct.Barcode2D); // 2D License

//                barcode.Licensee = "John Smith";
//barcode.LicenseType = LicenseType.DeveloperOrWeb;
//barcode.LicenseCount = 1;
//barcode.LicenseKey = "00000000";
//barcode.LicensedProduct = TBarCodeProduct.Barcode1D;


            return View();
        }
    }
}