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
    public class VideoHelper
    {
        ICategoryService _category;
        IRegionService _region;
        IVideoService _video;
        YouTubeHelper _ytHelper;
        ILanguageServices _Language;

        public VideoHelper()
        {
            _category = new CategoryService();
            _region = new RegionService();
            _ytHelper = new Helper.YouTubeHelper();
            _video = new VideoService();
            _Language = new LanguageServices();
        }

        // get the list of videos existing over the local server.


        // get the list from YouTube category and region wise.
        public async Task<bool> UpdateVideos()
        {
            try
            {
                var regions = _region.GetList();
                var langList = _Language.GetLanguageList();//.Where(x => x.relevanceLanguage.ToLower()=="gn");
                var categories = _category.GetList();//.Where(x => x.Name.ToLower() =="all");
                foreach (var region in regions)
                {
                    List<Video> listVideo = new List<Video>();
                    foreach (var category in categories)
                    {
                        foreach (var LangCat in langList)
                        {
                            LangCat.relevanceLanguage = LangCat.relevanceLanguage.ToLower() == "Gn".ToLower() ? "" : LangCat.relevanceLanguage;
                            listVideo.AddRange(await _ytHelper.SyncVideos((category.Name.ToLower() == "all" ? 0 : category.YtId), region, category.ID, (category.Name.ToLower() == "live"), LangCat.relevanceLanguage));
                        }
                    }

                    // save video data into the local database region wise
                    await SyncToDB(listVideo, region.Code);

                    // to remove old video from the local server.
                    _video.RemoveOldVideo(region.Code);
                    Console.WriteLine("========= Sync data successfully of Region: " + region.Code);
                }
                return true;
            }
            catch (Exception ex)
            {
                Framework.EventLogHandler.WriteLog(ex);
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "\n" + ex.Message + "\n _STACKTRACE_ " + ex.StackTrace);
                return false;
            }
            return false;
        }

        // Delete existing videos content from database and Add latest get from youtube.
        private async Task<bool> SyncToDB(List<Video> listVideo, string regionCode)
        {
            return _video.Save(listVideo, regionCode);
        }


    }
}
