using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int rc = 1002;
        
        //oranPagerMain.RecordCount = rc;
        //oranPagerMain.ShowCustomizeText = true;
        //oranPagerMain.CustomizeText = string.Format("<span class=\"cust_info\">Record Count:{0}</span>", rc);

        //// oranPagerMain.EnableCustomizeLayout = true;
        //oranPagerMain.CustomizeLayoutFormatString = "<i>{$text$}</i> <b>{$first$} {$last$} {$pre$} {$next$}</b> "
        //    + "<u>{$num$}</u>{$pre_more$} | {$next_more$}"
        //    + "<div>{$list$}</div>"
        //    + "<h1>{$goto$}</h1>";
    }
}