using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Gebiete
    /// </summary>
    public class AreaService : IAreaService
    {
        /// <summary>
        /// Setzt einen <see cref="IAreaDao"/>
        /// </summary>
        [Dependency]
        public IAreaDao AreaDao
        {
            private get; set;
        }
        
        /// <summary>
        ///     Liefert alle Gebiete innerhalb eines Landes
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IList<Area> GetAllIn(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            return AreaDao.GetAllIn(country).OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        ///     Erstellt ein neues Gebiet innerhalb eines Landes mit dem übergebenen Namen.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="name"></param>
        public Area Create(Country country, string name)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            return AreaDao.Create(country, new Area { Name = name });
        }

        /// <summary>
        ///     Liefert ob ein Gebiet noch verwendet wird
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool IsInUse(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            return AreaDao.IsInUse(area);
        }

        /// <summary>
        ///     Löscht ein Gebiet, wenn es nicht mehr verwendet wird
        /// </summary>
        /// <param name="area"></param>
        public void Delete(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            if (AreaDao.IsInUse(area))
            {
                throw new NodeInUseException();
            }

            AreaDao.Delete(area);
        }

        /// <summary>
        ///     Speichert das Gebiet
        /// </summary>
        /// <param name="area"></param>
        public void Save(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            AreaDao.Save(area);
        }
    }
}