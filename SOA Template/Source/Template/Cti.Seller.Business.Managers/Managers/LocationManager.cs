using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Cti.Seller.Business.Common;
using Cti.Seller.Business.Contracts;
using Cti.Seller.Business.Entities;
using Cti.Seller.Common;
using Cti.Seller.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System.Security.Permissions;

namespace Cti.Seller.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class LocationManager : ManagerBase, ILocationService
    {
        public LocationManager()
        {
        }
        
        public LocationManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public LocationManager(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public LocationManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

    
        //[OperationBehavior(TransactionScopeRequired = true)]
        ////[PrincipalPermission(SecurityAction.Demand, Role = Security.SellerAdminRole)]
        ////[PrincipalPermission(SecurityAction.Demand, Name = Security.SellerUser)]
        //public Location[] GetLocations(ProjectSearchParams searchParams)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        ILocationRepository locationRepository = _DataRepositoryFactory.GetDataRepository<ILocationRepository>();
             
        //        IEnumerable<Location> locations = locationRepository.GetLocations();
              

        //        return locations.ToArray();
        //    });
        //}

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.SellerAdminRole)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Security.SellerUser)]
        public Location[] GetLocations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ILocationRepository locationRepository = _DataRepositoryFactory.GetDataRepository<ILocationRepository>();

                IEnumerable<Location> locations = locationRepository.GetLocations();


                return locations.ToArray();
            });
        }
    }
}
