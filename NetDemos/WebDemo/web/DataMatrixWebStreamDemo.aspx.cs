using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Text;

public partial class DataMatrixWebStreamDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (hfFirst.Value.Equals("true"))
        {
            initForm();
            hfFirst.Value = "false";
            barcodeImage.ImageUrl = "datamatrix.aspx?Data=DataMatrix";
        }
        else
        {
            generateBarcodeImage();
        }

        hfUrl.Value = barcodeImage.ImageUrl;
    }

    private void initForm()
    {
        ddEncoding.Items.Add(new ListItem("ASCII"));
        ddEncoding.Items.Add(new ListItem("C40"));
        ddEncoding.Items.Add(new ListItem("Text"));
        ddEncoding.Items.Add(new ListItem("Base256"));

        ddFormat.Items.Add(new ListItem("Auto", 0 + ""));
        ddFormat.Items.Add(new ListItem("10X10", 1 + ""));
        ddFormat.Items.Add(new ListItem("12X12", 2 + ""));
        ddFormat.Items.Add(new ListItem("14X14", 3 + ""));
        ddFormat.Items.Add(new ListItem("16X16", 4 + ""));
        ddFormat.Items.Add(new ListItem("18X18", 5 + ""));
        ddFormat.Items.Add(new ListItem("20X20", 6 + ""));
        ddFormat.Items.Add(new ListItem("22X22", 7 + ""));
        ddFormat.Items.Add(new ListItem("24X24", 8 + ""));
        ddFormat.Items.Add(new ListItem("26X26", 9 + ""));
        ddFormat.Items.Add(new ListItem("32X32", 10 + ""));
        ddFormat.Items.Add(new ListItem("36X36", 11 + ""));
        ddFormat.Items.Add(new ListItem("40X40", 12 + ""));
        ddFormat.Items.Add(new ListItem("44X44", 13 + ""));
        ddFormat.Items.Add(new ListItem("48X48", 14 + ""));
        ddFormat.Items.Add(new ListItem("52X52", 15 + ""));
        ddFormat.Items.Add(new ListItem("64X64", 16 + ""));
        ddFormat.Items.Add(new ListItem("72X72", 17 + ""));
        ddFormat.Items.Add(new ListItem("80X80", 18 + ""));
        ddFormat.Items.Add(new ListItem("88X88", 19 + ""));
        ddFormat.Items.Add(new ListItem("96X96", 20 + ""));
        ddFormat.Items.Add(new ListItem("104X104", 21 + ""));
        ddFormat.Items.Add(new ListItem("120X120", 22 + ""));
        ddFormat.Items.Add(new ListItem("132X132", 23 + ""));
        ddFormat.Items.Add(new ListItem("144X144", 24 + ""));
        ddFormat.Items.Add(new ListItem("8X18", 25 + ""));
        ddFormat.Items.Add(new ListItem("8X32", 26 + ""));
        ddFormat.Items.Add(new ListItem("12X26", 27 + ""));
        ddFormat.Items.Add(new ListItem("12X36", 28 + ""));
        ddFormat.Items.Add(new ListItem("16X36", 29 + ""));
        ddFormat.Items.Add(new ListItem("16X48", 30 + ""));

        ddImageFormat.Items.Add(new ListItem("GIF", "gif"));
        ddImageFormat.Items.Add(new ListItem("JPEG", "jpg"));

        ddProcessTilde.Items.Add(new ListItem("Yes", "true"));
        ddProcessTilde.Items.Add(new ListItem("No", "false"));

        ctrUOM.Items.Add(new ListItem("Pixel", "0"));
        ctrUOM.Items.Add(new ListItem("CM", "1"));
        ctrUOM.Items.Add(new ListItem("Inch", "2"));
    }

    private void generateBarcodeImage()
    {
        NameValueCollection values = createHttpQueryString();

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < values.Count; i++)
        {
            if (i != 0)
            {
                builder.Append("&");
            }
            builder.Append(values.GetKey(i));
            builder.Append("=");
            builder.Append(HttpUtility.UrlEncode(values.Get(i)));
        }

        barcodeImage.ImageUrl = "datamatrix.aspx?" + builder.ToString();
    }

    private NameValueCollection createHttpQueryString()
    {
        NameValueCollection values = new NameValueCollection();
        values.Add("Data", tbData.Text);
        values.Add("Encoding", ddEncoding.SelectedValue);
        values.Add("Format", ddFormat.SelectedValue);
        values.Add("ProcessTilde", ddProcessTilde.SelectedValue);
        values.Add("UOM", ctrUOM.SelectedValue);
        values.Add("ModuleSize", tbModuleSize.Text);
        values.Add("Resolution", tbResolution.Text);
        values.Add("LeftMargin", tbLeftMargin.Text);
        values.Add("RightMargin", tbRightMargin.Text);
        values.Add("TopMargin", tbTopMargin.Text);
        values.Add("BottomMargin", tbBottomMargin.Text);
        values.Add("ImageFormat", ddImageFormat.Text);

        return values;
    }
}
