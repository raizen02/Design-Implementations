using System;
using System.Collections.Generic;
using System.Linq;
using Cti.Seller.Admin.ViewModels;
using Cti.Seller.Client.Contracts;
using Cti.Seller.Client.Entities;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cti.Seller.Admin.Tests
{
    [TestClass]
    public class DashboardViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Car[] data = new List<Car>()
            {
                new Car(),
                new Car()
            }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().GetAllCars()).Returns(data);

            DashboardViewModel viewModel = new DashboardViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Cars == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Cars != null && viewModel.Cars.Length == data.Length && viewModel.Cars[0] == data[0]);
        }
    }
}
