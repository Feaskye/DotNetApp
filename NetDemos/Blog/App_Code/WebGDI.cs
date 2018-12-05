using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;

public class WebGDI
{
    // Methods
    protected WebGDI()
    {
    }

    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
        for (int i = 0; i < imageEncoders.Length; i++)
        {
            if (imageEncoders[i].MimeType == mimeType)
            {
                return imageEncoders[i];
            }
        }
        return null;
    }

    public static void GetThumbnailImage(int Width, int Height, string SourceImg, string SaveImage)
    {
        string filename = SourceImg + ".jpg";
        Image image = Image.FromFile(SourceImg);
        Image original = image.GetThumbnailImage(Width, Height, new Image.GetThumbnailImageAbort(WebGDI.ThumbnailCallback), IntPtr.Zero);
        HttpContext.Current.Response.Clear();
        Bitmap bitmap = new Bitmap(original);
        Graphics graphics = Graphics.FromImage(bitmap);
        if (!string.IsNullOrEmpty(SaveImage))
        {
            bitmap.Save(filename, ImageFormat.Jpeg);
        }
        else
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.GetBuffer());
        }
        original.Dispose();
        image.Dispose();
        bitmap.Dispose();
        graphics.Dispose();
    }

    public static void GetThumbnailImage(int Width, int Height, string SourceImg, string Text, int Left, int Top, Font font)
    {
        string filename = SourceImg + ".jpg";
        Image image = Image.FromFile(SourceImg);
        Image original = image.GetThumbnailImage(Width, Height, new Image.GetThumbnailImageAbort(WebGDI.ThumbnailCallback), IntPtr.Zero);
        HttpContext.Current.Response.Clear();
        Bitmap bitmap = new Bitmap(original);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.DrawString(Text, font, new SolidBrush(Color.Black), (float)(Left - 1), (float)(Top + 2), new StringFormat(StringFormatFlags.DirectionVertical));
        graphics.DrawString(Text, font, new SolidBrush(Color.White), (float)Left, (float)Top, new StringFormat(StringFormatFlags.DirectionVertical));
        bitmap.Save(filename, ImageFormat.Jpeg);
        original.Dispose();
        image.Dispose();
        bitmap.Dispose();
        graphics.Dispose();
    }

    public static void GetWarterMarkPicTextImage(string SourceImage, string WaterMarkImage, string Text, string fontFamily, wmPosition wmPos, wmPosition textPos, string SaveImage)
    {
        Image image = Image.FromFile(SourceImage);
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        int[] numArray = new int[] { 0x10, 14, 12, 10, 8, 6, 4 };
        Font font = null;
        SizeF ef = new SizeF();
        for (int i = 0; i < 7; i++)
        {
            font = new Font(fontFamily, (float)numArray[i], FontStyle.Bold);
            ef = graphics.MeasureString(Text, font);
            if (((ushort)ef.Width) < ((ushort)width))
            {
                break;
            }
        }
        int num4 = (int)(height * 0.05);
        float y = (height - num4) - (ef.Height / 2f);
        float x = width / 2;
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        SolidBrush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        graphics.DrawString(Text, font, brush, new PointF(x + 1f, y + 1f), format);
        SolidBrush brush2 = new SolidBrush(Color.FromArgb(100, 0xff, 0xff, 0xff));
        graphics.DrawString(Text, font, brush2, new PointF(x, y), format);
        Image image2 = new Bitmap(WaterMarkImage);
        int srcWidth = image2.Width;
        int srcHeight = image2.Height;
        Bitmap bitmap2 = new Bitmap(bitmap);
        bitmap2.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics2 = Graphics.FromImage(bitmap2);
        ImageAttributes imageAttr = new ImageAttributes();
        ColorMap map = new ColorMap();
        map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
        map.NewColor=Color.FromArgb(0, 0, 0, 0);
        ColorMap[] mapArray = new ColorMap[] { map };
        imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
        float[][] numArray3 = new float[5][];
        float[] numArray4 = new float[5];
        numArray4[0] = 1f;
        numArray3[0] = numArray4;
        numArray4 = new float[5];
        numArray4[1] = 1f;
        numArray3[1] = numArray4;
        numArray4 = new float[5];
        numArray4[2] = 1f;
        numArray3[2] = numArray4;
        numArray4 = new float[5];
        numArray4[3] = 0.3f;
        numArray3[3] = numArray4;
        numArray4 = new float[5];
        numArray4[4] = 1f;
        numArray3[4] = numArray4;
        float[][] newColorMatrix = numArray3;
        ColorMatrix matrix = new ColorMatrix(newColorMatrix);
        imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        int num9 = (width - srcWidth) - 10;
        int num10 = 10;
        graphics2.DrawImage(image2, new Rectangle(num9, num10, srcWidth, srcHeight), 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel, imageAttr);
        image.Dispose();
        image = bitmap2;
        graphics.Dispose();
        graphics2.Dispose();
        bitmap.Dispose();
        image.Save(SaveImage, ImageFormat.Jpeg);
        image.Dispose();
        image2.Dispose();
        bitmap2.Dispose();
    }

    public static void GetWaterMarkPicImage(string SourceImage, string WaterMarkImage, wmPosition wmPos, string SaveImage, float opacity)
    {
        opacity = Convert.ToInt32((double)(opacity * 2.55));
        Image image = Image.FromFile(SourceImage);
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        Image image2 = new Bitmap(WaterMarkImage);
        int srcWidth = image2.Width;
        int srcHeight = image2.Height;
        Bitmap bitmap2 = new Bitmap(bitmap);
        bitmap2.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics2 = Graphics.FromImage(bitmap2);
        ImageAttributes imageAttr = new ImageAttributes();
        ColorMap map = new ColorMap();
        map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
        map.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] mapArray = new ColorMap[] { map };
        imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
        float[][] numArray2 = new float[5][];
        float[] numArray3 = new float[5];
        numArray3[0] = 1f;
        numArray2[0] = numArray3;
        numArray3 = new float[5];
        numArray3[1] = 1f;
        numArray2[1] = numArray3;
        numArray3 = new float[5];
        numArray3[2] = 1f;
        numArray2[2] = numArray3;
        numArray3 = new float[5];
        numArray3[3] = opacity;
        numArray2[3] = numArray3;
        numArray3 = new float[5];
        numArray3[4] = 1f;
        numArray2[4] = numArray3;
        float[][] newColorMatrix = numArray2;
        ColorMatrix matrix = new ColorMatrix(newColorMatrix);
        imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        int num5 = 0;
        int num6 = 0;
        switch (wmPos)
        {
            case wmPosition.MM:
                num5 = (width - srcWidth) / 2;
                num6 = (height - srcHeight) / 2;
                break;

            case wmPosition.LT:
                num5 = 10;
                num6 = 10;
                break;

            case wmPosition.LB:
                num5 = 10;
                num6 = (height - srcHeight) - 10;
                break;

            case wmPosition.CT:
                num5 = (width - srcWidth) / 2;
                num6 = 10;
                break;

            case wmPosition.CB:
                num5 = (width - srcWidth) / 2;
                num6 = (height - srcHeight) - 10;
                break;

            case wmPosition.RT:
                num5 = (width - srcWidth) - 10;
                num6 = 10;
                break;

            case wmPosition.RB:
                num5 = (width - srcWidth) - 10;
                num6 = (height - srcHeight) - 10;
                break;

            case wmPosition.LM:
                num5 = 10;
                num6 = (height - srcHeight) / 2;
                break;

            case wmPosition.RM:
                num5 = (width - srcWidth) - 10;
                num6 = (height - srcHeight) / 2;
                break;
        }
        int x = num5;
        int y = num6;
        graphics2.DrawImage(image2, new Rectangle(x, y, srcWidth, srcHeight), 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel, imageAttr);
        image.Dispose();
        image = bitmap2;
        graphics.Dispose();
        graphics2.Dispose();
        bitmap.Dispose();
        if (!string.IsNullOrEmpty(SaveImage))
        {
            image.Save(SaveImage, ImageFormat.Jpeg);
        }
        else
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.GetBuffer());
        }
        image.Dispose();
        image2.Dispose();
        bitmap2.Dispose();
    }

    public static void GetWaterMarkPicImage(string SourceImage, string WaterMarkImage, int wmPos, string SaveImage, int w, int h)
    {
        Image image2 = Image.FromFile(SourceImage).GetThumbnailImage(w, h, new Image.GetThumbnailImageAbort(WebGDI.ThumbnailCallback), IntPtr.Zero);
        HttpContext.Current.Response.Clear();
        Image image = image2;
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        Image image3 = new Bitmap(WaterMarkImage);
        int srcWidth = image3.Width;
        int srcHeight = image3.Height;
        Bitmap bitmap2 = new Bitmap(bitmap);
        bitmap2.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics2 = Graphics.FromImage(bitmap2);
        ImageAttributes imageAttr = new ImageAttributes();
        ColorMap map = new ColorMap();
        map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
        map.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] mapArray = new ColorMap[] { map };
        imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
        float[][] numArray2 = new float[5][];
        float[] numArray3 = new float[5];
        numArray3[0] = 1f;
        numArray2[0] = numArray3;
        numArray3 = new float[5];
        numArray3[1] = 1f;
        numArray2[1] = numArray3;
        numArray3 = new float[5];
        numArray3[2] = 1f;
        numArray2[2] = numArray3;
        numArray3 = new float[5];
        numArray3[3] = 0.3f;
        numArray2[3] = numArray3;
        numArray3 = new float[5];
        numArray3[4] = 1f;
        numArray2[4] = numArray3;
        float[][] newColorMatrix = numArray2;
        ColorMatrix matrix = new ColorMatrix(newColorMatrix);
        imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        int num5 = 0;
        int num6 = 0;
        switch (wmPos)
        {
            case 0:
                num5 = 10;
                num6 = 10;
                break;

            case 1:
                num5 = 10;
                num6 = (height - srcHeight) - 10;
                break;

            case 2:
                num5 = (width - srcWidth) / 2;
                num6 = 10;
                break;

            case 3:
                num5 = (width - srcWidth) / 2;
                num6 = (height - srcHeight) / 2;
                break;

            case 4:
                num5 = (width - srcWidth) / 2;
                num6 = (height - srcHeight) - 10;
                break;

            case 5:
                num5 = (width - srcWidth) - 10;
                num6 = 10;
                break;

            case 6:
                num5 = (width - srcWidth) - 10;
                num6 = (height - srcHeight) - 10;
                break;
        }
        int x = num5;
        int y = num6;
        graphics2.DrawImage(image3, new Rectangle(x, y, srcWidth, srcHeight), 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel, imageAttr);
        image.Dispose();
        image = bitmap2;
        graphics.Dispose();
        graphics2.Dispose();
        bitmap.Dispose();
        if (!string.IsNullOrEmpty(SaveImage))
        {
            image.Save(SaveImage, ImageFormat.Jpeg);
        }
        else
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.GetBuffer());
        }
        image.Dispose();
        image3.Dispose();
        bitmap2.Dispose();
    }

    public static void GetWaterMarkTextImage(string SourceImage, string Text, string fontFamily, wmPosition textPos, string SaveImage)
    {
        Color gray = Color.Gray;
        Color black = Color.Black;
        bool drawShadow = true;
        float fontSize = 16f;
        int opacity = 30;
        GetWaterMarkTextImage(SourceImage, Text, fontFamily, textPos, SaveImage, gray, black, drawShadow, fontSize, opacity);
    }

    public static void GetWaterMarkTextImage(string SourceImage, string Text, string fontFamily, wmPosition textPos, string SaveImage, Color fontColor, Color shadowColor, bool drawShadow, float fontSize, int opacity)
    {
        opacity = Convert.ToInt32((double)(opacity * 2.55));
        Image image = Image.FromFile(SourceImage);
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        Font font = new Font(fontFamily, fontSize, FontStyle.Regular);
        SizeF ef = new SizeF(fontSize, fontSize);
        float num3 = ef.Width - 15f;
        if (num3 < 0f)
        {
            num3 = 15f;
        }
        float num4 = ef.Height;
        if (num4 < 0f)
        {
            num4 = 15f;
        }
        float x = 0f;
        float y = 0f;
        switch (textPos)
        {
            case wmPosition.MM:
                x = width / 2;
                y = height / 2;
                break;

            case wmPosition.LT:
                x = num3 * Text.Length;
                y = num4 / 2f;
                break;

            case wmPosition.LB:
                x = num3 * Text.Length;
                y = (height - num4) - (num4 * 0.3f);
                if (y < 0f)
                {
                    y = 0f;
                }
                break;

            case wmPosition.CT:
                x = width / 2;
                y = num4 * 0.3f;
                break;

            case wmPosition.CB:
                x = width / 2;
                y = (height - num4) - (num4 * 0.5f);
                if (y < 0f)
                {
                    y = 0f;
                }
                break;

            case wmPosition.RT:
                x = width - (num3 * Text.Length);
                y = num4 * 0.3f;
                if (x < 0f)
                {
                    x = 0f;
                }
                break;

            case wmPosition.RB:
                x = width - (num3 * Text.Length);
                y = (height - num4) - (num4 * 0.5f);
                if (y < 0f)
                {
                    y = 0f;
                }
                if (x < 0f)
                {
                    x = 0f;
                }
                break;

            case wmPosition.LM:
                x = num3 * Text.Length;
                y = height / 2;
                break;

            case wmPosition.RM:
                x = width - (num3 * Text.Length);
                y = height / 2;
                break;
        }
        int num7 = (int)(height * 0.05);
        float num8 = (height - num7) - (ef.Height / 2f);
        float num9 = width / 2;
        StringFormat format = new StringFormat();
        format.Alignment=StringAlignment.Center;
        
        if (drawShadow)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(opacity, shadowColor));
            graphics.DrawString(Text, font, brush, x + 2f, y + 2f, format);
        }
        SolidBrush brush2 = new SolidBrush(Color.FromArgb(opacity, fontColor));
        graphics.DrawString(Text, font, brush2, x, y, format);
        graphics.Dispose();
        image.Dispose();
        image = bitmap;
        if (!string.IsNullOrEmpty(SaveImage))
        {
            image.Save(SaveImage, ImageFormat.Jpeg);
        }
        else
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.GetBuffer());
        }
        bitmap.Dispose();
        image.Dispose();
    }

    public static void ImageMark(string imgLoc, string wmLoc)
    {
        ImageMark(imgLoc, wmLoc, "RB");
    }

    public static void ImageMark(string imgLoc, string wmLoc, string wmAlign)
    {
        if (imgLoc.Substring(imgLoc.Length - 3, 3).ToLower() == "jpg")
        {
            Image original = Image.FromFile(imgLoc);
            int width = original.Width;
            int height = original.Height;
            Bitmap image = new Bitmap(original, width, height);
            image.SetResolution(72f, 72f);
            Graphics graphics = Graphics.FromImage(image);
            Image image2 = new Bitmap(wmLoc);
            int srcWidth = image2.Width;
            int srcHeight = image2.Height;
            if ((width >= srcWidth) && (height >= (srcHeight * 2)))
            {
                int num;
                int num2;
                Bitmap bitmap2 = new Bitmap(image);
                bitmap2.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                Graphics graphics2 = Graphics.FromImage(bitmap2);
                ImageAttributes imageAttr = new ImageAttributes();
                ColorMap map = new ColorMap();
                map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
                map.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] mapArray = new ColorMap[] { map };
                imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                float[][] numArray = new float[5][];
                float[] numArray2 = new float[5];
                numArray2[0] = 1f;
                numArray[0] = numArray2;
                numArray2 = new float[5];
                numArray2[1] = 1f;
                numArray[1] = numArray2;
                numArray2 = new float[5];
                numArray2[2] = 1f;
                numArray[2] = numArray2;
                numArray2 = new float[5];
                numArray2[3] = 0.8f;
                numArray[3] = numArray2;
                numArray2 = new float[5];
                numArray2[4] = 1f;
                numArray[4] = numArray2;
                float[][] newColorMatrix = numArray;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                if (wmAlign == "LT")
                {
                    num = 0;
                    num2 = 0;
                }
                else if (wmAlign == "LB")
                {
                    num = 0;
                    num2 = height - srcHeight;
                }
                else if (wmAlign == "RT")
                {
                    num = width - srcWidth;
                    num2 = 0;
                }
                else if (wmAlign == "CT")
                {
                    num = (width - srcWidth) / 2;
                    num2 = (height - srcHeight) / 2;
                }
                else
                {
                    num = width - srcWidth;
                    num2 = height - srcHeight;
                }
                graphics2.DrawImage(image2, new Rectangle(num, num2, srcWidth, srcHeight), 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel, imageAttr);
                original.Dispose();
                original = bitmap2;
                graphics.Dispose();
                graphics2.Dispose();
                image.Dispose();
                ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
                EncoderParameter parameter = new EncoderParameter(Encoder.Quality, 90L);
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = parameter;
                original.Save(imgLoc, encoderInfo, encoderParams);
                original.Dispose();
                image2.Dispose();
            }
        }
    }

    public static bool JustImage(string imgLoc, int width, int height)
    {
        Image image = Image.FromFile(imgLoc);
        int num = image.Width;
        int num2 = image.Height;
        if ((num < width) || (num2 < height))
        {
            return false;
        }
        return true;
    }

    public static void MakeSpamImageGen(string sessionName, string str)
    {
        HttpContext current = HttpContext.Current;
        int num = 5;
        int num2 = 12;
        int width = 60;
        int height = 20;
        float num5 = (width - (num * (num2 + 0.5f))) / 2f;
        float y = (height - (num2 * 1.7f)) / 2f;
        if (num2 == -1)
        {
            num2 = 30;
        }
        if (width == -1)
        {
            width = 290;
        }
        if (height == -1)
        {
            height = 80;
        }
        string str2 = str;
        current.Session.Add(sessionName, str2);
        Bitmap image = new Bitmap(Image.FromFile(current.Server.MapPath(string.Concat(new object[] { current.Request.ApplicationPath, "/Images/AntiSpamBgImgs/bg_", new Random().Next(5), ".jpg" }))), width, height);
        Graphics graphics = Graphics.FromImage(image);
        SolidBrush brush = new SolidBrush(Color.Black);
        string[] strArray = new string[] { "Arial", "Verdana", "Fixedsys", "宋体", "Haettenschweiler", "Lucida Sans Unicode", "Garamond", "Courier New", "Book Antiqua", "Arial Narrow" };
        for (int i = 0; i < str2.Length; i++)
        {
            string familyName = strArray[new Random().Next(i)];
            Font font = new Font(familyName, (float)num2, FontStyle.Bold);
            graphics.DrawString(str2.Substring(i, 1), font, brush, num5 + (i * num2), y);
            graphics.Flush();
        }
        graphics.Flush();
        graphics.Dispose();
        current.Response.ContentType = "image/gif";
        image.Save(current.Response.OutputStream, ImageFormat.Gif);
        current.Response.Flush();
        current.Response.End();
    }

    private static Size NewSize(int maxWidth, int maxHeight, int width, int height)
    {
        double num = 0.0;
        double num2 = 0.0;
        double num3 = Convert.ToDouble(width);
        double num4 = Convert.ToDouble(height);
        double num5 = Convert.ToDouble(maxWidth);
        double num6 = Convert.ToDouble(maxHeight);
        if ((num3 < num5) && (num4 < num6))
        {
            num = num3;
            num2 = num4;
        }
        else if ((num3 / num4) > (num5 / num6))
        {
            num = maxWidth;
            num2 = (num * num4) / num3;
        }
        else
        {
            num2 = maxHeight;
            num = (num2 * num3) / num4;
        }
        return new Size(Convert.ToInt32(maxHeight), Convert.ToInt32(maxWidth));
    }

    public static void SendSmallImage(string path, int width, int height)
    {
        Image image = null;
        Image image2 = null;
        Graphics graphics = null;
        try
        {
            image = Image.FromFile(path);
            int num = image.Width;
            int num2 = image.Height;
            if (num > width)
            {
                int num3 = width;
                int num4 = (int)((((double)num2) / ((double)num)) * width);
                if (num4 > height)
                {
                    num4 = height;
                    num3 = (int)((((double)num) / ((double)num2)) * height);
                }
                num = num3;
                num2 = num4;
            }
            else if (num2 > height)
            {
                num = (int)((((double)num) / ((double)num2)) * height);
                num2 = height;
            }
            int x = 0;
            int y = 0;
            if (num < width)
            {
                x = (width - num) / 2;
            }
            if (num2 < height)
            {
                y = (height - num2) / 2;
            }
            Size size = new Size(width, height);
            image2 = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(x, y, num, num2), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            image.Dispose();
            MemoryStream stream = new MemoryStream();
            image2.Save(stream, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            if (image != null)
            {
                image.Dispose();
            }
            if (image2 != null)
            {
                image2.Dispose();
            }
            if (graphics != null)
            {
                graphics.Dispose();
            }
        }
    }

    public static void SendSmallImage(string path, string out_path, int width, int height)
    {
        Image image = null;
        Image image2 = null;
        Graphics graphics = null;
        try
        {
            image = Image.FromFile(path);
            int num = image.Width;
            int num2 = image.Height;
            if (num > width)
            {
                int num3 = width;
                int num4 = (int)((((double)num2) / ((double)num)) * width);
                if (num4 > height)
                {
                    num4 = height;
                    num3 = (int)((((double)num) / ((double)num2)) * height);
                }
                num = num3;
                num2 = num4;
            }
            else if (num2 > height)
            {
                num = (int)((((double)num) / ((double)num2)) * height);
                num2 = height;
            }
            int x = 0;
            int y = 0;
            if (num < width)
            {
                x = (width - num) / 2;
            }
            if (num2 < height)
            {
                y = (height - num2) / 2;
            }
            Size size = new Size(width, height);
            image2 = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(x, y, num, num2), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            image.Dispose();
            MemoryStream stream = new MemoryStream();
            image2.Save(stream, ImageFormat.Jpeg);
            image2.Save(out_path, ImageFormat.Jpeg);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            if (image != null)
            {
                image.Dispose();
            }
            if (image2 != null)
            {
                image2.Dispose();
            }
            if (graphics != null)
            {
                graphics.Dispose();
            }
        }
    }

    private static bool ThumbnailCallback()
    {
        return true;
    }

}


public enum wmPosition
{
    MM=0,
    LT = 1,
    LB = 2,
    CT = 3,
    CB = 4,
    RT = 5,
    RB = 6,
    LM = 7,
    RM = 8
}

 

 



