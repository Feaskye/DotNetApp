using SkyTeleFault.App_Start;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyTeleFault.Controllers
{
    public class ControllerBase : Controller
    {
        DBAccessHelper _dBAccessHelper = new DBAccessHelper();

        
        public ActionResult JsonResult(string message,bool state=false)
        {
            return Content(new { state = state?1:0, msg = message }.ToJson());
        }

        public ActionResult JsonResult(bool state = true)
        {
            return Content(new { state = state ? 1 : 0, msg = "" }.ToJson());
        }

    }
}