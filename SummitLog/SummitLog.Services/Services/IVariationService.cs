using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Variations
    /// </summary>
    public interface IVariationService
    {
        /// <summary>
        ///     Liefert alle Variationen einer Route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        IList<Variation> GetAllOn(Route route);

        /// <summary>
        ///     Erstellt eine neue Variation einer Route mit einem Schwierigkeitsgrad
        /// </summary>
        /// <param name="route"></param>
        /// <param name="difficultyLevel"></param>
        /// <param name="variationName"></param>
        void Create(Route route, DifficultyLevel difficultyLevel, string variationName);

        /// <summary>
        ///     Löscht eine Variation, wenn diese nicht mehr verwendet wird.
        /// </summary>
        /// <param name="variation"></param>
        void Delete(Variation variation);

        /// <summary>
        ///     Prüft, ob eine Varation noch in Verwendung ist.
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        bool IsInUse(Variation variation);

        /// <summary>
        ///     Speichert die Variante
        /// </summary>
        /// <param name="variation"></param>
        void Save(Variation variation);

        /// <summary>
        ///     Ändert den Schwierigkeitsgrad einer Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="newLevel"></param>
        void ChangeDifficultyLevel(Variation variation, DifficultyLevel newLevel);
    }
}