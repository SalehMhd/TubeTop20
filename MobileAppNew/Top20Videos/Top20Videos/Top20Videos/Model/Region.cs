using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Top20Videos
{
    [DataContract]
    public class Region
    {
        [DataMember]
        public string EncryptedID { get; set; }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string LatLong { get; set; }
    }
}
