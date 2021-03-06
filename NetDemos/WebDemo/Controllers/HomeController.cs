﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    /// <summary>
    /// 条形码代码
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UserId = Session["uid"] + "";

            BarCodeEncoder _Code = new BarCodeEncoder();
            _Code.ValueFont = new Font("宋体", 100);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage("PH201606231026418896", BarCodeEncoder.Encode.Code128B);
            var imgPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + "BarCode.gif";
            imgTemp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Gif);
            
            return View();
        }



        public ActionResult About()
        {
            Code39 _Code39 = new Code39();

            _Code39.Height = 120;
            _Code39.Magnify = 1;
            _Code39.ViewFont = new Font("宋体", 20);


            System.Drawing.Image _CodeImage = _Code39.GetCodeImage("PH201606231026418896", Code39.Code39Model.Code39Normal, true);

            System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
            _CodeImage.Save(_Stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            _CodeImage.Save(Server.MapPath(@"\1.jpeg"));

            using (FileStream localFile = new FileStream(Server.MapPath(@"\streamTest.jpeg"),
           FileMode.OpenOrCreate))
            {
                //ms.ToArray()转换为字节数组就是想要的图片源字节

                localFile.Write(_Stream.ToArray(), 0, (int)_Stream.Length);
            }


            //Response.ContentType = "image/jpeg";
            //Response.Clear();
            //Response.BufferOutput = true;
            //Response.BinaryWrite(_Stream.GetBuffer());
            //Response.Flush();

            ViewBag.Message = "Your application description page.";
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }
    }
}