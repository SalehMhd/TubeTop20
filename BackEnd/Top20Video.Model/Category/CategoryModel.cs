using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;

namespace Top20Video.Models
{
    public class CategoryModel  
    {
        public string EncryptedID { get; set; }

        public int ID { get; set; }

        public int YtId { get; set; }
        
        public string Name { get; set; }

        public string ETag { get; set; }

        public int DisplayOrder { get; set; }

        public Status Status { get; set; }
    }

}
