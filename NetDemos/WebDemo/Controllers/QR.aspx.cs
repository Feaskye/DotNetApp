/*
 *********************************************************************
 * 程序名称 : WidgetS（辅助功能类）
 * 类名称   : zxing
 * 说明     : 二维码、条形码
 * 作者     : camnpr
 * 作成日期 : 2011-10-27
 * 网站     : http://www.camnpr.com/
 *********************************************************************
 */
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using com.google.zxing.qrcode;
using com.google.zxing;
using com.google.zxing.common;
using ByteMatrix = com.google.zxing.common.ByteMatrix;
using EAN13Writer = com.google.zxing.oned.EAN13Writer;
using EAN8Writer = com.google.zxing.oned.EAN8Writer;
using MultiFormatWriter = com.google.zxing.MultiFormatWriter;
using System.Drawing;
using WebDemo.Controllers;

namespace WidgetS
{
    public partial class QR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 生成指定信息的二维码图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string imgPath = "/QRimg/" + Guid.NewGuid().ToString() + ".png";
            //byte[] btCN = System.Text.Encoding.Default.GetBytes(TextBox1.Text.Trim());
            string content = TextBox1.Text.Trim();// System.Text.Encoding.UTF8.GetString(btCN);
            //中文乱码问题，修改com.google.zxing.qrcode.encoder.Encoder.cs 和 com.google.zxing.qrcode.decoder.DecodedBitStreamParser.cs
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 200, 200);
            ZXingHelper.writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png, MapPath(imgPath));
            Image1.ImageUrl = imgPath;
            Image1.DataBind();
        }

       

        public static Bitmap toBitmap(ByteMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }

        /// <summary>
        /// 解析二维码图 信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(MapPath(Image1.ImageUrl.Trim()));
            Bitmap bmap;
            try
            {
                bmap = new Bitmap(img);
            }
            catch (System.IO.IOException ioe)
            {
                Literal1.Text = ioe.ToString();
                return;
            }
            if (bmap == null)
            {
                Literal1.Text = "Could not decode image";
                return;
            }
            LuminanceSource source = new RGBLuminanceSource(bmap, bmap.Width, bmap.Height);
            com.google.zxing.BinaryBitmap bitmap = new com.google.zxing.BinaryBitmap(new com.google.zxing.common.HybridBinarizer(source));
            Result result;
            try
            {
                result = new MultiFormatReader().decode(bitmap);
            }
            catch (ReaderException re)
            {
                Literal1.Text = re.ToString();
                return;
            }
            Literal1.Text = result.Text;
        }

        /// <summary>
        /// 条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            string imgPath = "/QRimg/" + Guid.NewGuid().ToString() + ".png";
            EAN13Encode(barCode.Text.Trim(), 210, 80, imgPath);
            Image2.ImageUrl = imgPath;
            Image2.DataBind();
        }

        public void EAN13Encode(String contents, int width, int height, String imgPath)
        {
            int codeWidth = 3 + // start guard  
                    (7 * 6) + // left bars  
                    5 + // middle guard  
                    (7 * 6) + // right bars  
                    3; // end guard  
            codeWidth = Math.Max(codeWidth, width);

            ByteMatrix byteMatrix = new MultiFormatWriter().encode(contents, BarcodeFormat.EAN_13, codeWidth, height);

            ZXingHelper.writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png, MapPath(imgPath));
        }

        /// <summary>
        /// 读取条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(MapPath(Image2.ImageUrl.Trim()));
            Bitmap bmap;
            try
            {
                bmap = new Bitmap(img);
            }
            catch (System.IO.IOException ioe)
            {
                Literal2.Text = ioe.ToString();
                return;
            }
            if (bmap == null)
            {
                Literal2.Text = "Could not decode image";
                return;
            }
            LuminanceSource source = new RGBLuminanceSource(bmap, bmap.Width, bmap.Height);
            com.google.zxing.BinaryBitmap bitmap = new com.google.zxing.BinaryBitmap(new com.google.zxing.common.HybridBinarizer(source));
            Result result;
            try
            {
                result = new MultiFormatReader().decode(bitmap);
            }
            catch (ReaderException re)
            {
                Literal2.Text = re.ToString();
                return;
            }
            Literal2.Text = result.Text;
        }

    }
}