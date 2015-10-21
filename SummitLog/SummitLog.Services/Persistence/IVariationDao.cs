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
        void Create(Variation variation, Route route, DifficultyLevel difficultyLevel);
    }
}