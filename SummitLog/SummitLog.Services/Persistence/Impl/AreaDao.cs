using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

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
            return GraphClient.Cypher.Match("".Country("c").Has().Area("a"))
                .Where((Country c) => c.Id == country.Id).Return(a => a.As<Area>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Gegend in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="area"></param>
        public Area Create(Country country, Area area)
        {
            var query = GraphClient.Cypher
                .Match("".Country("c"))
                .Where((Country c) => c.Id == country.Id)
                .Create("c".Has().AreaWithParam())
                .WithParam("area", area);

            return query.Return(a=>a.As<Area>()).Results.First();
        }
    }
}