using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class SummitDao : ISummitDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public SummitDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle Gipfel einer Gipfelgruppe
        /// </summary>
        /// <returns></returns>
        public IList<Summit> GetAllIn(SummitGroup summitGroup)
        {
            return _graphClient.Cypher.Match("(sg:SummitGroup)-[:HAS]->(s:Summit)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id).Return(s => s.As<Summit>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Gipfel in einer Gipfelgruppe
        /// </summary>
        public void Create(SummitGroup summitGroup, Summit summit)
        {
            var query = _graphClient.Cypher
                .Match("(sg:SummitGroup)")
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Create("sg-[:HAS]->(summit:Summit {summit})")
                .WithParam("summit", summit);

            query.ExecuteWithoutResults();
        }
    }
}