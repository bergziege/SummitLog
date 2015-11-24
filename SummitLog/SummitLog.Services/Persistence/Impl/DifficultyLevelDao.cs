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
    /// DAO für Schwierigkeitsgrade einer Skala
    /// </summary>
    public class DifficultyLevelDao:BaseDao, IDifficultyLevelDao
    {
        /// <summary>
        /// Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public DifficultyLevelDao(GraphClient graphClient): base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Schwierigkeitsgrade einer Skala
        /// </summary>
        /// <returns></returns>
        public IList<DifficultyLevel> GetAllIn(DifficultyLevelScale difficultyLevelScale)
        {
            return GraphClient.Cypher.Match("".DifficultyLevelScale("dls").Has().DifficultyLevel("dl"))
                .Where((DifficultyLevelScale dls) => dls.Id == difficultyLevelScale.Id).Return(dl => dl.As<DifficultyLevel>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Schwierigkeitsgrad in einer Skala
        /// </summary>
        public DifficultyLevel Create(DifficultyLevelScale difficultyLevelScale, DifficultyLevel difficultyLevel)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("".DifficultyLevelScale("dls"))
                .Where((DifficultyLevelScale dls) => dls.Id == difficultyLevelScale.Id)
                .Create("dls".Has().DifficultyLevelWithParam())
                .WithParam("difficultyLevel", difficultyLevel);

            return query.Return(dl=>dl.As<DifficultyLevel>()).Results.First();
        }

        /// <summary>
        ///     Liefert ob ein Schwierigkeitsgrad aktuell verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        /// <returns></returns>
        public bool IsInUse(DifficultyLevel difficultyLevel)
        {
            long usages = GraphClient.Cypher.Match("".DifficultyLevel("dl").AnyInboundRelationsAs("usage").Variation())
                .Where((DifficultyLevel dl)=>dl.Id == difficultyLevel.Id)
                .Return(usage => usage.Count())
                .Results.First();
            return usages >0;
        }

        /// <summary>
        ///     Liefert das verwendete <see cref="DifficultyLevel" /> an einer <see cref="Variation" />
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        public DifficultyLevel GetLevelOnVariation(Variation variation)
        {
            string match = "".Variation("v").Has().DifficultyLevel("dl");
            return
                GraphClient.Cypher.Match(match)
                    .Where((Variation v) => v.Id == variation.Id)
                    .Return(dl => dl.As<DifficultyLevel>())
                    .Results.First();
        }

        /// <summary>
        ///     Löscht den Schwierigkeitsgrad, wenn dieser nicht mehr verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void Delete(DifficultyLevel difficultyLevel)
        {
            if (IsInUse(difficultyLevel))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("".DifficultyLevel("dl")).Where((DifficultyLevel dl)=>dl.Id == difficultyLevel.Id).Delete("dl").ExecuteWithoutResults();
        }
    }
}