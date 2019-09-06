using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Top20Videos
{
    [DataContract]
    public class Video
    {
        [DataMember] public string EncryptedID { get; set; }
        [DataMember] public long ID { get; set; }
        [DataMember] public string YouTubeId { get; set; }
        [DataMember] public string Title { get; set; }
        [DataMember] public string RegionCode { get; set; }
        [DataMember] public string CategoryName { get; set; }
        [DataMember] public int CategoryId { get; set; }
        [DataMember] public int YTCategoryId { get; set; }
        //[DataMember] public DateTime PublishedAt { get; set; }
        [DataMember] public string PublishAgo { get; set; }
        [DataMember] public string Thumbnail { get; set; }
        [DataMember] public string ThumbnailMedium { get; set; }
        [DataMember] public string VideoUrl { get; set; }
        [DataMember] public long ViewCount { get; set; }
        [DataMember] public string ViewDisplay { get; set; }
        [DataMember] public int CategoryDisplayOrder { get; set; }
        //[DataMember] public TimeSpan Duration { get; set; }
        [DataMember] public string Duration { get; set; }
        [DataMember] public string Channel { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string RelevanceLanguage { get; set; }

        public int BindingCategoryIndex { get; set; }
    }
}
