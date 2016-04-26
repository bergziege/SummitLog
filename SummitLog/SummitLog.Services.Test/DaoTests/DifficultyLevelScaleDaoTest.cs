using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssert;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    [TestClass]
    public class DifficultyLevelScaleDaoTest:DbTestBase
    {

        [TestMethod]
        public void TestCreateAndGetAll()
        {
            IDifficultyLevelScaleDao dao = Container.Resolve<DifficultyLevelScaleDao>();
            DifficultyLevelScale difficultyLevelScale = new DifficultyLevelScale() {Name = "sächsisch"};
            DifficultyLevelScale created = dao.Create(difficultyLevelScale);
            IList<DifficultyLevelScale> allDifficultyLevelScales = dao.GetAll();
            Assert.AreEqual(1, allDifficultyLevelScales.Count);
            Assert.AreEqual(difficultyLevelScale.Name, allDifficultyLevelScales.First().Name);
            Assert.AreEqual(difficultyLevelScale.Id, allDifficultyLevelScales.First().Id);
            Assert.AreEqual(created.Id, allDifficultyLevelScales.First().Id);
        }

        [TestMethod]
        public void TestIfScaleIsInUse()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            DifficultyLevel levelWithScale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(difficultyLevelScale:scale);
            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            Assert.IsTrue(difficultyLevelScaleDao.IsInUse(scale));
        }

        [TestMethod]
        public void TestDeleteScaleNotInUse()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            difficultyLevelScaleDao.Delete(scale);
            Assert.AreEqual(0, difficultyLevelScaleDao.GetAll().Count);
        }

        [TestMethod]
        public void TestDeleteScaleInUse()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            DifficultyLevel levelWithScale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(difficultyLevelScale: scale);
            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            Action action = ()=>difficultyLevelScaleDao.Delete(scale);
            action.ShouldThrow<NodeInUseException>();
        }

        [TestMethod]
        public void TestSave()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale("oldname");

            scale.Name = "newname";
            scale.SetAsDefault();

            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            difficultyLevelScaleDao.Save(scale);

            Assert.AreEqual("newname", difficultyLevelScaleDao.GetAll().First().Name);
            Assert.IsTrue(difficultyLevelScaleDao.GetAll().First().IsDefault);
        }

        [TestMethod]
        public void TestGetForLevel()
        {
            DifficultyLevelScale scale = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale();
            DifficultyLevel level = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevel(difficultyLevelScale: scale);

            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            DifficultyLevelScale scaleForLevel = difficultyLevelScaleDao.GetForDifficultyLevel(level);

            scaleForLevel.Should().NotBeNull();
            scaleForLevel.Id.Should().Be(scale.Id);
        }

        [TestMethod]
        public void TestGetDefaultScale()
        {
            DifficultyLevelScale existingDeault = Container.Resolve<DbTestDataGenerator>().CreateDifficultyLevelScale(isDefault:true);

            IDifficultyLevelScaleDao difficultyLevelScaleDao = Container.Resolve<DifficultyLevelScaleDao>();
            DifficultyLevelScale reloaded = difficultyLevelScaleDao.GetDefaultScale();

            reloaded.Id.Should().Be(existingDeault.Id);
            reloaded.IsDefault.ShouldBeTrue();
        }

        [TestMethod]
        public void TestGetDefaultScaleWithoutDefaultExisting()
        {
            Container.Resolve<DifficultyLevelScaleDao>().GetDefaultScale().ShouldBeNull();
        }
    }
}
