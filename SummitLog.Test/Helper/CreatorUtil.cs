using System;

using De.BerndNet2000.SummitLog.Domain;
using De.BerndNet2000.SummitLog.Persistence;

namespace De.BerndNet2000.SummitLog.Helper {
    /// <summary>
    ///     Tool das Methoden zum Erstellen von Domainobjekten bereitstellt.
    /// </summary>
    public class CreatorUtil {

        /// <summary>
        /// Liefert oder setzt einen <see cref="IDifficultyLevelGroupDao"/>
        /// </summary>
        public IDifficultyLevelGroupDao DifficultyLevelGroupDao { private get; set; }

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
        public DifficultyLevelGroup GetRandomDifficultyCategory(bool persist = true) {
            string name = "diffCat_" + _rand.Next();
            
            DifficultyLevelGroup cat = new DifficultyLevelGroup(name);
            if (persist) {
                DifficultyLevelGroupDao.Save(cat);
                DifficultyLevelGroupDao.FlushAndClear();
            }
            return cat;
        }
    }
}