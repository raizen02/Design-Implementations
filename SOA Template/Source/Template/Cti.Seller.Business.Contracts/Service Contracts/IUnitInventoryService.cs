using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Cti.Seller.Business.Entities;
using Cti.Seller.Common;
using Core.Common.Exceptions;

namespace Cti.Seller.Business.Contracts
{
    [ServiceContract]
    public interface IUnitInventoryService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Unit[] GetAvailableUnits(ProjectSearchParams searchParams);

    }


    [ServiceContract]
    public interface ILocationService
    {
        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //[FaultContract(typeof(AuthorizationValidationException))]
        //Location[] GetLocations(ProjectSearchParams searchParams);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Location[] GetLocations();


    }
}
