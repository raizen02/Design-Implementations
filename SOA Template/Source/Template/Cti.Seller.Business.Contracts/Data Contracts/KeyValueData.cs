using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.ServiceModel;

namespace Cti.Seller.Business.Contracts
{
    [DataContract]
    public class KeyValueData : DataContractBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

       
    }
}
