using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

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
            return GraphClient.Cypher.Match("".Area("a").Has().SummitGroup("sg"))
                .Where((Area a) => a.Id == area.Id).Return(sg => sg.As<SummitGroup>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Gipfelgruppe in einer Gegend
        /// </summary>
        /// <param name="area"></param>
        /// <param name="summitGroup"></param>
        public SummitGroup Create(Area area, SummitGroup summitGroup)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("".Area("a"))
                .Where((Area a) => a.Id == area.Id)
                .Create("a".Has().SummitGroupWithParam())
                .WithParam("summitGroup", summitGroup);

            return query.Return(sg => sg.As<SummitGroup>()).Results.First();
        }

        /// <summary>
        ///     Liefert ob eine Gipfelgruppe noch verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        public bool IsInUse(SummitGroup summitGroup)
        {
            var countResult = GraphClient.Cypher.Match("".SummitGroup("sg"))
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .OptionalMatch("".Node("sg").AnyOutboundRelationAs("usageOnRoute").Route())
                .OptionalMatch("".Node("sg").AnyOutboundRelationAs("usageOnSummit").Summit())
                .Return((usageOnRoute, usageOnSummit) => new {RouteCountUsageCount = usageOnRoute.Count(),
                    SummitUsageCount =usageOnSummit.Count()}).Results.First();
            return countResult.RouteCountUsageCount > 0 || countResult.SummitUsageCount > 0;
        }

        /// <summary>
        ///     Löscht eine Gipfelgruppen wenn diese nicht mehr verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        public void Delete(SummitGroup summitGroup)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            if (IsInUse(summitGroup))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("".Area().AnyOutboundRelationAs("usage").SummitGroup("sg"))
                .Where((SummitGroup sg) => sg.Id == summitGroup.Id)
                .Delete("sg, usage").ExecuteWithoutResults();
        }

        /// <summary>
        ///     Speichert die Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        public void Save(SummitGroup summitGroup)
        {
            GraphClient.Cypher.Match("".SummitGroup("sg"))
                .Where((SummitGroup sg)=>sg.Id == summitGroup.Id)
                .Set("sg.Name={Name}").WithParam("Name", summitGroup.Name)
                .ExecuteWithoutResults();
        }
    }
}