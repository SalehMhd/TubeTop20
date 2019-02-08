using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Models;

namespace Top20Video.Repository
{
    public interface ISettingService
    {
        /// <summary>
        /// to get the setting
        /// </summary>
        /// <returns>Setting detail</returns>
        SettingModel Get();

        /// <summary>
        /// to set the setting
        /// </summary>
        /// <returns>SettingModel model</returns>
        SettingModel Set(SettingModel model);

        /// <summary>
        /// to set the last sync time
        /// </summary>
        /// <param name="updateOn"></param>
        /// <returns></returns>
        bool SetLastUpdate(DateTime updateOn,bool isUpdated);
    }
}
