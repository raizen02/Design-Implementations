using System;
using Cti.Seller.Client.Bootstrapper;
using Cti.Seller.Client.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using Core.Common.Contracts;

namespace Cti.Seller.Client.Proxies.Tests
{
    [TestClass]
    public class ProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            IUnitInventoryService proxy 
                = ObjectBase.Container.GetExportedValue<IUnitInventoryService>();

            Assert.IsTrue(proxy is UnitInventoryClient);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            IUnitInventoryService proxy = factory.CreateClient<IUnitInventoryService>();

            Assert.IsTrue(proxy is UnitInventoryClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            IServiceFactory factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            IUnitInventoryService proxy = factory.CreateClient<IUnitInventoryService>();

            Assert.IsTrue(proxy is UnitInventoryClient);
        }
    }
}
