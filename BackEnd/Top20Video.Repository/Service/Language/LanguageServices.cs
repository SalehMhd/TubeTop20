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
   public class LanguageServices:ILanguageServices
    {
        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public LanguageServices()
        {
            //Work Done Till here get repositry of Lang

            _unitOfWork = new EfUnitOfWork();
        }
        /// <summary>
        /// Region details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        #region LanguageModel

        //*** Create a model for data

        public List<LanguageModel> GetLanguageList()
        {
            List<LanguageModel> model = new List<LanguageModel>();
            try
            {
                //model.Add(new LanguageModel() { ID = 1, relevanceLanguage = "en-GB",Status ="1" });
                //model.Add(new LanguageModel() { ID = 2, relevanceLanguage = "af", Status = "1" });
                //model.Add(new LanguageModel() { ID = 3, relevanceLanguage = "fr-CA", Status = "1" });
                //model.Add(new LanguageModel() { ID = 4, relevanceLanguage = "hi", Status = "1" });

                model = _unitOfWork.Repolanguage.Where(x => x.status == "1").Select(x => new LanguageModel
                {
                    ID = x.id,
                    relevanceLanguage = x.relevanceLanguage,
                    RelevanceLanguageName = x.RelevanceLanguageName,
                    Status = x.status

                }).ToList();
               // model.ForEach(x => x.EncryptedID = x.ID.ToString());

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
