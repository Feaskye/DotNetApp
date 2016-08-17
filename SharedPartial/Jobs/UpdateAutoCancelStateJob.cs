using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedPartial.Jobs
{
    public class UpdateAutoCancelStateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (this.DateDiff(DateTime.Now, order.CreateTime) > 30)
            {
                //更改状态
            }
        }

        //计算时间差的方法
        private int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new
            TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            // dateDiff = ts.Days.ToString()+"天"+ ts.Hours.ToString()+"小时"+ ts.Minutes.ToString()+"分钟"+ ts.Seconds.ToString()+"秒";
            dateDiff = ts.Minutes;

            return dateDiff;
        }
    }
}