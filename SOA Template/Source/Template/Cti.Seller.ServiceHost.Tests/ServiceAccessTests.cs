using System;
using System.ServiceModel;
using Cti.Seller.Business.Contracts;
using Cti.Seller.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cti.Seller.ServiceHost.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_unitInventory_manager_as_service()
        {
            ChannelFactory<IUnitInventoryService> channelFactory =
                new ChannelFactory<IUnitInventoryService>("");

            IUnitInventoryService proxy = channelFactory.CreateChannel();

            (proxy as ICommunicationObject).Open();

            channelFactory.Close();
        }
  
    }
}
