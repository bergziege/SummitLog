using Com.QueoFlow.Persistence.NHibernate.Testing.Spring;

using De.BerndNet2000.SummitLog.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Persistence {
    [TestClass]
    public class DifficultyCategoryDaoTest:PersistenceBaseTest {

        [Resource]
        private IDifficultyCategoryDao _difficultyCategoryDao;

        /// <summary>
        ///     Grundlegendes Speichern/Abrufen einer Schwierigkeitskategorie
        /// </summary>
        [TestMethod]
        public void TestSaveGet() {
            // Given: Eine Schwierigkeitskategorie über das Creator Util in der DB
            DifficultyCategory category = DomainObjectCreatorUtil.GetRandomDifficultyCategory();

            // When: anhand der ID abgerufen wird
            DifficultyCategory reloaded = _difficultyCategoryDao.Get(category.Id);

            // Then: muss obige Schwierigkeitskategorie geliefert werden.
            Assert.AreEqual(category, reloaded);
        }
    }
}