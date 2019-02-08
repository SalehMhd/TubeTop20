using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;
using Top20Video.Models;

namespace Top20Video.Repository
{
    public interface ILanguageServices
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <returns>list of RegionModel</returns>
        List<LanguageModel> GetLanguageList();
    }
}
