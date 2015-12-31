using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Variationen
    /// </summary>
    public interface IVariationDao
    {
        /// <summary>
        ///     Liefert alle Variationen einer Route
        /// </summary>
        /// <returns></returns>
        IList<Variation> GetAllOn(Route route);

        /// <summary>
        ///     Erstellt eine neue Variation einer Route zu einer bestimmen Schwierigkeit
        /// </summary>
        Variation Create(Variation variation, Route route, DifficultyLevel difficultyLevel);

        /// <summary>
        ///     Liefert ob die Variation noch verwendet wird (ob Logeiträge zur Variation vorhanden sind).
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        bool IsInUse(Variation variation);

        /// <summary>
        ///     Löscht die Variation, wenn diese nicht mehr verwendet wird.
        /// </summary>
        /// <param name="variationWithoutLogEntries"></param>
        void Delete(Variation variationWithoutLogEntries);

        /// <summary>
        ///     Speichert die Variation
        /// </summary>
        /// <param name="variation"></param>
        void Save(Variation variation);
    }
}