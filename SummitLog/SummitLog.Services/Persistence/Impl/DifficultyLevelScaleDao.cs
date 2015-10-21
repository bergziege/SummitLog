using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

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
                GraphClient.Cypher.Match("(difficultyLevelScale:DifficultyLevelScale)")
                    .Return(difficultyLevelScale => difficultyLevelScale.As<DifficultyLevelScale>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue DifficultyLevelScale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void Create(DifficultyLevelScale difficultyLevelScale)
        {
            GraphClient.Cypher.Create("(n:DifficultyLevelScale {difficultyLevelScale})").WithParam("difficultyLevelScale", difficultyLevelScale).ExecuteWithoutResults();
        }
    }
}