using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Länder
    /// </summary>
    public interface ICountryDao
    {
        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        IList<Country> GetAll();

        /// <summary>
        ///     Erstellt ein neues Land
        /// </summary>
        /// <param name="country"></param>
        Country Create(Country country);

        /// <summary>
        ///     Liefert ob ein Land noch verwendet wird
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        bool IsInUse(Country country);

        /// <summary>
        ///     Löscht ein Land, wenn dies nicht mehr verwendet wird
        /// </summary>
        /// <param name="country"></param>
        void Delete(Country country);

        /// <summary>
        ///     Speichert das Land
        /// </summary>
        /// <param name="country"></param>
        void Save(Country country);
    }
}