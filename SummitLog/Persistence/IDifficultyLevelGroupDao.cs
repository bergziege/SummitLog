using Com.QueoFlow.Persistence.NHibernate.Persistence;

using De.BerndNet2000.SummitLog.Domain;

namespace De.BerndNet2000.SummitLog.Persistence {
    /// <summary>
    ///     Schnittstelle für Daos für <see cref="DifficultyLevelGroup" />
    /// </summary>
    public interface IDifficultyLevelGroupDao : IDomainEntityWithIdAndNameDao<DifficultyLevelGroup> {
    }
}