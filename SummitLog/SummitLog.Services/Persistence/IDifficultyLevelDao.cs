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
        DifficultyLevel Create(DifficultyLevelScale difficultyLevelScale, DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Liefert ob ein Schwierigkeitsgrad aktuell verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        /// <returns></returns>
        bool IsInUse(DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Liefert das verwendete <see cref="DifficultyLevel" /> an einer <see cref="Variation" />
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        DifficultyLevel GetLevelOnVariation(Variation variation);

        /// <summary>
        ///     Löscht den Schwierigkeitsgrad, wenn dieser nicht mehr verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        void Delete(DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Speichert den Schwierigkeitsgrad
        /// </summary>
        /// <param name="difficultyLevel"></param>
        void Save(DifficultyLevel difficultyLevel);
    }
}