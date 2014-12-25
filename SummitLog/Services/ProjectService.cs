using Com.QueoFlow.Persistence.NHibernate.Persistence;
using Com.QueoFlow.Persistence.NHibernate.Service;
using Com.QueoFlow.TrackingtoolLogistik.Services.Domain;

namespace Com.QueoFlow.TrackingtoolLogistik.Services.Services {
    public class ProjectService : DomainEntityWithIdAndNameService<Project>, IProjectService {
        
        
        protected override IDomainEntityWithIdAndNameDao<Project> IdAndNameDao {
            get { throw new System.NotImplementedException(); }
        }
        protected override bool IsNameUnique {
            get { throw new System.NotImplementedException(); }
        }
    }
}