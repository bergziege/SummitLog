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
    }
}