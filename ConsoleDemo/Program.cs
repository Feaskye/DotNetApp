using ConsoleDemo.Demo;
using ConsoleDemo.Helpers;
using ConsoleDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
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


            var person = new Product() { ExpiryDate = DateTime.Now, Name = "小明" };

            var jSettings = new JsonSerializerSettings()
            {
                ContractResolver = new ConverterContractResolver()
            };


            var ss = JsonConvert.SerializeObject(person);
            Console.WriteLine(ss);

            string test = null;
            var ps = JsonConvert.SerializeObject(test, jSettings);
            Console.WriteLine(ps);
        }
        

    }


}
