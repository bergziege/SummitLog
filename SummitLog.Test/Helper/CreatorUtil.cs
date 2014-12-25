using System;

using De.BerndNet2000.SummitLog.Domain;

namespace De.BerndNet2000.SummitLog.Helper {
    /// <summary>
    ///     Tool das Methoden zum Erstellen von Domainobjekten bereitstellt.
    /// </summary>
    public class CreatorUtil {
        private Random _rand;
        /// <summary>
        ///     Erstellt eine Instanz der CreatorUtil-Klasse.
        /// </summary>
        public CreatorUtil() {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Erstellt eine Schwierigkeitsgradkategory mit einem zufälligen Namen
        /// </summary>
        /// <returns></returns>
        public DifficultyCategory GetRandomDifficultyCategory() {
            string name = "diffCat_" + _rand.Next();
            return new DifficultyCategory(name);
        }
    }
}