using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleDemo.HttpListenerApp
{
    /// <summary>
    /// HttpRequest逻辑处理 http://www.cnblogs.com/heyangyi/p/5832869.html
    /// </summary>
    public class HttpProvider
    {

        private static HttpListener httpFiledownload;  //文件下载处理请求监听
        private static HttpListener httOtherRequest;   //其他超做请求监听

        /// <summary>
        /// 开启HttpListener监听
        /// </summary>
        public static void Init()
        {
            httpFiledownload = new HttpListener(); //创建监听实例
            httpFiledownload.Prefixes.Add("http://127.0.0.1:11610/FileManageApi/Download/"); //添加监听地址 注意是以/结尾。
            httpFiledownload.Start(); //允许该监听地址接受请求的传入。
            Thread ThreadhttpFiledownload = new Thread(new ThreadStart(GethttpFiledownload)); //创建开启一个线程监听该地址得请求
            ThreadhttpFiledownload.Start();

            httOtherRequest = new HttpListener();
            httOtherRequest.Prefixes.Add("http://127.0.0.1:11610/BehaviorApi/EmailSend/");  //添加监听地址 注意是以/结尾。
            httOtherRequest.Start(); //允许该监听地址接受请求的传入。
            Thread ThreadhttOtherRequest = new Thread(new ThreadStart(GethttOtherRequest));
            ThreadhttOtherRequest.Start();
        }

        /// <summary>
        /// 执行文件下载处理请求监听行为
        /// </summary>
        public static void GethttpFiledownload()
        {
            while (true)
            {
                HttpListenerContext requestContext = httpFiledownload.GetContext(); //接受到新的请求
                try
                {
                    //reecontext 为开启线程传入的 requestContext请求对象
                    Thread subthread = new Thread(new ParameterizedThreadStart((reecontext) =>
                    {
                        Console.WriteLine("执行文件处理请求监听行为");

                        var request = (HttpListenerContext)reecontext;
                        var image = HttpUtility.UrlDecode(request.Request.QueryString["imgname"]); //接受GET请求过来的参数；
                        string filepath = AppDomain.CurrentDomain.BaseDirectory + image;
                        if (!File.Exists(filepath))
                        {
                            filepath = AppDomain.CurrentDomain.BaseDirectory + "default.jpg";       //下载默认图片
                        }
                        using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, (int)fs.Length); //将文件读到缓存区
                            request.Response.StatusCode = 200;
                            request.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            request.Response.ContentType = "image/jpg";
                            request.Response.ContentLength64 = buffer.Length;
                            var output = request.Response.OutputStream; //获取请求流
                            output.Write(buffer, 0, buffer.Length);     //将缓存区的字节数写入当前请求流返回
                            output.Close();
                        }
                    }));
                    subthread.Start(requestContext); //开启处理线程处理下载文件
                }
                catch (Exception ex)
                {
                    try
                    {
                        requestContext.Response.StatusCode = 500;
                        requestContext.Response.ContentType = "application/text";
                        requestContext.Response.ContentEncoding = Encoding.UTF8;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("System Error");
                        //对客户端输出相应信息.
                        requestContext.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = requestContext.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        //关闭输出流，释放相应资源
                        output.Close();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 执行其他超做请求监听行为
        /// </summary>
        public static void GethttOtherRequest()
        {
            while (true)
            {
                HttpListenerContext requestContext = httOtherRequest.GetContext(); //接受到新的请求
                try
                {
                    //reecontext 为开启线程传入的 requestContext请求对象
                    Thread subthread = new Thread(new ParameterizedThreadStart((reecontext) =>
                    {
                        Console.WriteLine("执行其他超做请求监听行为");
                        var request = (HttpListenerContext)reecontext;
                        var msg = HttpUtility.UrlDecode(request.Request.QueryString["behavior"]); //接受GET请求过来的参数；
                                                                                                  //在此处执行你需要进行的操作>>比如什么缓存数据读取，队列消息处理，邮件消息队列添加等等。

                        request.Response.StatusCode = 200;
                        request.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        request.Response.ContentType = "application/json";
                        requestContext.Response.ContentEncoding = Encoding.UTF8;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { success = true, behavior = msg }));
                        request.Response.ContentLength64 = buffer.Length;
                        var output = request.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }));
                    subthread.Start(requestContext); //开启处理线程处理下载文件
                }
                catch (Exception ex)
                {
                    try
                    {
                        requestContext.Response.StatusCode = 500;
                        requestContext.Response.ContentType = "application/text";
                        requestContext.Response.ContentEncoding = Encoding.UTF8;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("System Error");
                        //对客户端输出相应信息.
                        requestContext.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = requestContext.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        //关闭输出流，释放相应资源
                        output.Close();
                    }
                    catch { }
                }
            }
        }
    }
}
