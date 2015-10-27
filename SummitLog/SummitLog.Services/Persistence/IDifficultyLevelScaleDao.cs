using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Schwierigkeitsgradskalen
    /// </summary>
    public interface IDifficultyLevelScaleDao
    {
        /// <summary>
        ///     Liefert alle Schwierigkeitsgradskalen
        /// </summary>
        /// <returns></returns>
        IList<DifficultyLevelScale> GetAll();

        /// <summary>
        ///     Erstellt eine neue Schwierigkeitsgradskala
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        void Create(DifficultyLevelScale difficultyLevelScale);
    }
}