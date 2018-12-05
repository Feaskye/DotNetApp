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

public partial class WebStreamDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (hfFirst.Value.Equals("true"))
        {
            initForm();
            hfFirst.Value = "false";
            barcodeImage.ImageUrl = "linear.aspx?Data=ABC123&Type=CODE39";
        }
        else
        {
            generateBarcodeImage();
        }

        hfUrl.Value = barcodeImage.ImageUrl;
        
    }

    private void initForm()
    {
        ddType.Items.Add(new ListItem("CODABAR", "0"));
        ddType.Items.Add(new ListItem("CODE11", "1"));
        ddType.Items.Add(new ListItem("CODE2OF5", "2"));
        ddType.Items.Add(new ListItem("CODE39", "3"));
        ddType.Items.Add(new ListItem("CODE39EX", "4"));
        ddType.Items.Add(new ListItem("CODE93", "5"));
        ddType.Items.Add(new ListItem("EAN8", "7"));
        ddType.Items.Add(new ListItem("EAN8 Supplement 2", "8"));
        ddType.Items.Add(new ListItem("EAN8 Supplement 5", "9"));
        ddType.Items.Add(new ListItem("EAN13", "10"));
        ddType.Items.Add(new ListItem("EAN13 Supplement 2", "11"));
        ddType.Items.Add(new ListItem("EAN13 Supplement 5", "12"));
        ddType.Items.Add(new ListItem("ITF14", "18"));
        ddType.Items.Add(new ListItem("INTERLEAVED25", "19"));
        ddType.Items.Add(new ListItem("CODE128 Auto", "22"));
        ddType.Items.Add(new ListItem("CODE128A", "23"));
        ddType.Items.Add(new ListItem("CODE128B", "24"));
        ddType.Items.Add(new ListItem("CODE128C", "25"));
        ddType.Items.Add(new ListItem("UCC/EAN128", "26"));
        ddType.Items.Add(new ListItem("MSI", "31"));
        ddType.Items.Add(new ListItem("MSI10", "32"));
        ddType.Items.Add(new ListItem("MSI11", "33"));
        ddType.Items.Add(new ListItem("MSI1010", "34"));
        ddType.Items.Add(new ListItem("MSI1110", "35"));
        ddType.Items.Add(new ListItem("ONECODE", "36"));
        ddType.Items.Add(new ListItem("PLANET", "37"));
        ddType.Items.Add(new ListItem("POSTNET", "38"));
        ddType.Items.Add(new ListItem("RM4SCC", "39"));
        ddType.Items.Add(new ListItem("UPCA", "40"));
        ddType.Items.Add(new ListItem("UPCA Supplement 2", "41"));
        ddType.Items.Add(new ListItem("UPCA Supplement 5", "42"));
        ddType.Items.Add(new ListItem("UPCE", "43"));
        ddType.Items.Add(new ListItem("UPCE Supplement 2", "44"));
        ddType.Items.Add(new ListItem("UPCE Supplement 5", "45"));

        ddType.Items[3].Selected = true;

        ddAddCheckSum.Items.Add(new ListItem("Yes", "true"));
        ddAddCheckSum.Items.Add(new ListItem("No", "false"));


        ctrBearerBars.Items.Add(new ListItem("None", "0"));
        ctrBearerBars.Items.Add(new ListItem("Frame", "1"));
        ctrBearerBars.Items.Add(new ListItem("TopBottom", "2"));

        ddShowText.Items.Add(new ListItem("Yes", "true"));
        ddShowText.Items.Add(new ListItem("No", "false"));

        ddTextFontName.Items.Add(new ListItem("Arial", "Arial"));
        ddTextFontName.Items.Add(new ListItem("Times New Roman", "Times New Roman"));

        ddTextFontSize.Items.Add(new ListItem("8", "8"));
        ddTextFontSize.Items.Add(new ListItem("9", "9"));
        ddTextFontSize.Items.Add(new ListItem("10", "10"));
        ddTextFontSize.Items.Add(new ListItem("11", "11"));
        ddTextFontSize.Items.Add(new ListItem("12", "12"));
        ddTextFontSize.Items.Add(new ListItem("13", "13"));
        ddTextFontSize.Items.Add(new ListItem("14", "14"));

        ddTextFontSize.Items[1].Selected = true;

        cblTextFontStyle.Items.Add(new ListItem("Bold", "Bold"));
        cblTextFontStyle.Items.Add(new ListItem("Italic", "Italic"));
        cblTextFontStyle.Items.Add(new ListItem("Underline", "Underline"));

        ddRotate.Items.Add(new ListItem("Bottom Facing Down", "0"));
        ddRotate.Items.Add(new ListItem("Bottom Facing Left", "1"));
        ddRotate.Items.Add(new ListItem("Bottom Facing Up", "2"));
        ddRotate.Items.Add(new ListItem("Bottom Facing Right", "3"));

        ddImageFormat.Items.Add(new ListItem("JPEG", "jpg"));
        ddImageFormat.Items.Add(new ListItem("GIF", "gif"));
        ddImageFormat.Items.Add(new ListItem("BMP", "bmp"));
        ddImageFormat.Items.Add(new ListItem("TIFF", "tif"));

        ctrUOM.Items.Add(new ListItem("Pixel", "0"));
        ctrUOM.Items.Add(new ListItem("CM", "1"));
        ctrUOM.Items.Add(new ListItem("Inch", "2"));

        ddCodabarStartChar.Items.Add(new ListItem("A", "A"));
        ddCodabarStartChar.Items.Add(new ListItem("B", "B"));
        ddCodabarStartChar.Items.Add(new ListItem("C", "C"));
        ddCodabarStartChar.Items.Add(new ListItem("D", "D"));

        ddCodabarStopChar.Items.Add(new ListItem("A", "A"));
        ddCodabarStopChar.Items.Add(new ListItem("B", "B"));
        ddCodabarStopChar.Items.Add(new ListItem("C", "C"));
        ddCodabarStopChar.Items.Add(new ListItem("D", "D"));

        ddUPCENumber.Items.Add(new ListItem("0", "0"));
        ddUPCENumber.Items.Add(new ListItem("1", "1"));
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

        barcodeImage.ImageUrl = "linear.aspx?" + builder.ToString() ;
    }

    private NameValueCollection createHttpQueryString()
    {
        NameValueCollection values = new NameValueCollection();
        values.Add("Type", ddType.SelectedValue);
        values.Add("Data", tbData.Text);
        values.Add("SData", tbSData.Text);
        values.Add("BarWidth", tbBarWidth.Text);
        values.Add("BarHeight", tbBarHeight.Text);
        values.Add("Resolution", tbResolution.Text);
        values.Add("BearerBars", ctrBearerBars.Text);
        values.Add("UOM", ctrUOM.SelectedValue);
        values.Add("LeftMargin", tbLeftMargin.Text);
        values.Add("TopMargin", tbTopMargin.Text);
        values.Add("ShowText", ddShowText.SelectedValue);
        values.Add("TextFont", getFontSettingText());
        values.Add("Format", ddImageFormat.SelectedValue);
        values.Add("AddCheckSum", ddAddCheckSum.SelectedValue);
        values.Add("Rotate", ddRotate.SelectedValue);
        values.Add("CodabarStartChar", ddCodabarStartChar.SelectedValue);
        values.Add("CodabarStopChar", ddCodabarStopChar.SelectedValue);
        values.Add("UPCENumberSystem", ddUPCENumber.SelectedValue);

        return values;
    }

    private string getFontSettingText()
    {
        string style = ddTextFontName.SelectedValue + "|";
        style += ddTextFontSize.SelectedValue + "|";
        foreach (ListItem item in cblTextFontStyle.Items)
        {
            if (item.Selected)
            {
                style += item.Value + ",";
            }
        }

        return style;
    }
}
