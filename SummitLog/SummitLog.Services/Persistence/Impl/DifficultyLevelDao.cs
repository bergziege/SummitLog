using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Cypher;
using SummitLog.Services.Model;

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
            return GraphClient.Cypher.Match("(dls:DifficultyLevelScale)-[:HAS]->(dl:DifficultyLevel)")
                .Where((DifficultyLevelScale dls) => dls.Id == difficultyLevelScale.Id).Return(dl => dl.As<DifficultyLevel>()).Results.ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Schwierigkeitsgrad in einer Skala
        /// </summary>
        public DifficultyLevel Create(DifficultyLevelScale difficultyLevelScale, DifficultyLevel difficultyLevel)
        {
            ICypherFluentQuery query = GraphClient.Cypher
                .Match("(dls:DifficultyLevelScale)")
                .Where((DifficultyLevelScale dls) => dls.Id == difficultyLevelScale.Id)
                .Create("dls-[:HAS]->(d:DifficultyLevel {difficultyLevel})")
                .WithParam("difficultyLevel", difficultyLevel);

            return query.Return(d=>d.As<DifficultyLevel>()).Results.First();
        }
    }
}