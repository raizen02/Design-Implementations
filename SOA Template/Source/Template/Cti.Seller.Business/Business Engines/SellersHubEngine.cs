using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Cti.Seller.Business.Common;

using Core.Common.Contracts;
using Cti.Seller.Business.Entities;

namespace Cti.Seller.Business
{

    /* Responsible for handling business rules/validations
     * 
    */

    [Export(typeof(ISellersHubEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SellersHubEngine : ISellersHubEngine
    {
        [ImportingConstructor]
        public SellersHubEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IDataRepositoryFactory _DataRepositoryFactory;


        // Sample Function in the engine
        public bool IsUnitAvailable(int UnitId, Unit[] ReservedUnits)
        {
            bool available = true;
            Unit reserved = ReservedUnits.Where(unit => unit.UnitId == UnitId).FirstOrDefault();
            if (reserved != null)
            {
                available = false;
            }

            return available;
        }
    }
}
