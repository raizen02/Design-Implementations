using System;
using Cti.Seller.Business.Entities;
using Cti.Seller.Business;
using Cti.Seller.Data.Contracts;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Cti.Seller.Business.Tests
{
    [TestClass]
    public class SellerEngineTests
    {
        [TestMethod]
        public void IsUnitAlreadyReserved()
        {
            //Assemble
            List<Unit> units = new List<Unit>();
            for (int i = 1; i<= 5; i++)
            { 
                Unit _unit = new Unit()
                {
                    UnitId = i
                };
                units.Add(_unit);
           }

            //Act 
            Mock<IUnitRepository> mockUnitRepository = new Mock<IUnitRepository>();
            mockUnitRepository.Setup(obj => obj.GetAvailableUnits(new ProjectSearchParams() { ProjectId = 0, LocationId = 0 })).Returns(units);

            Mock<IDataRepositoryFactory> mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IUnitRepository>()).Returns(mockUnitRepository.Object);

            SellersHubEngine engine = new SellersHubEngine(mockRepositoryFactory.Object);


            //Assert
            bool try1 = engine.IsUnitAvailable(2,units.ToArray());

            Assert.IsFalse(try1);

            bool try2 = engine.IsUnitAvailable(1, units.ToArray());

            Assert.IsTrue(try2);
        }
    }
}
