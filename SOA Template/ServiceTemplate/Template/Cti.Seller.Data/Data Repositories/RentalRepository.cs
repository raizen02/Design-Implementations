using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Cti.Seller.Business.Entities;
using Cti.Seller.Data.Contracts;
using Core.Common.Extensions;

namespace Cti.Seller.Data
{
    [Export(typeof(IRentalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalRepository : DataRepositoryBase<Rental>, IRentalRepository
    {
        protected override Rental AddEntity(SellerContext entityContext, Rental entity)
        {
            return entityContext.RentalSet.Add(entity);
        }

        protected override Rental UpdateEntity(SellerContext entityContext, Rental entity)
        {
            return (from e in entityContext.RentalSet
                    where e.RentalId == entity.RentalId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Rental> GetEntities(SellerContext entityContext)
        {
            return from e in entityContext.RentalSet
                   select e;
        }

        protected override Rental GetEntity(SellerContext entityContext, int id)
        {
            var query = (from e in entityContext.RentalSet
                         where e.RentalId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carId)
        {
            using (SellerContext entityContext = new SellerContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.CarId == carId
                            select e;

                return query.ToFullyLoaded();
            }
        }

        public Rental GetCurrentRentalByCar(int carId)
        {
            using (SellerContext entityContext = new SellerContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.CarId == carId && e.DateReturned == null
                            select e;

                return query.FirstOrDefault();
            }
        }

        public IEnumerable<Rental> GetCurrentlyRentedCars()
        {
            using (SellerContext entityContext = new SellerContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.DateReturned == null
                            select e;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByAccount(int accountId)
        {
            using (SellerContext entityContext = new SellerContext())
            {
                var query = from e in entityContext.RentalSet
                            where e.AccountId == accountId
                            select e;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
        {
            using (SellerContext entityContext = new SellerContext())
            {
                var query = from r in entityContext.RentalSet
                            where r.DateReturned == null
                            join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                            join c in entityContext.CarSet on r.CarId equals c.CarId
                            select new CustomerRentalInfo()
                            {
                                Customer = a,
                                Car = c,
                                Rental = r
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
