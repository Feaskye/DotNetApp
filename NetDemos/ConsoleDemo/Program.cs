using ConsoleDemo.Demo;
using ConsoleDemo.Helpers;
using ConsoleDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //var result = "false".Equals(false);

            #region Json.Net字段序列化
            //var person = new Product() { ExpiryDate = DateTime.Now, Name = "小明" };

            //var jSettings = new JsonSerializerSettings()
            //{
            //    NullValueHandling = NullValueHandling.Include,
            //    ContractResolver = new ConverterContractResolver()
            //};


            //var ss = JsonConvert.SerializeObject(person, jSettings);
            //Console.WriteLine(ss);
            #endregion

            //HttpListenerApp.HttpProvider.Init();

        }
        


    }


}
