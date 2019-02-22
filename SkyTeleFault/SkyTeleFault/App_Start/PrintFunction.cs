using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyTeleFault
{
    public class PrintFunction
    {
        #region excel样式和单元格设置
        /// <summary>
        /// 设置cell样式
        /// </summary>
        /// <param name="workbook">workbook</param>
        /// <param name="ys">1为主标题，2为属性标题，3为文本框居中，4为文本框居左有边框，5为文本框为%类型保留2位有效数字,6为文本框居左无边框，7为文本框居右无边框，8为文本框居中无边框</param>
        /// <returns></returns>
        public static Aspose.Cells.Style GetCellStyle(Aspose.Cells.Workbook workbook, string ys)
        {
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
            switch (ys)
            {
                case "1":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 14;//文字大小 
                    style.Font.IsBold = true;//粗体
                    break;
                case "2":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;//文字居中 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 10;//文字大小 
                    style.Font.IsBold = true;//粗体 
                    style.IsTextWrapped = true;//单元格内容自动换行 
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "3":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    //style.Font.IsBold = true;//粗体
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "4":
                    style.HorizontalAlignment = TextAlignmentType.Left;//文字居左有边框
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "5":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Custom = "0.00%";
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "6":
                    style.HorizontalAlignment = TextAlignmentType.Left;//文字居左无边框
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    break;
                case "7":
                    style.HorizontalAlignment = TextAlignmentType.Right;//文字居右无边框
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    break;
                case "8":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居右无边框
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    break;
                case "9":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "10":
                    style.HorizontalAlignment = TextAlignmentType.Left;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "11":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Font.IsBold = true;//粗体
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "L11":
                    style.HorizontalAlignment = TextAlignmentType.Left;
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "C11":
                    style.HorizontalAlignment = TextAlignmentType.Center;
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 8;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "C2":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;//文字居中 
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 12;//文字大小 
                    style.Font.IsBold = true;//粗体 
                    style.IsTextWrapped = true;//单元格内容自动换行 
                    break;
                case "GZ1":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 10;//文字大小
                    style.IsTextWrapped = true;
                    style.Font.IsBold = true;//粗体
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "GZ2":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 9;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                case "HEAD":
                    style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                    style.VerticalAlignment = TextAlignmentType.Center;
                    style.Font.Name = "宋体";//文字字体 
                    style.Font.Size = 11;//文字大小
                    style.IsTextWrapped = true;
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    break;
                default:
                    break;
            }
            return style;
        }

        /// <summary>
        /// 获取Excel列号
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string getlh(int n)
        {
            string lh = string.Empty;
            switch (n)
            {
                case 1:
                    lh = "A";
                    break;
                case 2:
                    lh = "B";
                    break;
                case 3:
                    lh = "C";
                    break;
                case 4:
                    lh = "D";
                    break;
                case 5:
                    lh = "E";
                    break;
                case 6:
                    lh = "F";
                    break;
                case 7:
                    lh = "G";
                    break;
                case 8:
                    lh = "H";
                    break;
                case 9:
                    lh = "I";
                    break;
                case 10:
                    lh = "J";
                    break;
                case 11:
                    lh = "K";
                    break;
                case 12:
                    lh = "L";
                    break;
                case 13:
                    lh = "M";
                    break;
                case 14:
                    lh = "N";
                    break;
                case 15:
                    lh = "O";
                    break;
                case 16:
                    lh = "P";
                    break;
                case 17:
                    lh = "Q";
                    break;
                case 18:
                    lh = "R";
                    break;
                case 19:
                    lh = "S";
                    break;
                case 20:
                    lh = "T";
                    break;
                case 21:
                    lh = "U";
                    break;
                case 22:
                    lh = "V";
                    break;
                case 23:
                    lh = "W";
                    break;
                case 24:
                    lh = "X";
                    break;
                case 25:
                    lh = "Y";
                    break;
                case 26:
                    lh = "Z";
                    break;
                default:
                    break;
            }
            return lh;
        }
        #endregion
    }
}