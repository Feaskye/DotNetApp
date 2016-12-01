using com.google.zxing;
using com.google.zxing.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebDemo.Controllers
{
    public class ZXingHelper
    {
        public static void EAN13Encode(String contents, int width, int height, String imgPath)
        {
            int codeWidth = 3 + // start guard  
                    (7 * 6) + // left bars  
                    5 + // middle guard  
                    (7 * 6) + // right bars  
                    3; // end guard  
            codeWidth = Math.Max(codeWidth, width);

             ByteMatrix byteMatrix = new MultiFormatWriter().encode("PH201609261515507467", BarcodeFormat.CODE_128, 200, 100);

            writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png,HttpContext.Current.Server.MapPath(imgPath));
        }

        public static void writeToFile(ByteMatrix matrix, System.Drawing.Imaging.ImageFormat format, string file)
        {
            Bitmap bmap = toBitmap(matrix);
            bmap.Save(file, format);
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

    }
}