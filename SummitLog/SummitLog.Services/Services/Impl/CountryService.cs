using System;
using System.Collections.Generic;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Länder
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly ICountryDao _countryDao;

        /// <summary>
        ///     Erstellt eine neue Instanz des Country Services
        /// </summary>
        /// <param name="countryDao"></param>
        public CountryService(ICountryDao countryDao)
        {
            _countryDao = countryDao;
        }

        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            return _countryDao.GetAll();
        }

        /// <summary>
        ///     Erstellt ein neues Land mit dem übergebenen Namen
        /// </summary>
        /// <param name="countryName"></param>
        public void Create(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentNullException(nameof(countryName));
            _countryDao.Create(new Country {Name = countryName});
        }
    }
}