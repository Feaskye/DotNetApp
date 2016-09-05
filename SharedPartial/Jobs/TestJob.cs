using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedPartial.Jobs
{
    public class TestJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TestJob));

        public void Execute(IJobExecutionContext context)
        {
            //if (this.DateDiff(DateTime.Now, order.CreateTime) > 30)
            //{
            //    //更改状态
            //}
        }
    }
}