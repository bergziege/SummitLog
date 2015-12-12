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
    ///     DAO für DifficultyLevelScale
    /// </summary>
    public class DifficultyLevelScaleDao :BaseDao, IDifficultyLevelScaleDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public DifficultyLevelScaleDao(GraphClient graphClient): base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle DifficultyLevelScale
        /// </summary>
        /// <returns></returns>
        public IList<DifficultyLevelScale> GetAll()
        {
            return
                GraphClient.Cypher.Match("".DifficultyLevelScale("dls"))
                    .Return(dls => dls.As<DifficultyLevelScale>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue DifficultyLevelScale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public DifficultyLevelScale Create(DifficultyLevelScale difficultyLevelScale)
        {
            return GraphClient.Cypher.Create("".DifficultyLevelScaleWithParam()).WithParam("difficultyLevelScale", difficultyLevelScale).Return( dls=>dls.As<DifficultyLevelScale>()).Results.First();
        }

        /// <summary>
        ///     Liefert ob eine Scale aktuell in Verwendung ist.
        ///     d.h. ob es Schwierigkeitsgrade zu dieser Skala gibt.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public bool IsInUse(DifficultyLevelScale scale)
        {
            return
                GraphClient.Cypher.Match("".DifficultyLevelScale("dls").AnyOutboundRelationAs("usage").DifficultyLevel())
                .Where((DifficultyLevelScale dls)=>dls.Id == scale.Id)
                    .Return(usage => usage.Count())
                    .Results.First() > 0;
        }

        /// <summary>
        ///     Löscht die übergebene Schwierigkeitsgradskala
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void Delete(DifficultyLevelScale difficultyLevelScale)
        {
            if (IsInUse(difficultyLevelScale))
            {
                throw new NodeInUseException();
            }
            GraphClient.Cypher.Match("".DifficultyLevelScale("dls")).Where((DifficultyLevelScale dls)=>dls.Id == difficultyLevelScale.Id).Delete("dls").ExecuteWithoutResults();
        }

        /// <summary>
        ///     Speichert die Schwierigkeitsgradskale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void Save(DifficultyLevelScale difficultyLevelScale)
        {
            if (difficultyLevelScale == null) throw new ArgumentNullException(nameof(difficultyLevelScale));
            GraphClient.Cypher.Match("".DifficultyLevelScale("dls"))
                .Where((DifficultyLevelScale dls) => dls.Id == difficultyLevelScale.Id)
                .Set("dls.Name = {name}").WithParam("name", difficultyLevelScale.Name)
                .ExecuteWithoutResults();
        }
    }
}