using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Schwierigkeitsgradskalen
    /// </summary>
    public interface IDifficultyLevelScaleService
    {
        /// <summary>
        ///     Liefert alle Schwierigkeitsgradskalen
        /// </summary>
        /// <returns></returns>
        IList<DifficultyLevelScale> GetAll();

        /// <summary>
        ///     Erstellt eine neue Schwierigkeitsgradskala mit dem übergebenen Namen
        /// </summary>
        /// <param name="scaleName"></param>
        void Create(string scaleName);

        /// <summary>
        ///     Liefert ob eine Skale verwendet wird
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        /// <returns></returns>
        bool IsInUse(DifficultyLevelScale difficultyLevelScale);

        /// <summary>
        ///     Löscht die Skale, wenn nicht mehr verwendet
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        void Delete(DifficultyLevelScale difficultyLevelScale);
    }
}