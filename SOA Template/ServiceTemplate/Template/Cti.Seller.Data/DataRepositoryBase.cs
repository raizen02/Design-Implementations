using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Contracts;
using Core.Common.Data;

namespace Cti.Seller.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, SellerContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
