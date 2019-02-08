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

namespace Top20Video.Repository
{
    public class TrendingService : ITrendingService
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public TrendingService()
        {
            _unitOfWork = new EfUnitOfWork();
        }

        public List<TrendingModel> GetList(int categoryId, string regionCode)
        {
            List<TrendingModel> model = new List<TrendingModel>();
            try
            {
                var list = _unitOfWork.RepoTrending.Where(x => !(x.IsDeleted ?? false));


                if (categoryId > 0)
                {
                    list = list.Where(x => x.CategoryId == categoryId);
                }

                if (!string.IsNullOrEmpty(regionCode))
                {
                    list = list.Where(x => x.CountryCode.ToLower() == regionCode.ToLower());
                }
                else
                {
                    list = list.Where(x => x.CountryCode.ToLower() == utilityHelper.DefaultRegion.ToLower());
                }

                model = list.OrderBy(x => x.Category.DisplayOrder).ThenByDescending(x => x.ViewsCount).Select(x => new TrendingModel
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

        public bool Save(List<Trending> dbList)
        {
            try
            {
                if (dbList != null && dbList.Count() > 1)
                {
                    MarkDeleted();
                    foreach (var item in dbList)
                    {
                        _unitOfWork.RepoTrending.Add(item);
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
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return false;
        }

        public void RemoveOldVideo()
        {
            try
            {
                string query = "exec dbo.RemoveOldVideo";
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

        public void MarkDeleted()
        {
            try
            {
                using (dbTop20Video_9359Entities ctx = new dbTop20Video_9359Entities())
                {
                    string query = string.Format("exec dbo.MarkDeletedTrending");
                    ctx.Database.ExecuteSqlCommand(query);
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
        }

    }
}
