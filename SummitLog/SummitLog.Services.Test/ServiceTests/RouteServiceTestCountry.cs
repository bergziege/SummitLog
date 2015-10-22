
using NUnit.Framework;

namespace SummitLog.Services.Test.ServiceTests
{
    [TestFixture]
    public class RouteServiceTestCountry
    {
        [Test]
        public void TestGetRoutesInCountry()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetRoutesInNullCountry()
        {
            Assert.Fail();
        }

        [Test]
        public void TestCreateRouteInCountry()
        {
            Assert.Fail();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void TestCreateRouteInCountryMissingName(string routeName)
        {
            Assert.Fail();
        }

        [Test]
        public void TestCreateRouteInCountryMissingCountry()
        {
            Assert.Fail();
        }

    }
}
