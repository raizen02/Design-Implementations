using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace Cti.Seller.Business.Entities
{
    [DataContract]
    public class Location : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Barangay { get; set; }

        [DataMember]
        public string Municipality { get; set; }


        [DataMember]
        public string Region { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return Code; }
            set { Code = value; }
        }

        #endregion
    }
}
