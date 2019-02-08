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

namespace Top20Video.Repository
{
    /// <summary>
    /// to insert log detail into database
    /// </summary>
    public class SettingService : ISettingService
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public SettingService()
        {
            _unitOfWork = new EfUnitOfWork();
        }



        /// <summary>
        /// Setting details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        #region Setting

        public SettingModel Get()
        {
            SettingModel model = new SettingModel();
            try
            {
                model = _unitOfWork.RepoSyncSetting.GetAll().Select(x => new SettingModel
                {
                    Duration = x.Duration ?? 60,
                    ID = x.Id,
                    IsSuccess = x.IsSuccess ?? false,
                    LastUpdate = x.LastUpdate ?? new DateTime(),
                }).FirstOrDefault();

                if (model == null) { model = new SettingModel(); }
                model.EncryptedID = model.ID.ToString().ToEnctypt();

                return model;
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }


        public SettingModel Set(SettingModel model)
        {
            try
            {
                SyncSetting setting = _unitOfWork.RepoSyncSetting.GetAll().FirstOrDefault();
                if (setting == null)
                {
                    #region Save

                    setting.Duration = model.Duration;

                    #endregion
                }
                else
                {
                    #region Update

                    setting.Duration = model.Duration;
                   
                    #endregion
                }
                _unitOfWork.Commit();
                model.EncryptedID = model.ID.ToString().ToEnctypt();
                return model;
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        public bool SetLastUpdate(DateTime updateOn,bool isUpdated)
        {
            try
            {
                SyncSetting setting = _unitOfWork.RepoSyncSetting.GetAll().FirstOrDefault();
                if (setting != null)
                {
                    setting.LastUpdate = updateOn;
                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return false;
        }

        #endregion

    }
}


