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

public partial class QRCodeWebStreamDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (hfFirst.Value.Equals("true"))
        {
            initForm();
            hfFirst.Value = "false";
            barcodeImage.ImageUrl = "qrcode.aspx?Data=QRCode";
        }
        else
        {
            generateBarcodeImage();
        }

        hfUrl.Value = barcodeImage.ImageUrl;
    }

    private void initForm()
    {
        ctrEncoding.Items.Add(new ListItem("Auto", "0"));
        ctrEncoding.Items.Add(new ListItem("AlphaNumeric", "1"));
        ctrEncoding.Items.Add(new ListItem("Byte", "2"));
        ctrEncoding.Items.Add(new ListItem("Numeric", "3"));
        ctrEncoding.Items.Add(new ListItem("Kanji", "4"));

        ctrVersion.Items.Add(new ListItem("Auto", 0 + ""));
        for (int i = 1; i <= 40; i++)
        {
            ctrVersion.Items.Add(new ListItem("Version " + i, i + ""));
        }

        ctrECL.Items.Add(new ListItem("L", "0"));
        ctrECL.Items.Add(new ListItem("M", "1"));
        ctrECL.Items.Add(new ListItem("Q", "2"));
        ctrECL.Items.Add(new ListItem("H", "3"));

        ctrEnableStructuredAppend.Items.Add(new ListItem("No", "false"));
        ctrEnableStructuredAppend.Items.Add(new ListItem("Yes", "true"));

        ctrFNC1Mode.Items.Add(new ListItem("Not Supported", "0"));
        ctrFNC1Mode.Items.Add(new ListItem("UCC/EAN", "1"));
        ctrFNC1Mode.Items.Add(new ListItem("AIM", "2"));

        ctrProcessTilde.Items.Add(new ListItem("No", "false"));
        ctrProcessTilde.Items.Add(new ListItem("Yes", "true"));

        ctrImageFormat.Items.Add(new ListItem("PNG", "png"));
        ctrImageFormat.Items.Add(new ListItem("GIF", "gif"));
        ctrImageFormat.Items.Add(new ListItem("JPEG", "jpg"));

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

        barcodeImage.ImageUrl = "qrcode.aspx?" + builder.ToString();
    }

    private NameValueCollection createHttpQueryString()
    {
        NameValueCollection values = new NameValueCollection();

        values.Add("Data", ctrData.Text);
        values.Add("Encoding", ctrEncoding.Text);
        values.Add("Version", ctrVersion.Text);
        values.Add("ECL", ctrECL.Text);
        values.Add("EnableStructuredAppend", ctrEnableStructuredAppend.Text);
        values.Add("StructuredAppendCount", ctrStructuredAppendCount.Text);
        values.Add("StructuredAppendIndex", ctrStructuredAppendIndex.Text);
        values.Add("FNC1Mode", ctrFNC1Mode.Text);
        values.Add("ApplicationIndicator", ctrApplicationIndicator.Text);
        values.Add("ECI", ctrECI.Text);
        values.Add("ProcessTilde", ctrProcessTilde.Text);

        values.Add("UOM", ctrUOM.SelectedValue);
        values.Add("ModuleSize", ctrModuleSize.Text);
        values.Add("LeftMargin", ctrLeftMargin.Text);
        values.Add("RightMargin", ctrRightMargin.Text);
        values.Add("TopMargin", ctrTopMargin.Text);
        values.Add("BottomMargin", ctrBottomMargin.Text);
        values.Add("Resolution", ctrResolution.Text);
        values.Add("ImageFormat", ctrImageFormat.Text);

        return values;
    }
}
