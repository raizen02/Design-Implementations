using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cti.Seller.Client.Proxies.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_inventory_client_connection()
        {
            UnitInventoryClient proxy = new UnitInventoryClient();

            proxy.Open();
        }

        [TestMethod]
        public void test_account_client_connection()
        {
            AccountClient proxy = new AccountClient();

            proxy.Open();
        }

       
    }
}
