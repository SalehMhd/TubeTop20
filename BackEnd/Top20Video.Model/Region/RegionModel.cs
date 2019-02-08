using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;

namespace Top20Video.Models
{
    public class RegionModel  
    {
        public string EncryptedID { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int DisplayOrder { get; set; }

        public Status Status { get; set; }

        public string LatLong { get; set; } //Added on 24-04-17
    }

}
