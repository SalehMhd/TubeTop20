using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Models;

namespace Top20Video.Repository
{
    public interface ITrendingService
    {
        /// <summary>
        /// to get filter list with categoryId and regionCode
        /// </summary>
        /// <returns>list of Videos</returns>
        List<TrendingModel> GetList(int categoryId, string regionCode);

        /// <summary>
        /// To save the list of videos into the database
        /// </summary>
        /// <param name="dbList"></param>
        /// <returns></returns>
        bool Save(List<Top20Video.Data.Trending> dbList);

        /// <summary>
        /// to remove the old videos from the server.
        /// </summary>
        /// <param name="regionCode"></param>
        void RemoveOldVideo();

        /// <summary>
        /// to update the video status to deleted once added the updted videos from YouTube server.
        /// </summary>
        /// <param name="regionCode"></param>
        void MarkDeleted();

    }
}
