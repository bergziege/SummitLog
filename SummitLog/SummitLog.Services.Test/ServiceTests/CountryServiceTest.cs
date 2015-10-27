using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using FluentAssert;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class CountryServiceTest
    {
        [Test]
        public void TestGetAll()
        {
            Mock<ICountryDao> countryDaoMock = new Mock<ICountryDao>();
            countryDaoMock.Setup(x => x.GetAll()).Returns(new List<Country> {new Country {Name = "D"}});

            ICountryService countryService = new CountryService(countryDaoMock.Object);
            IList<Country> result = countryService.GetAll();
            Assert.AreEqual(1, result.Count);
            countryDaoMock.Verify(x=>x.GetAll(), Times.Once);
        }

        [Test]
        public void TestCreate()
        {
            Mock<ICountryDao> countryDaoMock = new Mock<ICountryDao>();
            countryDaoMock.Setup(x => x.Create(It.IsAny<Country>()));

            string countryName = "D";

            ICountryService countryService = new CountryService(countryDaoMock.Object);
            countryService.Create(countryName);

            countryDaoMock.Verify(x=>x.Create(It.Is<Country>(y=>y.Name == countryName)));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestMissingName(string name)
        {
            Action act = ()=>new CountryService(null).Create(name);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}