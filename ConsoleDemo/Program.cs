using ConsoleDemo.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>() { 0,1,2};
            IEnumerable<string> strs = ints.Select(i =>
            Task.Run(async () => await Int2StringAsync(i)).Result);

            Console.WriteLine(strs.FirstOrDefault());
        }

        static async Task<string> Int2StringAsync(int i)
        {
            return await Task.Run<string>(() => i.ToString());
        }
    }
}
