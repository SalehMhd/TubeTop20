using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Videos.Models
{
    public class VideoModel
    {
        [JsonProperty("EncryptedID")]
        public string EncryptedID { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }
        
            [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }
        [JsonProperty("Duration")]
        public string Duration { get; set; }
        [JsonProperty("PublishedAt")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty("RegionCode")]
        public string RegionCode { get; set; }
        [JsonProperty("RegionCodeThumbnail")]
        public string RegionCodeThumbnail { get; set; }
        [JsonProperty("ThumbnailMedium")]
        public string ThumbnailMedium { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("VideoUrl")]
        public string VideoUrl { get; set; }
        [JsonProperty("Channel")]
        public string Channel { get; set; }

        [JsonProperty("YouTubeId")]
        public string YouTubeId { get; set; }

        [JsonProperty("YTCategoryId")]
        public int YTCategoryId { get; set; }

        [JsonProperty("PublishAgo")]
        public string PublishAgo { get; set; }

        [JsonProperty("Thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("ViewCount")]
        public long ViewCount { get; set; }
        [JsonProperty("ViewDisplay")]
        public string ViewDisplay { get; set; }
        [JsonProperty("CategoryDisplayOrder")]
        public int CategoryDisplayOrder { get; set; }

        

    }


}
