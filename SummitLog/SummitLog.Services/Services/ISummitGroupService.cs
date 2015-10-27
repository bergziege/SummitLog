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
    }
}