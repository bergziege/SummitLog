﻿using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Schwierigkeitsgrade
    /// </summary>
    public interface IDifficultyLevelService
    {
        /// <summary>
        ///     Liefert alle Schwierigkeitsgrade einer Skala
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        IList<DifficultyLevel> GetAllIn(DifficultyLevelScale scale);

        /// <summary>
        ///     Erstellt einen neuen Schwierigkeitsgrad innerhalb einers Skala.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        DifficultyLevel Create(DifficultyLevelScale scale, string name, int score);

        /// <summary>
        ///     Liefert ob der Schwierigkeitsgrad verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        /// <returns></returns>
        bool IsInUse(DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Löscht den Schwierigkeitsgrad, wenn dieser nicht verwendet wird.
        /// </summary>
        /// <param name="difficultyLevel"></param>
        void Delete(DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Speichert den Schwierigkeitsgrad
        /// </summary>
        /// <param name="difficultyLevel"></param>
        void Save(DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Liefert den Schwierigkeitsgrad einer Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        DifficultyLevel GetForVariation(Variation variation);
    }
}