using Com.QueoFlow.Persistence.NHibernate.Persistence;

using De.BerndNet2000.SummitLog.Domain;

namespace De.BerndNet2000.SummitLog.Persistence {
    /// <summary>
    ///     Schnittstelle für Daos für <see cref="DifficultyCategory" />
    /// </summary>
    public interface IDifficultyCategoryDao : IDomainEntityWithIdAndNameDao<DifficultyCategory> {
    }
}