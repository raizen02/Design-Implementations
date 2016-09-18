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
    public class UnitInventoryManager : ManagerBase, IUnitInventoryService
    {
        public UnitInventoryManager()
        {
        }
        
        public UnitInventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public UnitInventoryManager(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public UnitInventoryManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.SellerAdminRole)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Security.SellerUser)]
        public Unit[] GetAvailableUnits(ProjectSearchParams searchParams)
        {
            throw new NotImplementedException();
        }
    }
}
