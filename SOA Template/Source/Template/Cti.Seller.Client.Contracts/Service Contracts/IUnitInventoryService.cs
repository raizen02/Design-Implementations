using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Cti.Seller.Client.Entities;
using Cti.Seller.Common;
using Core.Common.Contracts;
using Core.Common.Exceptions;

namespace Cti.Seller.Client.Contracts
{
    [ServiceContract]
    public interface IUnitInventoryService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Unit[] GetAvailableUnits(ProjectParams searchParams);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Unit GetUnit(ProjectParams searchParams);

        #region Async operations

        Task<Unit[]> GetAvailableUnitsAsync(ProjectParams searchParams);

        Task<Unit> GetUnitAsync(ProjectParams searchParams);

        #endregion
    }
}
