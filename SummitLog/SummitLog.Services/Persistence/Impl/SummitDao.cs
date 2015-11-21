using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class SummitDao :BaseDao, ISummitDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public SummitDao(GraphClient graphClient):base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Gipfel einer Gipfelgruppe
        /// </summary>
        /// <returns></returns>
        public IList<Summit> GetAllIn(SummitGroup summitGroup)
        {
            return GraphClient.Cypher.Match("(sg:SummitGroup)-[:HAS]->(s:Summit)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id).Return(s => s.As<Summit>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Gipfel in einer Gipfelgruppe
        /// </summary>
        public Summit Create(SummitGroup summitGroup, Summit summit)
        {
            var query = GraphClient.Cypher
                .Match("(sg:SummitGroup)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg-[:HAS]->(s:Summit {summit})")
                .WithParam("summit", summit);

            return query.Return(s=>s.As<Summit>()).Results.First();
        }
    }
}