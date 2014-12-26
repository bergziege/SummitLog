using Com.QueoFlow.Persistence.NHibernate.Domain;

namespace De.BerndNet2000.SummitLog.Persistence.Mappings {
    /// <summary>
    ///     FluentNHibernate Mapping für ein DomainEntityWithId Objekt der queo Commons.
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="DomainEntityWithId" />
    /// </typeparam>
    public class DomainEntityWithIdMap<T> : DomainEntityMap<T>
            where T : DomainEntityWithId {
        /// <summary>
        ///     Erzeugt ein neues Mapping zu einer <see cref="DomainEntityWithId" />
        /// </summary>
        protected DomainEntityWithIdMap() {
            Id(x => x.Id).GeneratedBy.Native();
        }
    }
}