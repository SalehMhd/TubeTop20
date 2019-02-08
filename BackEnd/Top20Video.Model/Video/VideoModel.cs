using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;

namespace Top20Video.Models
{
    public class VideoModel
    {
        public string EncryptedID { get; set; }

        public long ID { get; set; }

        public string YouTubeId { get; set; }
         
        public string Title { get; set; }

        public string RegionCode { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public int YTCategoryId { get; set; }

        public DateTime PublishedAt { get; set; }

        public string PublishAgo { get; set; }

        public string Thumbnail { get; set; }
        public string ThumbnailMedium { get; set; }

        public string VideoUrl { get; set; }

        public long ViewCount { get; set; }

        public string ViewDisplay { get; set; }

        public int CategoryDisplayOrder { get; set; }

        public string Duration { get; set; }

        public string Channel { get; set; }

        public string Description { get; set; }

        public string RelevanceLanguage { get; set; }

    }

}
