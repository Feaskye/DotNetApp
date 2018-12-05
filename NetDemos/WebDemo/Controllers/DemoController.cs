////using BarcodeLib.Barcode.ASP.NET;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

//namespace WebDemo.Controllers
//{
//    /// <summary>
//    /// 参考网址：http://www.barcodelib.com/csharp/
//    /// http://www.barcodelib.com/net_barcode/main.html
//    /// </summary>
//    public class DemoController : Controller
//    {
//        //
//        // GET: /Demo/
//        public ActionResult Index()
//        {
//            var orderNo = "PH201604191233106501";
//            CreateBarCode(orderNo, "~/barcode001.png");
//            CreateQrCode(orderNo, "~/qrcode001.png");
//            return View();
//        }

//        private void CreateBarCode(string text,string path)
//        {
//            // Create an linear barcode object (BarcodeLib.Barcode.Linear)
//            BarcodeLib.Barcode.Linear barcode = new BarcodeLib.Barcode.Linear();

//            // Set barcode type to Code 39
//            barcode.Type = BarcodeLib.Barcode.BarcodeType.CODE39;

//            barcode.ImageHeight = 100;
//            barcode.ImageWidth = 120;
//            //barcode.BarHeight = 200;
//            barcode.BarWidth = 1;
//            barcode.BearerBarWidth = 100;
//            // Set your encoded barcode value
//            barcode.Data = text;

//            // Other barcode settings
//            // Save barcode image into your system
//            // Draw barcode image into a PNG file
//            barcode.drawBarcode(Server.MapPath(path));
//        }


//        private void CreateQrCode(string text, string path)
//        {
//            // Create an linear barcode object (BarcodeLib.Barcode.Linear)
//            BarcodeLib.Barcode.QRCode barcode = new BarcodeLib.Barcode.QRCode();

//            barcode.ImageHeight = 180;
//            barcode.ImageWidth = 200;
//            barcode.ModuleSize = 5;
//            // Set your encoded barcode value
//            barcode.Data = text;

//            // Other barcode settings
//            // Save barcode image into your system
//            // Draw barcode image into a PNG file
//            barcode.drawBarcode(Server.MapPath(path));
//        }
//    }
//}