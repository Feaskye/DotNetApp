using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace SharedPartial.Jobs
{
    public class JobManage
    {
        private static ISchedulerFactory sf = new StdSchedulerFactory();

        //调度器
        private static IScheduler scheduler;

        /// <summary>
        /// 读取调度器配置文件的开始时间
        /// </summary>
        public static void StartScheduleFromConfig()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                XDocument xDoc = XDocument.Load(Path.Combine(currentDir, "JobScheduler.config"));
                var jobScheduler = from x in xDoc.Descendants("JobScheduler") select x;

                var jobs = jobScheduler.Elements("Job");
                XElement jobDetailXElement, triggerXElement;

                //获取调度器
                scheduler = sf.GetScheduler();

                //声明触发器
                CronTriggerImpl cronTrigger;

                foreach (var job in jobs)
                {
                    //加载程序集joblibaray  
                    Assembly ass = Assembly.LoadFrom(Path.Combine(currentDir + "JobAutoUpdateState\\", job.Element("DllName").Value));

                    //获取任务名字
                    jobDetailXElement = job.Element("JobDetail");
                    //获取任务触发的时间
                    triggerXElement = job.Element("Trigger");

                    JobDetailImpl jobDetail = new JobDetailImpl(jobDetailXElement.Attribute("job").Value,
                                                            jobDetailXElement.Attribute("group").Value,
                                                            ass.GetType(jobDetailXElement.Attribute("jobtype").Value));


                    if (triggerXElement.Attribute("type").Value.Equals("CronTrigger"))
                    {
                        cronTrigger = new CronTriggerImpl(triggerXElement.Attribute("name").Value,
                                                        triggerXElement.Attribute("group").Value,
                                                        triggerXElement.Attribute("expression").Value);
                        //添加定时器
                        scheduler.ScheduleJob(jobDetail, cronTrigger);
                        scheduler.AddJob(,)
                    }
                }

                //开始执行定时器
                scheduler.Start();

            }
            catch (Exception)
            {


            }

        }

        /// <summary>
        /// 关闭定时器
        /// </summary>
        public static void ShutDown()
        {
            if (scheduler != null && !scheduler.IsShutdown)
            {
                scheduler.Shutdown();
            }
        }
    }
}