using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Gebiete
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        ///     Liefert alle Gebiete innerhalb eines Landes
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        IList<Area> GetAllIn(Country country);

        /// <summary>
        ///     Erstellt ein neues Gebiet innerhalb eines Landes mit dem übergebenen Namen.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="name"></param>
        void Create(Country country, string name);

        /// <summary>
        ///     Liefert ob ein Gebiet noch verwendet wird
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        bool IsInUse(Area area);

        /// <summary>
        ///     Löscht ein Gebiet, wenn es nicht mehr verwendet wird
        /// </summary>
        /// <param name="area"></param>
        void Delete(Area area);
    }
}