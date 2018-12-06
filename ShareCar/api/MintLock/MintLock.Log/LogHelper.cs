using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLock.Log
{
    public class LogHelper
    {
        public ILog GetLogger(Type t)
        {
            return LogManager.GetLogger(t);
        }

        public void WriteLog(string msg)
        {
            var logger = this.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(msg);
        }
    }
}
