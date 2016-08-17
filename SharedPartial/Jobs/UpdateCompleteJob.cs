using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedPartial.Jobs
{
    public class UpdateCompleteJob : IJob
    {
        /// <summary>
        ///在Job 中我们必须实现IJob接口中的Execute 这个方法，才能正确的使用Job
        /// </summary>
        public void Execute(IJobExecutionContext context)
        {
            //更改订单处理完成
            //foreach (var item in query)
            //{
            //    if (!string.IsNullOrEmpty(item.ExpressCode))
            //    {
            //        ExpressModel model = ExpressAPI._GetExpress(item.ExpressCode);

            //        //确定 快递已签收  修改订单状态 已完成
            //        if (model.state == "3" && model.ischeck == "1")
            //        {
            //            var order = _ctx.Orders.FirstOrDefault(c => c.OrderId == item.OrderId);

            //            order.OrderStatus = WechatMall.Common.EnumHelper.OrderStatus.Over;
            //            order.FlowStatus = WechatMall.Common.EnumHelper.FlowStatus.Over;
            //            order.UpdateTime = DateTime.Now;

            //            _ctx.SaveChanges();
            //        }
            //    }

            //}
        }
    }
}