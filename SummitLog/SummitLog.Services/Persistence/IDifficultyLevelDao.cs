using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Schwierigkeitsgrade in einer Skala
    /// </summary>
    public interface IDifficultyLevelDao
    {
        /// <summary>
        ///     Liefert alle Schwierigkeitsgrade einer Skala
        /// </summary>
        /// <returns></returns>
        IList<DifficultyLevel> GetAllIn(DifficultyLevelScale difficultyLevelScale);

        /// <summary>
        ///     Erstellt einen neuen Schwierigkeitsgrad in einer Skala
        /// </summary>
        void Create(DifficultyLevelScale difficultyLevelScale, DifficultyLevel difficultyLevel);
    }
}