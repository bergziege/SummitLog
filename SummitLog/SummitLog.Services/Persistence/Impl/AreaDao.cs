using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Exceptions;
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

        /// <summary>
        ///     Liefert ob das Gebiet verwendet wird.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool IsInUse(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            var countResult = GraphClient.Cypher.Match("".Area("a"))
                .OptionalMatch("".Node("a").AnyOutboundRelationAs("usageOnRoute").Route())
                .OptionalMatch("".Node("a").AnyOutboundRelationAs("usageOnSummitGroup").SummitGroup())
                .Where((Area a) => a.Id == area.Id)
                .Return((usageOnRoute, usageOnSummitGroup) => new {
                    RouteCountUsageCount = usageOnRoute.Count(),
                    SummitGroupUsageCount = usageOnSummitGroup.Count()
                }).Results.First();
            return countResult.RouteCountUsageCount > 0 || countResult.SummitGroupUsageCount > 0;
        }

        /// <summary>
        ///     Löscht ein Gebiet, wenn dies nicht mehr in Verwendung ist.
        /// </summary>
        /// <param name="area"></param>
        public void Delete(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));

            if (IsInUse(area))
            {
                throw new NodeInUseException();
            }

            GraphClient.Cypher.Match("".Area("a").AnyInboundRelationsAs("usages").Country())
                .Where((Area a)=>a.Id == area.Id)
                .Delete("a, usages").ExecuteWithoutResults();
        }
    }
}