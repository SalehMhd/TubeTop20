using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Quartz.Job;
using Quartz;
using Quartz.Impl;

namespace Top20Video.AutoService
{
    public class JobScheduler
    {
        public static string filePath { get; set; }

        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SyncVideoJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SyncVideoFromYouTube", "VideoGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public static async Task<bool> SyncNow()
        {
            //await new Top20Video.YouTubeAPI.Helper.VideoHelper().UpdateVideos();
            //return await new Top20Video.YouTubeAPI.Helper.TrendingHelper().UpdateTrending();
            return true;
        }
    }

    public class SyncVideoJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Start Execution");
            StartService();
        }


        //private async Task Execute(string filePath)
        //{
        //    Process process = new Process();
        //    process.StartInfo.FileName = filePath;
        //    process.Start();
        //}

        public void StartService()//todo: just for testing
        {
            string _filePath = JobScheduler.filePath;
            Task.Run(async () =>
            {
                await YouTubeAPI.cSyncService.Run();
                //await Execute(_filePath);
            });
        }

    }
}
