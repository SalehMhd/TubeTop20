//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Top20Video.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Video
    {
        public long Id { get; set; }
        public string YtId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CountryCode { get; set; }
        public string Title { get; set; }
        public string ETag { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PublishedAt { get; set; }
        public Nullable<long> ViewsCount { get; set; }
        public string Duration { get; set; }
        public string VideoUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string PublishedBy { get; set; }
        public string Channel { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string RelevanceLanguage { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
