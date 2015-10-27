using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Gipfel in Gipfelgruppen
    /// </summary>
    public interface ISummitDao
    {
        /// <summary>
        ///     Liefert alle Gipfel einer Gipfelgruppe
        /// </summary>
        /// <returns></returns>
        IList<Summit> GetAllIn(SummitGroup summitGroup);

        /// <summary>
        ///     Erstellt einen neuen Gipfel in einer Gipfelgruppe
        /// </summary>
        void Create(SummitGroup summitGroup, Summit summit);
    }
}