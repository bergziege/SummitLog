using De.BerndNet2000.SummitLog.Exceptions;
using De.BerndNet2000.SummitLog.Persistence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Domain {
    [TestClass]
    public class DifficultyLevelGroupTest :PersistenceBaseTest{
        /// <summary>
        ///     Wenn eine Schwierigkeitsgradgruppe mit einem leeren Namen erstellt wird
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyNameException))]
        public void TestCreateEmptyName() {
            // Given: nix

            // When: eine Schwierigkeitsgradgruppe mit einem leeren Namen erstellt wird
            new DifficultyLevelGroup("   ");

            // Then: s. Exception
        }

        /// <summary>
        ///     Testet das fehlerfreie Erstellen einer Schwierigkeitsgradgruppe
        /// </summary>
        [TestMethod]
        public void TestCreateNormal() {
            // Given: einen Namen für eine neue Gruppe
            const string NAME = "group 1";
            // When: die Gruppe mit dem Namen erstellt wird
            DifficultyLevelGroup group = new DifficultyLevelGroup(NAME);
            // Then: muss sich der Name üder das Property abrufen lassen.
            Assert.AreEqual(NAME, group.Name);
        }

        /// <summary>
        ///     der Name der Gruppe mit einem leeren Namen aktualisiert werden soll
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyNameException))]
        public void TestUpdateEmptyName() {
            // Given: eine bestehende Gruppe
            DifficultyLevelGroup group = DomainObjectCreatorUtil.GetRandomDifficultyLevelGroup(persist: false);

            // When: der Name mit einem leeren Namen aktualisiert werden soll.
            group.Update("   ");

            // Then: s. Exception
        }

        /// <summary>
        ///     Testet das fehlerfreie Aktualisieren des Namens einer Gruppe
        /// </summary>
        [TestMethod]
        public void TestUpdateNormal() {
            // Given: eine bestehende Gruppe sowie einen neuen Namen
            DifficultyLevelGroup group = DomainObjectCreatorUtil.GetRandomDifficultyLevelGroup(persist: false);
            const string NEW_NAME = "neuerName";

            // When: der Name der bestehenden Gruppe mit dem neuen Namen aktualisiert wird
            group.Update(NEW_NAME);
            // Then: muss sich der neue Name über das Property abrufen lassen.
            Assert.AreEqual(NEW_NAME, group.Name);
        }
    }
}