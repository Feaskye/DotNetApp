using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.FactoryTest
{
    public interface IFile
    {
        void New();
        void Save();
    }
    public class DocFile : IFile
    {
        public void New()
        {
            Console.WriteLine("New Doc Create");
        }
        public void Save()
        {
            Console.WriteLine("Save Doc");
        }
    }
    public class TxtFile : IFile
    {
        public void New()
        {
            Console.WriteLine("New Txt Create");
        }
        public void Save()
        {
            Console.WriteLine("Save Txt");
        }
    }
}
