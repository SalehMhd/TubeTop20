using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using Top20Video.Models;
using Top20Video.Repository;

namespace Top20Video.Web.Api
{
    public class TrendingRecentController : BaseApiController
    {
        ITrendingService trendingService;
        IVideoService videoService;
        ICategoryService categoryService;

        public TrendingRecentController()
        {
            trendingService = new TrendingService();
            videoService = new VideoService();
            categoryService = new CategoryService();
        }

        private List<VideoModel> transformToVideoModel(List<TrendingModel> trendings)
        {
            var videos = new List<VideoModel>();

            foreach (var trendingModel in trendings)
            {
                var vModel = new VideoModel
                {
                    ID = trendingModel.ID,
                    CategoryId = trendingModel.CategoryId,
                    CategoryName = trendingModel.CategoryName,
                    CategoryDisplayOrder = trendingModel.CategoryDisplayOrder,
                    Channel = trendingModel.Channel,
                    Description = trendingModel.Description,
                    Duration = trendingModel.Duration,
                    EncryptedID = trendingModel.EncryptedID,
                    PublishAgo = trendingModel.PublishAgo,
                    PublishedAt = trendingModel.PublishedAt,
                    RegionCode = trendingModel.RegionCode,
                    RelevanceLanguage = trendingModel.RelevanceLanguage,
                    Thumbnail = trendingModel.Thumbnail,
                    ThumbnailMedium = trendingModel.ThumbnailMedium,
                    Title = trendingModel.Title,
                    VideoUrl = trendingModel.VideoUrl,
                    ViewCount = trendingModel.ViewCount,
                    ViewDisplay = trendingModel.ViewDisplay,
                    YTCategoryId = trendingModel.YTCategoryId,
                    YouTubeId = trendingModel.YouTubeId
                };
                videos.Add(vModel);
            }

            return videos;
        }

        public HttpResponseMessage Get(string regionCode)
        {
            var videos = new List<VideoModel>();
            var categories = categoryService.GetList();

            if (regionCode == "All")
            {
                foreach (var categoryModel in categories)
                {
                    videos.AddRange(videoService.GetVideos(categoryModel.ID, regionCode));
                }
            }
            else
            {
                foreach (var categoryModel in categories)
                {
                    videos.AddRange(transformToVideoModel(trendingService.GetList(categoryModel.ID, regionCode)));
                }
            }
            return SuccessResult(videos);
        }

    }
}