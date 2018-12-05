using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    private const string FILENAME = "index.html";
    public string SearchKey = "<p>";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.ToLower() == "post")
        {
            if (Request.Form["search"] != null)
            {
                SearchKey=Request.Form["search"].ToString();
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        //if (File.Exists(Server.MapPath(FILENAME)))
        //{
        //    Response.Redirect(FILENAME);
        //}
    }



    //protected void Page_LoadComplete(object sender, EventArgs e)
    //{
    //    System.IO.StringWriter html = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter tw = new System.Web.UI.HtmlTextWriter(html);
    //    base.Render(tw);
    //    System.IO.StreamWriter sw;
    //    sw = new System.IO.StreamWriter(Server.MapPath(FILENAME), false, System.Text.Encoding.Default);
    //    sw.Write(html.ToString());
    //    sw.Close();
    //    tw.Close();

    //    //if (File.Exists(Server.MapPath(FILENAME)))
    //    //{
    //    //    Response.Redirect(FILENAME);
    //    //}
    //}

    //protected override void Render(Page Page,string FileName)
    //{
    //    System.IO.StringWriter html = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter tw = new System.Web.UI.HtmlTextWriter(html);
    //    base.Render(tw);
    //    System.IO.StreamWriter sw;
    //    sw = new System.IO.StreamWriter(Server.MapPath(FileName), false, System.Text.Encoding.Default);
    //    sw.Write(html.ToString());
    //    sw.Close();
    //    tw.Close();
    //    Response.Write(html.ToString());
    //}  






}