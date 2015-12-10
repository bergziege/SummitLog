using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Länder
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        IList<Country> GetAll();

        /// <summary>
        ///     Erstellt ein neues Land mit dem übergebenen Namen
        /// </summary>
        /// <param name="countryName"></param>
        void Create(string countryName);

        /// <summary>
        ///     Liefert ob ein Land verwendet wird
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        bool IsInUse(Country country);

        /// <summary>
        ///     Löscht ein Land wenn es nicht mehr in Verwendung ist
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