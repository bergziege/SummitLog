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
    }
}