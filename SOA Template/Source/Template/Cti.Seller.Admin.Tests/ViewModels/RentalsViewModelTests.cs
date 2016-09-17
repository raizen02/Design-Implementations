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
    public class RentalsViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            RentalsViewModel viewModel = new RentalsViewModel(mockServiceFactory.Object);

            //Assert.IsTrue(viewModel.Cars == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            //Assert.IsTrue(viewModel.Cars != null && viewModel.Cars.Length == data.Length && viewModel.Cars[0] == data[0]);
        }
    }
}
