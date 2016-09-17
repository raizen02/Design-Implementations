using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace Cti.Seller.Business.Entities
{
    [DataContract]
    public class Unit : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int UnitId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }


        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public int ProjectId { get; set; }


        [DataMember]
        public int PhaseId { get; set; }


        [DataMember]
        public string Block { get; set; }

        [DataMember]
        public string InventoryUnit { get; set; }

        [DataMember]
        public string ProductType { get; set; }


        [DataMember]
        public string AllocationStatus { get; set; }


        [DataMember]
        public string UnitModel { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return UnitId; }
            set { UnitId = value; }
        }

        #endregion
    }
}
