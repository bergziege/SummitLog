using Com.QueoFlow.Persistence.NHibernate.Domain;

namespace De.BerndNet2000.SummitLog.Persistence.Mappings {
    /// <summary>
    ///     FluentNHibernate Mapping für ein DomainEntityWithIdAndName Objekt der queo Commons.
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="DomainEntityWithIdAndName" />
    /// </typeparam>
    public class DomainEntityWithIdAndNameMap<T> : DomainEntityWithIdMap<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///     Erzeugt ein neues Mapping zu einer <see cref="DomainEntityWithIdAndName" />
        /// </summary>
        protected DomainEntityWithIdAndNameMap() {
            Map(x => x.Name);
        }
    }
}