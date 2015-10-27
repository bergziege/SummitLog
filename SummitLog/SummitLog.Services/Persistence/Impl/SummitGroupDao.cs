using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class SummitGroupDao : BaseDao, ISummitGroupDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public SummitGroupDao(GraphClient graphClient) : base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Gipfelgruppen einer Gegend
        /// </summary>
        /// <returns></returns>
        public IList<SummitGroup> GetAllIn(Area area)
        {
            return GraphClient.Cypher.Match("(a:Area)-[:HAS]->(sg:SummitGroup)")
                .Where((Area a) => a.Id == area.Id).Return(sg => sg.As<SummitGroup>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Gipfelgruppe in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <param name="summitGroup"></param>
        public void Create(Area area, SummitGroup summitGroup)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("(a:Area)")
                .Where((Area a) => a.Id == area.Id)
                .Create("a-[:HAS]->(summitGroup:SummitGroup {summitGroup})")
                .WithParam("summitGroup", summitGroup);

            query.ExecuteWithoutResults();
        }
    }
}