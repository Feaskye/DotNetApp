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

public partial class PDF417WebStreamDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (hfFirst.Value.Equals("true"))
        {
            initForm();
            hfFirst.Value = "false";
            barcodeImage.ImageUrl = "pdf417.aspx?Data=PDF417";
        }
        else
        {
            generateBarcodeImage();
        }

        hfUrl.Value = barcodeImage.ImageUrl;
    }

    private void initForm()
    {
        ctrMode.Items.Add(new ListItem("Binary"));
        ctrMode.Items.Add(new ListItem("Text"));
        ctrMode.Items.Add(new ListItem("Numeric"));
        ctrMode.SelectedIndex = 1;

        ctrEcl.Items.Add(new ListItem("0"));
        ctrEcl.Items.Add(new ListItem("1"));
        ctrEcl.Items.Add(new ListItem("2"));
        ctrEcl.Items.Add(new ListItem("3"));
        ctrEcl.Items.Add(new ListItem("4"));
        ctrEcl.Items.Add(new ListItem("5"));
        ctrEcl.Items.Add(new ListItem("6"));
        ctrEcl.Items.Add(new ListItem("7"));
        ctrEcl.Items.Add(new ListItem("8"));
        ctrEcl.SelectedIndex = 2;

        ctrCompact.Items.Add(new ListItem("Yes", "true"));
        ctrCompact.Items.Add(new ListItem("No", "false"));
        ctrCompact.SelectedIndex = 1;

        ctrProcessTilde.Items.Add(new ListItem("Yes", "true"));
        ctrProcessTilde.Items.Add(new ListItem("No", "false"));

        ctrImageFormat.Items.Add(new ListItem("GIF", "gif"));
        ctrImageFormat.Items.Add(new ListItem("JPEG", "jpg"));


        ctrBarRatio.Items.Add(new ListItem("2"));
        ctrBarRatio.Items.Add(new ListItem("3"));
        ctrBarRatio.Items.Add(new ListItem("4"));
        ctrBarRatio.Items.Add(new ListItem("5"));
        ctrBarRatio.SelectedIndex = 1;

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

        barcodeImage.ImageUrl = "pdf417.aspx?" + builder.ToString();
    }

    private NameValueCollection createHttpQueryString()
    {
        NameValueCollection values = new NameValueCollection();
        values.Add("Data", ctrData.Text);
        values.Add("Mode", ctrMode.Text);
        values.Add("ECL", ctrEcl.Text);
        values.Add("Compact", ctrCompact.Text);
        values.Add("ProcessTilde", ctrProcessTilde.Text);
        values.Add("ImageFormat", ctrImageFormat.Text);

        values.Add("UOM", ctrUOM.SelectedValue);
        values.Add("BarWidth", ctrBarWidth.Text);
        values.Add("BarRatio", ctrBarRatio.Text);
        values.Add("Rows", ctrRows.Text);
        values.Add("Columns", ctrColumns.Text);
        values.Add("Resolution", ctrResolution.Text);
        values.Add("LeftMargin", ctrLeftMargin.Text);
        values.Add("RightMargin", ctrRightMargin.Text);
        values.Add("TopMargin", ctrTopMargin.Text);
        values.Add("BottomMargin", ctrBottomMargin.Text);

        return values;
    }
}
