using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Business.Entities;
using Core.Common.Contracts;

namespace Cti.Seller.Data.Contracts
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}
