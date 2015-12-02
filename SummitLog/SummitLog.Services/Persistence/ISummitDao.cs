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
        Summit Create(SummitGroup summitGroup, Summit summit);

        /// <summary>
        ///     Liefert ob der Gipfel verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        bool IsInUse(Summit summit);

        /// <summary>
        ///     Löscht einen Gipfel wenn diese rnicht mehr verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        void Delete(Summit summit);

        /// <summary>
        ///     Speichert denm Gipfel
        /// </summary>
        /// <param name="summit"></param>
        void Save(Summit summit);
    }
}