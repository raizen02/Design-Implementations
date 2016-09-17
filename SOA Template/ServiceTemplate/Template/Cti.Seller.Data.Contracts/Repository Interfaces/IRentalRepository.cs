using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;
using Core.Common.Contracts;

namespace Cti.Seller.Data.Contracts
{
    public interface IRentalRepository : IDataRepository<Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(int carId);
        Rental GetCurrentRentalByCar(int carId);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<Rental> GetRentalHistoryByAccount(int accountId);
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
    }
}
