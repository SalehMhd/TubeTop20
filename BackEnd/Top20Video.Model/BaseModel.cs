using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Video.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            DisplayStatus= true;
        }
        public string EncryptedID { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
        public bool DisplayStatus { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }

}
