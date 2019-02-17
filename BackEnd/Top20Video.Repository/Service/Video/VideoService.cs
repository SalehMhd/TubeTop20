using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Data;
using Top20Video.Framework;
using Top20Video.Models;
using MoreLinq;

//using System.Web.Script.Serialization;


namespace Top20Video.Repository
{
    /// <summary>
    /// to insert log detail into database
    /// </summary>
    public class VideoService : IVideoService
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public VideoService()
        {
            _unitOfWork = new EfUnitOfWork();
        }

        /// <summary>
        /// Video details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        public List<VideoModel> GetList(int categoryId, string regionCode,string LanguageCode)
        {
            List<VideoModel> model = new List<VideoModel>();
            try
            {
                var list = _unitOfWork.RepoVideo.Where(x => !(x.IsDeleted ?? false));
              

                if (categoryId > 0)
                {
                    list = list.Where(x => x.CategoryId == categoryId);
                }
             

                if(!string.IsNullOrEmpty(LanguageCode))
                {
                    //if(LanguageCode == "Gn")
                    //{ }
                    //else
                    //{ list = list.Where(x => x.RelevanceLanguage.ToLower() == LanguageCode.ToLower()); }
                    if(LanguageCode.ToLower() != "gn")
                        list = list.Where(x => x.RelevanceLanguage.ToLower() == LanguageCode.ToLower());
                    
                   
                }
                
                if (!string.IsNullOrEmpty(regionCode))
                {
                    list = list.Where(x => x.CountryCode.ToLower() == regionCode.ToLower());
                }
                else
                {
                    list = list.Where(x => x.CountryCode.ToLower() == utilityHelper.DefaultRegion.ToLower());
                }


                if (LanguageCode.ToLower() != "gn")
                {
                    model = list.Where(x => x.Category.Name.ToLower() != "all").OrderBy(x => x.Category.DisplayOrder).ThenByDescending(x => x.ViewsCount).Select(x => new VideoModel
                    {
                        ID = x.Id,
                        CategoryId = x.CategoryId ?? 0,
                        CategoryName = x.Category.Name ?? "",
                        YTCategoryId = x.Category.YtId ?? 0,
                        Channel = x.Channel ?? "",
                        Description = "" ?? "",
                        //Description = x.Description ?? "",
                        Duration = x.Duration ?? "",
                        PublishedAt = x.PublishedAt ?? new DateTime(),
                        RegionCode = x.CountryCode ?? "SE",
                        Thumbnail = x.ThumbImageUrl ?? "",
                        Title = x.Title ?? "",
                        VideoUrl = x.VideoUrl ?? "",
                        YouTubeId = x.YtId ?? "",
                        ViewCount = x.ViewsCount ?? 0,
                        CategoryDisplayOrder = x.Category.DisplayOrder ?? 0,
                        RelevanceLanguage = x.RelevanceLanguage ?? "",
                    }).ToList();
                }
                else
                {
                    var cat = list.Where(x=> x.Category.Name.ToLower() != "all").Select(x => x.CategoryId).Distinct();
                    foreach(int i in cat)
                    {
                        model.AddRange(
                             list.Where(x => x.Category.Id == i).DistinctBy(x=>x.YtId).OrderBy(x => x.Category.DisplayOrder).
                            ThenByDescending(x => x.ViewsCount).Take(20).
                            Select(x => new VideoModel
                        {
                            ID = x.Id,
                            CategoryId = x.CategoryId ?? 0,
                            CategoryName = x.Category.Name ?? "",
                            YTCategoryId = x.Category.YtId ?? 0,
                            Channel = x.Channel ?? "",
                            Description = "" ?? "",
                            //Description = x.Description ?? "",
                            Duration = x.Duration ?? "",
                            PublishedAt = x.PublishedAt ?? new DateTime(),
                            RegionCode = x.CountryCode ?? "SE",
                            Thumbnail = x.ThumbImageUrl ?? "",
                            Title = x.Title ?? "",
                            VideoUrl = x.VideoUrl ?? "",
                            YouTubeId = x.YtId ?? "",
                            ViewCount = x.ViewsCount ?? 0,
                            CategoryDisplayOrder = x.Category.DisplayOrder ?? 0,
                            RelevanceLanguage = x.RelevanceLanguage ?? "",
                        }));
                    }

                }

                //added to get records with top 20 for all as well
                if (categoryId <= 0)
                {
                    //List<string> YTID = new List<string>();

                    var listTopAll = list.Where(x=>x.CategoryId!=4) .DistinctBy(x => x.YtId).OrderByDescending(x => x.ViewsCount).Take(20);
                        //list.GroupBy(o => new { o.YtId }).Select(o => o.FirstOrDefault()).OrderByDescending(x => x.ViewsCount).Take(20);
                    

                    if (listTopAll != null)
                    {
                        foreach(var x in listTopAll)
                        {
                            model.Add(new VideoModel
                            {
                                ID = x.Id,
                                CategoryId = 6,
                                CategoryName ="All",
                                YTCategoryId = 0,
                                Channel = x.Channel ?? "",
                                Description = "" ?? "",
                                //Description = x.Description ?? "",
                                Duration = x.Duration ?? "",
                                PublishedAt = x.PublishedAt ?? new DateTime(),
                                RegionCode = x.CountryCode ?? "SE",
                                Thumbnail = x.ThumbImageUrl ?? "",
                                Title = x.Title ?? "",
                                VideoUrl = x.VideoUrl ?? "",
                                YouTubeId = x.YtId ?? "",
                                ViewCount = x.ViewsCount ?? 0,
                                CategoryDisplayOrder = x.Category.DisplayOrder ?? 0,
                                RelevanceLanguage = x.RelevanceLanguage ?? "",
                            });
                        }
                    }
                }

                //model.ForEach(x => x.EncryptedID = x.ID.ToString());
                //model.ForEach(x => x.VideoUrl = x.YouTubeId.ToMakeVideoLink());
                //model.ForEach(x => x.ThumbnailMedium = x.Thumbnail.ToMakeThumbnail(ThumbnailType.Medium));
                model.ForEach(x => x.PublishAgo = x.PublishedAt.ToTimeDisplay());
                model.ForEach(x => x.ViewDisplay = x.ViewCount.ToViewsCount());
                return model;
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        

        #region For Service use
        public bool Save(List<Video> dbList, string regionCode)
       {
            //var jsonSerialiser = new JavaScriptSerializer();
            //var json = jsonSerialiser.Serialize(aList);
            try
            {
                if (dbList != null && dbList.Count() > 1)
                {
                    MarkDeleted(regionCode);
                    foreach (var item in dbList)
                    {
                        _unitOfWork.RepoVideo.Add(item);
                    }
                    _unitOfWork.Commit();

                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
                //EventLogHandler.WriteLog(ex);
               // UnMarkDeleted(regionCode);
            }
            return false;
        }

        public void RemoveOldVideo(string regionCode)
        {
            try
            {
                string query = "exec dbo.RemoveOldVideo";
                if (!string.IsNullOrEmpty(regionCode))
                {
                    query = string.Format("exec dbo.RemoveOldVideo '{0}'", regionCode);
                }
                using (dbTop20Video_9359Entities ctx = new dbTop20Video_9359Entities())
                {
                    ctx.Database.ExecuteSqlCommand(query);

                    Console.WriteLine("Remove old videos successfully");
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
        }

        public void MarkDeleted(string regionCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(regionCode))
                {
                    using (dbTop20Video_9359Entities ctx = new dbTop20Video_9359Entities())
                    {
                        string query = string.Format("exec dbo.MarkDeleted '{0}'", regionCode);
                        ctx.Database.ExecuteSqlCommand(query);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
        }

        public void UnMarkDeleted(string regionCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(regionCode))
                {
                    using (dbTop20Video_9359Entities ctx = new dbTop20Video_9359Entities())
                    {
                        string query = string.Format("exec dbo.UnMarkDeleted '{0}'", regionCode);
                        ctx.Database.ExecuteSqlCommand(query);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
        }
        #endregion


        public List<VideoModel> GetAllVideos()
        {
            List<VideoModel> model = new List<VideoModel>();
            var listTopAll = _unitOfWork
                .RepoVideo
                .Where(x => !(x.IsDeleted ?? false))
                .ToList()
                .Where(x => x.PublishedAt.HasValue ? DateTime.Now.AddHours(-24) < x.PublishedAt.Value : x.PublishedAt.HasValue)
                .DistinctBy(x => x.YtId)
                .OrderByDescending(x => x.ViewsCount)
                .Take(20);
            foreach (var x in listTopAll)
            {
                model.Add(new VideoModel
                {
                    ID = x.Id,
                    CategoryId = 6,
                    CategoryName = "All",
                    YTCategoryId = 0,
                    Channel = x.Channel ?? "",
                    Description = "" ?? "",
                    //Description = x.Description ?? "",
                    Duration = x.Duration ?? "",
                    PublishedAt = x.PublishedAt ?? new DateTime(),
                    RegionCode = x.CountryCode ?? "SE",
                    Thumbnail = x.ThumbImageUrl ?? "",
                    Title = x.Title ?? "",
                    VideoUrl = x.VideoUrl ?? "",
                    YouTubeId = x.YtId ?? "",
                    ViewCount = x.ViewsCount ?? 0,
                    CategoryDisplayOrder = x.Category.DisplayOrder ?? 0,
                    RelevanceLanguage = x.RelevanceLanguage ?? "",
                });
            }
            model.ForEach(x => x.PublishAgo = x.PublishedAt.ToTimeDisplay());
            model.ForEach(x => x.ViewDisplay = x.ViewCount.ToViewsCount());

            return model;
        }

        public IEnumerable<VideoModel> GetVideos(int categoryId, string regionCode)
        {
            List<VideoModel> model = new List<VideoModel>();
            var listTopCategorical = _unitOfWork
                .RepoVideo
                .Where(x => !(x.IsDeleted ?? false) && (x.CategoryId == categoryId) && (x.CountryCode == regionCode))
                .ToList()
                .Where(x => x.PublishedAt.HasValue ? DateTime.Now.AddHours(-24) < x.PublishedAt.Value : x.PublishedAt.HasValue)
                .DistinctBy(x => x.YtId)
                .OrderByDescending(x => x.ViewsCount)
                .Take(20);
            foreach (var x in listTopCategorical)
            {
                model.Add(new VideoModel
                {
                    ID = x.Id,
                    CategoryId = x.CategoryId.HasValue ? x.CategoryId.Value : 6,
                    CategoryName = x.Category.Name,
                    YTCategoryId = 0,
                    Channel = x.Channel ?? "",
                    Description = "" ?? "",
                    //Description = x.Description ?? "",
                    Duration = x.Duration ?? "",
                    PublishedAt = x.PublishedAt ?? new DateTime(),
                    RegionCode = regionCode,
                    Thumbnail = x.ThumbImageUrl ?? "",
                    Title = x.Title ?? "",
                    VideoUrl = x.VideoUrl ?? "",
                    YouTubeId = x.YtId ?? "",
                    ViewCount = x.ViewsCount ?? 0,
                    CategoryDisplayOrder = x.Category.DisplayOrder ?? 0,
                    RelevanceLanguage = x.RelevanceLanguage ?? "",
                });
            }
            model.ForEach(x => x.PublishAgo = x.PublishedAt.ToTimeDisplay());
            model.ForEach(x => x.ViewDisplay = x.ViewCount.ToViewsCount());

            return model;
        }
    }
}


