using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für DifficultyLevelScale
    /// </summary>
    public class DifficultyLevelScaleDao : IDifficultyLevelScaleDao
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public DifficultyLevelScaleDao(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        /// <summary>
        ///     Liefert alle DifficultyLevelScale
        /// </summary>
        /// <returns></returns>
        public IList<DifficultyLevelScale> GetAll()
        {
            return
                _graphClient.Cypher.Match("(difficultyLevelScale:DifficultyLevelScale)")
                    .Return(difficultyLevelScale => difficultyLevelScale.As<DifficultyLevelScale>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt eine neue DifficultyLevelScale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void Create(DifficultyLevelScale difficultyLevelScale)
        {
            _graphClient.Cypher.Create("(n:DifficultyLevelScale {difficultyLevelScale})").WithParam("difficultyLevelScale", difficultyLevelScale).ExecuteWithoutResults();
        }
    }
}