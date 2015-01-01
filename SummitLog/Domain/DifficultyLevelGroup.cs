using Com.QueoFlow.Persistence.NHibernate.Domain;

namespace De.BerndNet2000.SummitLog.Domain {
    /// <summary>
    ///     Eine Schwierigkeitsgradkategorie
    /// </summary>
    public class DifficultyLevelGroup : DomainEntityWithIdAndName {
        /// <summary>
        ///     Ctor für NHibernate
        /// </summary>
        public DifficultyLevelGroup() {
        }

        /// <summary>
        ///     Erstellt eine neue Schwierigkeitsgradkategorie
        /// </summary>
        /// <param name="name"></param>
        public DifficultyLevelGroup(string name)
                : base(name) {
        }
    }
}