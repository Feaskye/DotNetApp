using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.Demo
{
    public class AsyncDemo
    {

        //异步将int转换成string
        static async Task<string> Int2StringAsync(int i)
        {
            return await Task.Run<string>(() => i.ToString());
        }

    }
}
