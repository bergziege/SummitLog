using Com.QueoFlow.Persistence.NHibernate.Domain;

using FluentNHibernate.Mapping;

namespace De.BerndNet2000.SummitLog.Persistence.Mappings {
    /// <summary>
    ///     FluentNHibernate Mapping für ein DomainEntity Objekt der queo Commons.
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="DomainEntity" />
    /// </typeparam>
    public class DomainEntityMap<T> : ClassMap<T>
            where T : DomainEntity {
        /// <summary>
        ///     Erzeugt ein neues Mapping zu einer <see cref="DomainEntity" />
        /// </summary>
        protected DomainEntityMap() {
            Map(x => x.BusinessId).Unique();
        }
    }
}