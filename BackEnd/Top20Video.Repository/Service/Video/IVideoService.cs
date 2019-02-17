using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Models;

namespace Top20Video.Repository
{
    public interface IVideoService
    {
        /// <summary>
        /// to get filter list with categoryId and regionCode
        /// </summary>
        /// <returns>list of Videos</returns>
        List<VideoModel> GetList(int categoryId, string regionCode,string LanguageCode);


        List<VideoModel> GetAllVideos();

        /// <summary>
        /// To save the list of videos into the database
        /// </summary>
        /// <param name="dbList"></param>
        /// <returns></returns>
        bool Save(List<Top20Video.Data.Video> dbList, string regionCode);

        /// <summary>
        /// to remove the old videos from the server.
        /// </summary>
        /// <param name="regionCode"></param>
        void RemoveOldVideo(string regionCode);

        /// <summary>
        /// to update the video status to deleted once added the updted videos from YouTube server.
        /// </summary>
        /// <param name="regionCode"></param>
        void MarkDeleted(string regionCode);
        /// <summary>
        /// to update the video status to active incase video not sync successfully.
        /// </summary>
        /// <param name="regionCode"></param>
        void UnMarkDeleted(string regionCode);

        IEnumerable<VideoModel> GetVideos(int categoryId, string regionCode);
    }
}
