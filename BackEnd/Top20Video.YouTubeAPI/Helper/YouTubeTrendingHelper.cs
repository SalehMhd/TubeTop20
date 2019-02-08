using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Top20Video.Data;
using Top20Video.Framework;
using Top20Video.Models;

namespace Top20Video.YouTubeAPI.Helper
{
    class YouTubeTrendingHelper
    {
        private string _appName;
        private string _apiKey;
        private int _maxResult;
        private YouTubeService _youTubeService;

        public YouTubeTrendingHelper()
        {
            //_apiKey = ConfigurationManager.AppSettings["ytApiKey"];
            _apiKey = "AIzaSyALsF5cT0btJlaBJzKxadGyVtWfSrxBBgw";
            _maxResult = Convert.ToInt32(ConfigurationManager.AppSettings["MaxResult"]);
            _appName = "Top20Video-YouTube_Key";
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = _appName,
            });
        }

        public async Task<List<Trending>> SyncTrending(string regionCode, CategoryModel category)
        {
            List<Top20Video.Data.Trending> dbList = new List<Data.Trending>();

            try
            {
                var videoRequest = _youTubeService.Videos.List("snippet,contentDetails,Statistics");
                videoRequest.MaxResults = _maxResult;
                videoRequest.RegionCode = regionCode;
                if(category.YtId != 0)
                    videoRequest.VideoCategoryId = category.YtId.ToString();

                videoRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;

                var videoResponse = await videoRequest.ExecuteAsync(CancellationToken.None);
                foreach (var item in videoResponse.Items)
                {
                    Top20Video.Data.Trending dbVideo = new Data.Trending();

                    dbVideo.RelevanceLanguage = "Gn";
                    dbVideo.YtId = item.Id ?? "";
                    dbVideo.ETag = (item.ETag ?? "").Replace("\"", "");
                    dbVideo.CategoryId = category.ID;
                    dbVideo.CountryCode = regionCode;
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
                //Console.WriteLine(e);
                Framework.EventLogHandler.WriteTextLog("\n" + DateTime.Now.ToString() + "\n" + ex.Message + "\n _STACKTRACE_ " + ex.StackTrace);
                throw;
            }
            return dbList;
        }
    }
}
