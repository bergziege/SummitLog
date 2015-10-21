using Neo4jClient;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     Basisklasse für DAOs
    /// </summary>
    public class BaseDao
    {
        /// <summary>
        ///     Erstellt eine neue INstanz der Basiklasse
        /// </summary>
        /// <param name="graphClient"></param>
        public BaseDao(GraphClient graphClient)
        {
            GraphClient = graphClient;
        }

        /// <summary>
        ///     Liefert den GaphClient
        /// </summary>
        public GraphClient GraphClient { get; }
    }
}