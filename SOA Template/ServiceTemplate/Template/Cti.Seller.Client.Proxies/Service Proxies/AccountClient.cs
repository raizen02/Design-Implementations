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
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountClient : UserClientBase<IAccountService>, IAccountService
    {
        public Account GetCustomerAccountInfo(string loginEmail)
        {
            return Channel.GetCustomerAccountInfo(loginEmail);
        }

        public void UpdateCustomerAccountInfo(Account account)
        {
            Channel.UpdateCustomerAccountInfo(account);
        }

        #region Async operations

        public Task<Account> GetCustomerAccountInfoAsync(string loginEmail)
        {
            return Channel.GetCustomerAccountInfoAsync(loginEmail);
        }

        public Task UpdateCustomerAccountInfoAsync(Account account)
        {
            return Channel.UpdateCustomerAccountInfoAsync(account);
        }

        #endregion
    }
}
