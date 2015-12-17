using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Gipfelgruppen
    /// </summary>
    public interface ISummitGroupService
    {
        /// <summary>
        ///     Liefert alle Gipfelgruppen innerhalb eines Gebiets
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        IList<SummitGroup> GetAllIn(Area area);

        /// <summary>
        ///     Erstellt eine neue Gipfelgruppe innerhalb eines Gebiets mit dem übergebenen Namen.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="name"></param>
        void Create(Area area, string name);

        /// <summary>
        ///     Liefert ob die Gipfelgruppe in Verwendung ist
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        bool IsInUse(SummitGroup summitGroup);

        /// <summary>
        ///     Löscht eine Gipfelgruppe wenn diese nicht mehr verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        void Delete(SummitGroup summitGroup);

        /// <summary>
        ///     Speichert die Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        void Save(SummitGroup summitGroup);
    }
}