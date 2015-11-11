using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Gegendem in einem Land
    /// </summary>
    public class AreaDao : BaseDao, IAreaDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public AreaDao(GraphClient graphClient) : base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Gegenden in einem Land
        /// </summary>
        /// <returns></returns>
        public IList<Area> GetAllIn(Country country)
        {
            return GraphClient.Cypher.Match("(c:Country)-[:HAS]->(a:Area)")
                .Where((Country c) => c.Id == country.Id).Return(a => a.As<Area>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Gegend in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="area"></param>
        public void Create(Country country, Area area)
        {
            var query = GraphClient.Cypher
                .Match("(c:Country)")
                .Where((Country c) => c.Id == country.Id)
                .Create("c-[:HAS]->(area:Area {area})")
                .WithParam("area", area);

            query.ExecuteWithoutResults();
        }
    }
}