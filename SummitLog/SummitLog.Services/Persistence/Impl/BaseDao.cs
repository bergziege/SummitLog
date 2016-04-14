using Microsoft.Practices.Unity;
using Neo4jClient;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     Basisklasse für DAOs
    /// </summary>
    public class BaseDao
    {
        /// <summary>
        ///     Liefert den GaphClient
        /// </summary>
        [Dependency]
        public GraphClient GraphClient { set; protected get; }
    }
}