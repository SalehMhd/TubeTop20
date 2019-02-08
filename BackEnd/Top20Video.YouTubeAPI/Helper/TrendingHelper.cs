using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;
using Top20Video.Models;
using Top20Video.Repository;
using Top20Video.Data;

namespace Top20Video.YouTubeAPI.Helper
{
    public class TrendingHelper
    {
        ICategoryService _category;
        IRegionService _region;
        ITrendingService _trendingService;
        YouTubeTrendingHelper _ytTrendingHelper;
        ILanguageServices _Language;

        public TrendingHelper()
        {
            _category = new CategoryService();
            _region = new RegionService();
            _ytTrendingHelper = new Helper.YouTubeTrendingHelper();
            _trendingService = new TrendingService();
            _Language = new LanguageServices();
        }

        public async Task<bool> UpdateTrending()
        {
            try
            {
                var regions = _region.GetList().Where(r => r.Code != "All");
                var categories = _category.GetList();

                List<Trending> listTrendings = new List<Trending>();

                foreach (var region in regions)
                {
                    foreach (var category in categories)
                    {
                        listTrendings.AddRange(await _ytTrendingHelper.SyncTrending(region.Code, category));
                    }
                }

                if (listTrendings.Any())
                {
                    await SyncToDB(listTrendings);
                }

                // to remove old video from the local server.
                _trendingService.RemoveOldVideo();
                Console.WriteLine("========= Sync data successfully ");

                return true;
            }

            catch (Exception ex)
            {
                //Framework.EventLogHandler.WriteLog(ex);
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "\n" + ex.Message + "\n _STACKTRACE_ " + ex.StackTrace);
                return false;
            }

            return false;
        }

        private async Task<bool> SyncToDB(List<Trending> listTrending)
        {
            return _trendingService.Save(listTrending);
        }

    }
}
