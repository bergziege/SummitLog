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
        /// <param name="variationName"></param>
        /// <param name="route"></param>
        /// <param name="difficultyLevel"></param>
        void Create(string variationName, Route route, DifficultyLevel difficultyLevel);

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
    }
}