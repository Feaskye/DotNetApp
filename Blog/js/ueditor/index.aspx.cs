using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class js_ueditor_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.ToLower() == "post")
        {
            if (Request.Form["txtContent"]!=null)
            {
                //Response.Write(Request.Form["txtContent"].ToString());
                //hideContent.Value = Request.Form["txtContent"].ToString();
            }
        }
    }
}