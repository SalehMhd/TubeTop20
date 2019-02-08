using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Videos.Models
{
    public class CategoryModel
    {
        [JsonProperty("EncryptedID")]
        public string EncryptedID { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("YtId")]
        public int YtId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ETag")]
        public string ETag { get; set; }

        [JsonProperty("DisplayOrder")]
        public int DisplayOrder { get; set; }

    }
}
