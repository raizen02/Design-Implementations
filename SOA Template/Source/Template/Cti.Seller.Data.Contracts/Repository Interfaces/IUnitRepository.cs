using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;
using Core.Common.Contracts;

namespace Cti.Seller.Data.Contracts
{
    public interface IUnitRepository : IDataRepository<Unit>
    {
        IEnumerable<Unit> GetAvailableUnits(ProjectSearchParams searchParams);
        Unit GetUnit(ProjectSearchParams searchParams);  
    }
}
