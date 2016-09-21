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
    public interface ILocationService : IServiceContract
    {
        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //[FaultContract(typeof(AuthorizationValidationException))]
        //Location[] GetLocations(ProjectParams searchParams);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Location[] GetLocations();



        #region Async operations

        Task<Location[]> GetLocationsAsync();


        #endregion
    }
}
