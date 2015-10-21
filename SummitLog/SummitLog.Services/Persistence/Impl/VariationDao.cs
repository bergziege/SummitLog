using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    /// DAO für Gegendem in einem Land
    /// </summary>
    public class VariationDao: IVariationDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        /// Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public VariationDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle Variationen einer Route
        /// </summary>
        /// <returns></returns>
        public IList<Variation> GetAllOn(Route route)
        {
            return _graphClient.Cypher.Match("(r:Route)-[:HAS]->(v:Variation)")
                .Where((Route r) => r.Id == route.Id).Return(v => v.As<Variation>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Variation einer Route zu einer bestimmen Schwierigkeit
        /// </summary>
        public void Create(Variation variation, Route route, DifficultyLevel difficultyLevel)
        {
            ICypherFluentQuery query = _graphClient.Cypher
                .Match("(r:Route),(dl:DifficultyLevel)")
                .Where((Route r, DifficultyLevel dl) => r.Id == route.Id && dl.Id == difficultyLevel.Id)
                .Create("r-[:HAS]->(variation:Variation {variation})-[:HAS]->dl")
                .WithParam("variation", variation);

            query.ExecuteWithoutResults();
        }
    }
}