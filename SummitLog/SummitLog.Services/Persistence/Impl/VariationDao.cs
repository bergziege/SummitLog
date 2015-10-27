using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class VariationDao : BaseDao, IVariationDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public VariationDao(GraphClient graphClient) : base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Variationen einer Route
        /// </summary>
        /// <returns></returns>
        public IList<Variation> GetAllOn(Route route)
        {
            return GraphClient.Cypher.Match("(r:Route)-[:HAS]->(v:Variation)")
                .Where((Route r) => r.Id == route.Id).Return(v => v.As<Variation>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Variation einer Route zu einer bestimmen Schwierigkeit
        /// </summary>
        public void Create(Variation variation, Route route, DifficultyLevel difficultyLevel)
        {
            var query = GraphClient.Cypher
                .Match("(r:Route),(dl:DifficultyLevel)")
                .Where((Route r, DifficultyLevel dl) => r.Id == route.Id && dl.Id == difficultyLevel.Id)
                .Create("r-[:HAS]->(variation:Variation {variation})-[:HAS]->dl")
                .WithParam("variation", variation);

            query.ExecuteWithoutResults();
        }
    }
}