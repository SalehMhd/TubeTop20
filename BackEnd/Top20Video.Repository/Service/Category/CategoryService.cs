using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;
using Top20Video.Models;

namespace Top20Video.Repository
{
    /// <summary>
    /// to insert log detail into database
    /// </summary>
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public CategoryService()
        {
            _unitOfWork = new EfUnitOfWork();
        }

        /// <summary>
        /// category details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        #region Category

        public List<CategoryModel> GetList()
        {
            List<CategoryModel> model = new List<CategoryModel>();
            try
            {
                model = _unitOfWork.RepoCategory.Where(x => x.Status == (int)Status.Active).Select(x => new CategoryModel
                {
                    ID = x.Id,
                    Name = x.Name,
                    ETag = x.ETag,
                    DisplayOrder = x.DisplayOrder ?? 9999,
                    YtId = x.YtId ?? 0,
                    Status = (Status)x.Status,
                }).ToList();
                model.ForEach(x => x.EncryptedID = x.ID.ToString());

                return model;
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return model;
        } 
        #endregion

    }
}


