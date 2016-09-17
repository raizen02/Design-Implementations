using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;

namespace Cti.Seller.Data.Contracts
{
    public class ProjectInfo
    {
        public Account Customer { get; set; }
        public Unit[] units { get; set; }
    }
}