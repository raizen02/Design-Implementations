using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Cti.Seller.Client.Contracts;
using Cti.Seller.Client.Entities;
using Core.Common.ServiceModel;

namespace Cti.Seller.Client.Proxies
{
    [Export(typeof(ILocationService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LocationClient : UserClientBase<ILocationService>, ILocationService
    {
        public Unit[] GetAvailableUnits(ProjectParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<Unit[]> GetAvailableUnitsAsync(ProjectParams searchParams)
        {
            throw new NotImplementedException();
        }


        public Location[] GetLocations()
        {
            return Channel.GetLocations();
        }

        public Task<Location[]> GetLocationsAsync()
        {
            return Channel.GetLocationsAsync();
        }

        public Unit GetUnit(ProjectParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<Unit> GetUnitAsync(ProjectParams searchParams)
        {
            throw new NotImplementedException();
        }
    }
}
