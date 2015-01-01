using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Persistence {
    [TestClass]
    public class DifficultyLevelGroupDaoTest:PersistenceBaseTest {

        [Resource]
        private IDifficultyLevelGroupDao _difficultyLevelGroupDao;

        /// <summary>
        ///     Grundlegendes Speichern/Abrufen einer Schwierigkeitskategorie
        /// </summary>
        [TestMethod]
        public void TestSaveGet() {
            // Given: Eine Schwierigkeitskategorie über das Creator Util in der DB
            DifficultyLevelGroup difficultyLevelGroup = DomainObjectCreatorUtil.GetRandomDifficultyLevelGroup();

            // When: anhand der ID abgerufen wird
            DifficultyLevelGroup reloaded = _difficultyLevelGroupDao.Get(difficultyLevelGroup.Id);

            // Then: muss obige Schwierigkeitskategorie geliefert werden.
            Assert.AreEqual(difficultyLevelGroup, reloaded);
        }
    }
}