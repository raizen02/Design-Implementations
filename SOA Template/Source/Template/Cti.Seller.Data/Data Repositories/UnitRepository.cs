using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Cti.Seller.Business.Entities;
using Cti.Seller.Data.Contracts;
using Core.Common.Extensions;
using System.Text;
using System.Data.SqlClient;
using Cti.Seller.Data.Data_Repositories;
using System.Data;

namespace Cti.Seller.Data
{
    [Export(typeof(IUnitRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnitRepository : DataRepositoryBase<Unit>, IUnitRepository
    {
        protected override Unit AddEntity(SellerContext entityContext, Unit entity)
        {
            return entityContext.UnitSet.Add(entity);
        }

        protected override Unit UpdateEntity(SellerContext entityContext, Unit entity)
        {
            return (from e in entityContext.UnitSet
                    where e.UnitId == entity.UnitId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Unit> GetEntities(SellerContext entityContext)
        {
            return from e in entityContext.UnitSet 
                   select e;
        }

        protected override Unit GetEntity(SellerContext entityContext, int id)
        {
            var query = (from e in entityContext.UnitSet
                         where e.UnitId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Unit> GetAvailableUnits(ProjectSearchParams searchParams)
        {

            using (SellerContext entityContext = new SellerContext())
            {
                var query = (from e in entityContext.UnitSet
                             where e.ProjectId == searchParams.ProjectId
                                    && e.LocationId == searchParams.LocationId
                                    && e.PhaseId == searchParams.PhaseId
                             select e);


                return query;
            }
        }

        public Unit GetUnit(ProjectSearchParams searchParams)
        {

            using (SellerContext entityContext = new SellerContext())
            {
                var query = (from e in entityContext.UnitSet 
                             where e.ProjectId == searchParams.ProjectId 
                                    &&  e.LocationId == searchParams.LocationId
                                    && e.PhaseId  == searchParams.PhaseId
                             select e);

               return query.FirstOrDefault();
            }
        }

        // Legacy/Transition Approach

        public Unit GetUnit(string SQL, ProjectSearchParams searchParams)
        {
            

            SqlParameter param1 = new SqlParameter("@ProjectId", searchParams.ProjectId );
            SqlParameter param2 = new SqlParameter("@LocationId", searchParams.LocationId);
            SqlParameter[] parameters = new SqlParameter[2] { param1, param2 };

            DataTable tmpDT = DaoHelperMSSQL.GetData(SQL, parameters);

            Unit result = new Unit();

            //Mapping/Conversion tmpDT to result here

            return result;
        }

    }
}
