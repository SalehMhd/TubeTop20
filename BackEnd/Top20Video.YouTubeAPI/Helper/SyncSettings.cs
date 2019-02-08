using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Data;
using Top20Video.Models;
using Top20Video.Repository;

namespace Top20Video.YouTubeAPI.Helper
{
    class SyncSettings : VideoHelper
    {
        // get the last updated date time.
        // get the duration of sync.
        ISettingService _service;
        private TrendingHelper _trendingHelper;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SyncSettings()
        {
            _service = new SettingService();
            _trendingHelper = new TrendingHelper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsSync()
        {
            var setting = _service.Get();
            return (setting != null && setting.LastUpdate.AddMinutes(setting.Duration) <= UtilityHelper.CurrentDateTime);
        }

        /// <summary>
        /// to sync data between YouTube and Local server.
        /// </summary>
        /// <returns></returns>
        public async Task SyncData()
        {
            XmlConfigurator.Configure();
            Console.WriteLine("=============== Check last sync status ==========");
            if (IsSync())
            {
                Framework.EventLogHandler.WriteLog(new Exception("SYNC"));

                Console.WriteLine("=============== video sync is in process ==========");
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "=============== video sync is in process ==========");

                bool isUpdated = true;
                isUpdated = await UpdateVideos();
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "=============== videos have been synced ==========");

                isUpdated = await _trendingHelper.UpdateTrending();
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "=============== trendings have been synced ==========");

                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "=============== videos have been updated ==========");
                Console.WriteLine("=============== videos have been updated ==========");
                _service.SetLastUpdate(UtilityHelper.CurrentDateTime, isUpdated);
            }
            else
            {
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "Do Not SYNC");

                //bool isUpdated = await UpdateVideos();
                //isUpdated = await _trendingHelper.UpdateTrending();
                //_service.SetLastUpdate(UtilityHelper.CurrentDateTime, isUpdated);
                Console.WriteLine("=============== Sync already done. ==========");
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "=============== Sync already done. ==========");
            }
        }
    }
}
