using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Gegenden in Ländern
    /// </summary>
    public interface ISummitGroupDao
    {
        /// <summary>
        ///     Liefert alle Gipfelgruppen in einer Gegend
        /// </summary>
        /// <returns></returns>
        IList<SummitGroup> GetAllIn(Area area);

        /// <summary>
        ///     Erstellt eine neue Gipfelgruppe in einer Gegend
        /// </summary>
        SummitGroup Create(Area area, SummitGroup summitGroup);

        /// <summary>
        ///     Liefert ob eine Gipfelgruppe noch verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        bool IsInUse(SummitGroup summitGroup);

        /// <summary>
        ///     Löscht eine Gipfelgruppen wenn diese nicht mehr verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        void Delete(SummitGroup summitGroup);
    }
}