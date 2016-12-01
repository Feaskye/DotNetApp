using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class ZXingController : Controller
    {
        // GET: ZXing
        public ActionResult Index()
        {
            string imgPath = "/QRimg/" + Guid.NewGuid().ToString() + ".png";
            ZXingHelper.EAN13Encode("PH20160926151", 210, 80, imgPath);

            return View(imgPath);
        }
    }
}