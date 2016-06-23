using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class CodeHighlighter
    {
        public string bar_code(object str, int ch, int cw, int type_code)
        {
            //str:输入的字符串;ch:要显示条形码的高度;cw:要显示条码的宽度;type_code:代码类型
            string strTmp = str.ToString();
            string code = strTmp;
            // ToLower()将string转化成小写形式的副本，返回是使用指定区域的性的大小写规则。
            strTmp = strTmp.ToLower();
            int height = ch;
            int width = cw;

            //将传入的参数进行转化。
            strTmp = strTmp.Replace("0", "_|_|__||_||_|"); ;
            strTmp = strTmp.Replace("1", "_||_|__|_|_||");
            strTmp = strTmp.Replace("2", "_|_||__|_|_||");
            strTmp = strTmp.Replace("3", "_||_||__|_|_|");
            strTmp = strTmp.Replace("4", "_|_|__||_|_||");
            strTmp = strTmp.Replace("5", "_||_|__||_|_|");
            strTmp = strTmp.Replace("7", "_|_|__|_||_||");
            strTmp = strTmp.Replace("6", "_|_||__||_|_|");
            strTmp = strTmp.Replace("8", "_||_|__|_||_|");
            strTmp = strTmp.Replace("9", "_|_||__|_||_|");
            strTmp = strTmp.Replace("a", "_||_|_|__|_||");
            strTmp = strTmp.Replace("b", "_|_||_|__|_||");
            strTmp = strTmp.Replace("c", "_||_||_|__|_|");
            strTmp = strTmp.Replace("d", "_|_|_||__|_||");
            strTmp = strTmp.Replace("e", "_||_|_||__|_|");
            strTmp = strTmp.Replace("f", "_|_||_||__|_|");
            strTmp = strTmp.Replace("g", "_|_|_|__||_||");
            strTmp = strTmp.Replace("h", "_||_|_|__||_|");
            strTmp = strTmp.Replace("i", "_|_||_|__||_|");
            strTmp = strTmp.Replace("j", "_|_|_||__||_|");
            strTmp = strTmp.Replace("k", "_||_|_|_|__||");
            strTmp = strTmp.Replace("l", "_|_||_|_|__||");
            strTmp = strTmp.Replace("m", "_||_||_|_|__|");
            strTmp = strTmp.Replace("n", "_|_|_||_|__||");
            strTmp = strTmp.Replace("o", "_||_|_||_|__|");
            strTmp = strTmp.Replace("p", "_|_||_||_|__|");
            strTmp = strTmp.Replace("r", "_||_|_|_||__|");
            strTmp = strTmp.Replace("q", "_|_|_|_||__||");
            strTmp = strTmp.Replace("s", "_|_||_|_||__|");
            strTmp = strTmp.Replace("t", "_|_|_||_||__|");
            strTmp = strTmp.Replace("u", "_||__|_|_|_||");
            strTmp = strTmp.Replace("v", "_|__||_|_|_||");
            strTmp = strTmp.Replace("w", "_||__||_|_|_|");
            strTmp = strTmp.Replace("x", "_|__|_||_|_||");
            strTmp = strTmp.Replace("y", "_||__|_||_|_|");
            strTmp = strTmp.Replace("z", "_|__||_||_|_|");
            strTmp = strTmp.Replace("-", "_|__|_|_||_||");
            strTmp = strTmp.Replace("*", "_|__|_||_||_|");
            strTmp = strTmp.Replace("/", "_|__|__|_|__|");
            strTmp = strTmp.Replace("%", "_|_|__|__|__|");
            strTmp = strTmp.Replace("+", "_|__|_|__|__|");
            strTmp = strTmp.Replace(".", "_||__|_|_||_|");
            strTmp = strTmp.Replace("_", "<span style='height:" + height + ";width:" + width + ";background:#FFFFFF;'></span>");
            strTmp = strTmp.Replace("|", "<span style='height:" + height + ";width:" + width + ";background:#000000;'></span>");

            if (type_code == 1)
            {
                return strTmp + "<BR>" + code;
            }
            else
            {
                return strTmp;
            }
        }


        public void CreateCodeLogo(string code)
        {

            long len = code.Length;
            string lastString = "";
            char[] list = new char[len + 1];


            list = code.ToCharArray();

            for (int i = 0; i < list.Length; i++)
            {
                lastString += this.ConvertToBinaryString(list[i].ToString());
            }

            char[] numList = new char[lastString.Length + 1];
            numList = lastString.ToCharArray();


            Bitmap image = new Bitmap(200, 140);
            Graphics g = Graphics.FromImage(image);

            g.Clear(Color.White);

            Pen penBlack = new Pen(Color.FromArgb(255, 0, 0, 0), 2.5F);
            Pen penWhite = new Pen(Color.White, 2.5F);

            int j = 0;

            for (float k = 10; j < numList.Length; k += 2F, j++)
            {
                if (numList[j].ToString() == "1")
                {
                    g.DrawLine(penBlack, k, 10, k, 110);

                }
                else
                {
                    g.DrawLine(penWhite, k, 10, k, 110);
                }

                if (j % 4 == 0)
                {
                    g.DrawString(list[j / 4].ToString(), new System.Drawing.Font("Courier New", 12), new SolidBrush(Color.Red), k, 112);
                }
            }
            image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }


        //将字符串数值转换为二进制字符串数值
        public string ConvertToBinaryString(string buf)
        {
            int[] temp = new int[20];
            string binary;
            int val = 0, i = 0, j;

            //先将字符转化为十进制数
            try
            {
                val = Convert.ToInt32(buf);
            }
            catch
            {
                val = 0;
            }

            if (val == 0)
            {
                return ("0000");
            }

            i = 0;
            while (val != 0)
            {
                temp[i++] = val % 2;
                val /= 2;
            }

            binary = "";

            for (j = 0; j <= i - 1; j++)
            {
                binary += (char)(temp[i - j - 1] + 48);
            }

            if (binary.Length < 4)   //如果小于4位左边补零
            {
                int len = 4 - binary.Length;
                string str = "";

                while (len > 0)
                {
                    str += "0";
                    len--;
                }

                binary = str + binary;
            }

            return (binary);
        }



    }
}