using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;
using Core.Common.Contracts;

namespace Cti.Seller.Data.Contracts
{
    public interface ILocationRepository : IDataRepository<Unit>
    {
        IEnumerable<Location> GetLocations(ProjectSearchParams searchParams);
        IEnumerable<Location> GetLocations();
        Location GetLocation(int LocationId);  
    }
}
