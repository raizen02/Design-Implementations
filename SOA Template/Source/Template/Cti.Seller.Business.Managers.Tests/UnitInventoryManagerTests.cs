using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Cti.Seller.Business.Entities;
using Cti.Seller.Data.Contracts;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cti.Seller.Business.Managers.Tests
{
    [TestClass]
    public class UnitInventoryManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            GenericPrincipal principal = new GenericPrincipal(
               new GenericIdentity("Seller"), new string[] { "Administrators", "SellerAdmin" });
            Thread.CurrentPrincipal = principal;
        }
     }
}
