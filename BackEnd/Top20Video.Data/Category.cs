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
    
    public partial class Category
    {
        public Category()
        {
            this.Videos = new HashSet<Video>();
            this.Trendings = new HashSet<Trending>();
        }
    
        public int Id { get; set; }
        public Nullable<int> YtId { get; set; }
        public string Name { get; set; }
        public string ETag { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<byte> Status { get; set; }
    
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Trending> Trendings { get; set; }
    }
}
