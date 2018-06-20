
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace WebDemo.Controllers
{
    /// <summary>
    /// http://www.cnblogs.com/tianma3798/p/5426869.html
    /// </summary>
    public class ZXingHelper
    {
        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        public static void QRCode(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(text);
            string filename = @"H:\桌面\截图\generate1.png";
            map.Save(filename, ImageFormat.Png);
            map.Dispose();
        }

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="text"></param>
        public static void BarCode(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = 150,
                Height = 50,
                Margin = 2
            };
            writer.Options = options;
            Bitmap map = writer.Write(text);
            string filename = @"H:\桌面\截图\generate2.png";
            map.Save(filename, ImageFormat.Png);
        }

    }
}