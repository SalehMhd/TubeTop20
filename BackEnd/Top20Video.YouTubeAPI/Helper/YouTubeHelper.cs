using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

using Top20Video.Framework;

namespace Top20Video.YouTubeAPI.Helper
{
    class YouTubeHelper
    {
        private string _appName;
        private string _apiKey;
        private int _maxResult;
        private string _gamingYouTubeId;
        private YouTubeService _youTubeService;

        // connection to service
        // get the category list
        // get the country
        // get the top 10 videos by county and category from the youtube
        // craete a list of all videos into a single list.

        public YouTubeHelper()
        {
            //_apiKey = ConfigurationManager.AppSettings["ytApiKey"];
            _apiKey = "AIzaSyALsF5cT0btJlaBJzKxadGyVtWfSrxBBgw";
            _maxResult = Convert.ToInt32(ConfigurationManager.AppSettings["MaxResult"]);
            _gamingYouTubeId = ConfigurationManager.AppSettings["GamingYouTubeId"];
            //_appName = this.GetType().ToString();
            _appName = "Top20Video-YouTube_Key";
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = _appName,
            });
        }

        private string GetVideoIds(int youTubeCategoryId, Models.RegionModel region, bool isLive,string RLanguage)
        {
            List<string> restrictedVideoId = new List<string>();
            var videoRequest = _youTubeService.Search.List("id");
            videoRequest.MaxResults = _maxResult;
            //Not Required  videoRequest.RegionCode = region.Code; //todo
            //videoRequest.RegionCode = "US";
            #region Language Selector
            if (RLanguage == "")
            {
                videoRequest.Q = "0|1|2|3|4|5|6|7|8|9|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z";
            }
            else
            {
                videoRequest.RelevanceLanguage = RLanguage;
            }
            #endregion
            videoRequest.Type = "video";
            
            videoRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
            videoRequest.PublishedAfter = DateTime.Now.AddDays(-1).ToUniversalTime();
            //videoRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            // videoRequest.VideoLicense = SearchResource.ListRequest.VideoLicenseEnum.Youtube;
            // videoRequest.VideoSyndicated = SearchResource.ListRequest.VideoSyndicatedEnum.True__;
            //videoRequest.Location = region.LatLong; //todo
            //videoRequest.LocationRadius = "1000km";//todo



            if (youTubeCategoryId > 0 && !isLive)
                videoRequest.VideoCategoryId = youTubeCategoryId.ToString();
            else
            {
                restrictedVideoId = GetGamingVideoIds(region, isLive, RLanguage);
                videoRequest.MaxResults = _maxResult * 2;
            }
            if (isLive)
            {
                videoRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
            }
            var videoResponse = videoRequest.Execute();

            string vIds = string.Empty;
            int rIndex = 0;
            foreach (var item in videoResponse.Items)
            {
                if (rIndex >= _maxResult) { break; }
                if (restrictedVideoId.Where(x => x == item.Id.VideoId).Count() == 0) // need to check with
                {
                    vIds += item.Id.VideoId + ",";
                    rIndex++;
                }
            }

            if (!string.IsNullOrEmpty(vIds)) { return vIds.Substring(0, vIds.Length - 1); }
            return vIds;
        }

        private List<string> GetGamingVideoIds(Models.RegionModel region, bool isLive, string RLanguage)
        {
            var videoRequest = _youTubeService.Search.List("id");
            videoRequest.MaxResults = _maxResult;
            //Not Required videoRequest.RegionCode = region.Code; //todo
            //videoRequest.RegionCode = "US";
            #region Language Selector
            if (RLanguage == "")
            {
                videoRequest.Q = "0|1|2|3|4|5|6|7|8|9|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z";
            }
            else
            {
                videoRequest.RelevanceLanguage = RLanguage;
            }
            #endregion
            videoRequest.Type = "video";
            
            videoRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
            videoRequest.PublishedAfter = DateTime.Now.AddDays(-1).ToUniversalTime();

            
            videoRequest.VideoCategoryId = _gamingYouTubeId;
            //videoRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            //videoRequest.VideoLicense = SearchResource.ListRequest.VideoLicenseEnum.Youtube;
            //videoRequest.VideoSyndicated = SearchResource.ListRequest.VideoSyndicatedEnum.True__;
            //videoRequest.Location = region.LatLong;
            //videoRequest.LocationRadius = "1000km";

            if (isLive)
            {
                videoRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
            }
            var videoResponse = videoRequest.Execute();

            List<string> vIds = new List<string>();
            for (int i = 0; i < videoResponse.Items.Count && i < _maxResult; i++)
            {
                vIds.Add(videoResponse.Items[i].Id.VideoId);
            }
            return vIds;
        }

        public async Task<List<Top20Video.Data.Video>> SyncVideos(int youTubeCategoryId, Models.RegionModel region, int categoryId, bool isLive, string RLanguage)
        {
            List<Top20Video.Data.Video> dbList = new List<Data.Video>();
            try
            {
                // first get the videos id's which is latest;

                var videoRequest = _youTubeService.Videos.List("snippet,contentDetails,liveStreamingDetails,Statistics");
                videoRequest.MaxResults = _maxResult;
                //Not Required videoRequest.RegionCode =  region.Code;
                //videoRequest.RegionCode = "US";

                //***The id parameter specifies a comma-separated list of the YouTube video ID(s) for the resource(s) that are being retrieved.
                // In a video resource, the id property specifies the video's ID. (string)

                videoRequest.Id = GetVideoIds(youTubeCategoryId, region, isLive, RLanguage);

                //videoRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
                if (youTubeCategoryId > 0 && !isLive)
                    videoRequest.VideoCategoryId = youTubeCategoryId.ToString();

                var videoResponse = videoRequest.Execute();
                //var videoResponse = await videoRequest.ExecuteAsync();

                foreach (var item in videoResponse.Items)
                {
                    System.Diagnostics.Debug.WriteLine($"Video: {item.Snippet.Title}");

                    Top20Video.Data.Video dbVideo = new Data.Video();

                    dbVideo.RelevanceLanguage = RLanguage == "".ToLower() ? "Gn" : RLanguage;
                    dbVideo.YtId = item.Id ?? "";
                    dbVideo.ETag = (item.ETag ?? "").Replace("\"", "");
                    dbVideo.CategoryId = categoryId;
                    dbVideo.CountryCode = region.Code;
                    if (item.Snippet != null)
                    {
                        dbVideo.PublishedAt = item.Snippet.PublishedAt;
                        dbVideo.Title = item.Snippet.Title ?? "";
                        dbVideo.Description = "Default" ?? "";
                        dbVideo.ThumbImageUrl = item.Snippet.Thumbnails.Default__.Url ?? "";
                        dbVideo.Channel = item.Snippet.ChannelTitle ?? "";
                    }
                    dbVideo.VideoUrl = string.Format("https://www.youtube.com/watch?v={0}", item.Id);
                    if (item.Statistics != null)
                    {
                        dbVideo.ViewsCount = (long?)item.Statistics.ViewCount;
                    }
                    //dbVideo.Duration = item.ContentDetails != null ? (item.ContentDetails.Duration ?? "").Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", "") : "0:0";
                    dbVideo.Duration = item.ContentDetails != null ? (item.ContentDetails.Duration ?? "").ToDurationDisplayFormat() : "0:0";

                    //bind video
                    dbList.Add(dbVideo);
                }
            }
            catch (Exception ex)
            {
                //Framework.EventLogHandler.WriteLog(ex);
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "\n" + ex.Message + "\n _STACKTRACE_ " + ex.StackTrace);
                throw ex;
            }
            return dbList;
        }

    }


}
