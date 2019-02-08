using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top20Video.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Top20Video.Models
{
    public class SettingModel
    {
        public string EncryptedID { get; set; }

        public int ID { get; set; }

        [Required]
        [DisplayName("Sync Duration (In Minutes)")]
        public int Duration { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool IsSuccess { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }

}
