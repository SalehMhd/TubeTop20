using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Videos.Models
{
    public class Data
    {
        [JsonProperty("CategoryID")]
        public string CategoryID { get; set; }
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
