using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}