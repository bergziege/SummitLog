using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Gegenden in Ländern
    /// </summary>
    public interface IAreaDao
    {
        /// <summary>
        ///     Liefert alle Gegenden in einem Land
        /// </summary>
        /// <returns></returns>
        IList<Area> GetAllIn(Country country);

        /// <summary>
        ///     Erstellt eine neue Gegend in einem Land
        /// </summary>
        /// <param name="country"></param>
        /// <param name="area"></param>
        Area Create(Country country, Area area);

        /// <summary>
        ///     Liefert ob das Gebiet verwendet wird.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        bool IsInUse(Area area);

        /// <summary>
        ///     Löscht ein Gebiet, wenn dies nicht mehr in Verwendung ist.
        /// </summary>
        /// <param name="area"></param>
        void Delete(Area area);

        /// <summary>
        ///     Speichert das Gebiet
        /// </summary>
        /// <param name="area"></param>
        void Save(Area area);
    }
}