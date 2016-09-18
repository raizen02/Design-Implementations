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
    [Export(typeof(IUnitInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnitInventoryClient : UserClientBase<IUnitInventoryService>, IUnitInventoryService
    {
        //public Car UpdateCar(Car car)
        //{
        //    return Channel.UpdateCar(car);
        //}

        //public void DeleteCar(int carId)
        //{
        //    Channel.DeleteCar(carId);
        //}

        //public Car GetCar(int carId)
        //{
        //    return Channel.GetCar(carId);
        //}

        //public Car[] GetAllCars()
        //{
        //    return Channel.GetAllCars();
        //}

        //public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        //{
        //    return Channel.GetAvailableCars(pickupDate, returnDate);
        //}

        //#region Async operations

        //public Task<Car> UpdateCarAsync(Car car)
        //{
        //    return Channel.UpdateCarAsync(car);
        //}

        //public Task DeleteCarAsync(int carId)
        //{
        //    return Channel.DeleteCarAsync(carId);
        //}

        //public Task<Car> GetCarAsync(int carId)
        //{
        //    return Channel.GetCarAsync(carId);
        //}

        //public Task<Car[]> GetAllCarsAsync()
        //{
        //    return Channel.GetAllCarsAsync();
        //}

        //public Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate)
        //{
        //    return Channel.GetAvailableCarsAsync(pickupDate, returnDate);
        //}

        //#endregion
        public Unit[] GetAvailableUnits(ProjectParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<Unit[]> GetAvailableUnitsAsync(ProjectParams searchParams)
        {
            throw new NotImplementedException();
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
