using Com.QueoFlow.Persistence.NHibernate.Domain;

namespace De.BerndNet2000.SummitLog.Domain {
    /// <summary>
    /// Eine Schwierigkeitsgradkategorie
    /// </summary>
    public class DifficultyCategory:DomainEntityWithIdAndName {
        /// <summary>
        /// Erstellt eine neue Schwierigkeitsgradkategorie
        /// </summary>
        /// <param name="name"></param>
        public DifficultyCategory(string name)
                : base(name) {
        }
    }
}