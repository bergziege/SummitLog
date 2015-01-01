using Com.QueoFlow.Persistence.NHibernate.Domain;

using De.BerndNet2000.SummitLog.Exceptions;

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
            if (string.IsNullOrWhiteSpace(name)) {
                throw new EmptyNameException();
            }
        }

        public override void Update(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new EmptyNameException();
            }
            base.Update(name);
        }
    }
}