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
    public class RegionService : IRegionService
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public RegionService()
        {
            _unitOfWork = new EfUnitOfWork();
        }

        /// <summary>
        /// Region details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        #region Region

        public List<RegionModel> GetList()
        {
            List<RegionModel> model = new List<RegionModel>();
            try
            {
                model = _unitOfWork.RepoCountry.Where(x => x.Status == (int)Status.Active).Select(x => new RegionModel
                {
                    ID = x.Id,
                    Name = x.Name ?? "",
                    Code = x.Code ?? "",
                    Status = (Status)x.Status,
                    LatLong = x.LatLot ?? ""
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


