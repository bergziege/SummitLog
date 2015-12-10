using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
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
            return _countryDao.GetAll().OrderBy(x=>x.Name).ToList();
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

        /// <summary>
        ///     Liefert ob ein Land verwendet wird
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool IsInUse(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            return _countryDao.IsInUse(country);
        }

        /// <summary>
        ///     Löscht ein Land wenn es nicht mehr in Verwendung ist
        /// </summary>
        /// <param name="country"></param>
        public void Delete(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (_countryDao.IsInUse(country))
            {
                throw new NodeInUseException();
            }
            _countryDao.Delete(country);
        }

        /// <summary>
        ///     Speichert das Land
        /// </summary>
        /// <param name="country"></param>
        public void Save(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            _countryDao.Save(country);
        }
    }
}