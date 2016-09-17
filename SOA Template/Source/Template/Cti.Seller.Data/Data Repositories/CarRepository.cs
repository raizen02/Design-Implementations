using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Cti.Seller.Business.Entities;
using Cti.Seller.Data.Contracts;
using Core.Common.Extensions;

namespace Cti.Seller.Data
{
    [Export(typeof(ICarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRepository : DataRepositoryBase<Car>, ICarRepository
    {
        protected override Car AddEntity(SellerContext entityContext, Car entity)
        {
            return entityContext.CarSet.Add(entity);
        }

        protected override Car UpdateEntity(SellerContext entityContext, Car entity)
        {
            return (from e in entityContext.CarSet
                    where e.CarId == entity.CarId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Car> GetEntities(SellerContext entityContext)
        {
            return from e in entityContext.CarSet
                   select e;
        }

        protected override Car GetEntity(SellerContext entityContext, int id)
        {
            var query = (from e in entityContext.CarSet
                         where e.CarId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
