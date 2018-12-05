using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.FactoryTest
{
    interface IFileFactory
    {
        IFile Create();
    }
    public class DocFileFactory : IFileFactory
    {
        public IFile Create()
        {
            return new DocFile();
        }
    }
    public class TxtFileFactory : IFileFactory
    {
        public IFile Create()
        {
            return new TxtFile();
        }
    }
}
