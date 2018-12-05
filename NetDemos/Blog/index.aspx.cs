using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Runtime.Caching;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //对客户端页面输出脚本 
        //this.ClientScript.RegisterArrayDeclaration("aAry", "1,2,3");
        //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "sblk", "var aa=1;");
        try
        {
            int p = int.Parse("rter");
        }
        catch (Exception ex)
        {
            LogHelper.WriteLog(ex.ToString());
        }

    }


    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff")+Path.GetExtension(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(Server.MapPath("UploadFiles/" + FileName));
            string sourseFile = Server.MapPath("UploadFiles/" + FileName);
            string targetFileName = Server.MapPath("UploadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "_water" + Path.GetExtension(FileUpload1.PostedFile.FileName));

            Color fontColor = Color.FromName("Gray");
            Color fontShodowColor = Color.FromName("Gray");

            WebGDI.GetWaterMarkTextImage(sourseFile, "麦勒商城", "黑体", wmPosition.RB, targetFileName, fontColor, fontShodowColor, true, 30, 70);
        }
    }


}