using System;

using De.BerndNet2000.SummitLog.Exceptions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace De.BerndNet2000.SummitLog.Domain {
    [TestClass]
    public class DifficultyLevelTest {
        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyNameException))]
        public void TestCreateEmptyName() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN
        }

        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        public void TestCreateNormal() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN

            Assert.Fail();
        }

        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateNullGroup() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN
        }

        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyNameException))]
        public void TestUpdateEmptyName() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN
        }

        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        public void TestUpdateNormal() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN

            Assert.Fail();
        }

        /// <summary>
        ///     SUMMARY
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateNullGroup() {
            // Given: GIVEN

            // When: WHEN

            // Then: THEN
        }
    }
}