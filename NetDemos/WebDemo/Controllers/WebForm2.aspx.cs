﻿using BarcodeLib.Barcode.ASP.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.Controllers
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QRCodeASPNETResponse.drawBarcode(Request, Response);
        }
    }
}