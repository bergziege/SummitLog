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
        DifficultyLevelScale Create(DifficultyLevelScale difficultyLevelScale);

        /// <summary>
        ///     Liefert ob eine Scale aktuell in Verwendung ist.
        ///     d.h. ob es Schwierigkeitsgrade zu dieser Skala gibt.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        bool IsInUse(DifficultyLevelScale scale);

        /// <summary>
        ///     Löscht die übergebene Schwierigkeitsgradskala
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        void Delete(DifficultyLevelScale difficultyLevelScale);

        /// <summary>
        ///     Speichert die Schwierigkeitsgradskale
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        void Save(DifficultyLevelScale difficultyLevelScale);
    }
}