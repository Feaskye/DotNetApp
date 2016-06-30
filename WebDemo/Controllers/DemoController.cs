using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/
        public ActionResult Index()
        {
            IPAddress[] ipAddresses;
            Task<IPAddress[]> ipAddressesPromise = Dns.GetHostAddressesAsync("oreilly.com");
            ipAddressesPromise.ContinueWith(_ =>
            {
                ipAddresses = ipAddressesPromise.Result;
            });
            return View();
        }


	}



}