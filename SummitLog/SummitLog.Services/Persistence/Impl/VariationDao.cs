using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

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
        public Variation Create(Variation variation, Route route, DifficultyLevel difficultyLevel)
        {
            var query = GraphClient.Cypher
                .Match("(r:Route),(dl:DifficultyLevel)")
                .Where((Route r, DifficultyLevel dl) => r.Id == route.Id && dl.Id == difficultyLevel.Id)
                .Create("r-[:HAS]->(v:Variation {variation})-[:HAS]->dl")
                .WithParam("variation", variation);

            return query.Return(v => v.As<Variation>()).Results.First();
        }

        /// <summary>
        /// Liefert ob die Variation noch verwendet wird (ob Logeiträge zur Variation vorhanden sind).
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        public bool IsInUse(Variation variation)
        {
            return GraphClient.Cypher.Match("".Variation("var").AnyOutboundRelationAs("usage").LogEntry())
                .Where((Variation var) => var.Id == variation.Id).Return(var => var.Count()).Results.First() > 0;
        }

        /// <summary>
        ///     Löscht die Variation, wenn diese nicht mehr verwendet wird.
        /// </summary>
        /// <param name="variation"></param>
        public void Delete(Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            if (IsInUse(variation))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("".Route().AnyOutboundRelationAs("routeAssignment").Variation("v").AnyOutboundRelationAs("levelAssignment").DifficultyLevel())
                .Where((Variation v)=>v.Id == variation.Id).Delete("v, levelAssignment, routeAssignment").ExecuteWithoutResults();
        }
    }
}