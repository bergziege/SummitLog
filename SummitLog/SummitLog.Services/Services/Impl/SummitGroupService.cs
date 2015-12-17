using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Gipfelgruppen
    /// </summary>
    public class SummitGroupService : ISummitGroupService
    {
        private readonly ISummitGroupDao _summitGroupDao;

        /// <summary>
        ///     Erstellt eine neue Instanz des Service
        /// </summary>
        /// <param name="summitGroupDao"></param>
        public SummitGroupService(ISummitGroupDao summitGroupDao)
        {
            _summitGroupDao = summitGroupDao;
        }

        /// <summary>
        ///     Liefert alle Gipfelgruppen innerhalb eines Gebiets
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public IList<SummitGroup> GetAllIn(Area area)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            return _summitGroupDao.GetAllIn(area).OrderBy(x=>x.Name).ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Gipfelgruppe innerhalb eines Gebiets mit dem übergebenen Namen.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="name"></param>
        public void Create(Area area, string name)
        {
            if (area == null) throw new ArgumentNullException(nameof(area));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            _summitGroupDao.Create(area, new SummitGroup {Name = name});
        }

        /// <summary>
        ///     Liefert ob die Gipfelgruppe in Verwendung ist
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        public bool IsInUse(SummitGroup summitGroup)
        {
            return _summitGroupDao.IsInUse(summitGroup);
        }

        /// <summary>
        ///     Löscht eine Gipfelgruppe wenn diese nicht mehr verwendet wird
        /// </summary>
        /// <param name="summitGroup"></param>
        public void Delete(SummitGroup summitGroup)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            if (_summitGroupDao.IsInUse(summitGroup))
            {
                throw new NodeInUseException();
            }
            _summitGroupDao.Delete(summitGroup);
        }

        /// <summary>
        ///     Speichert die Gipfelgruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        public void Save(SummitGroup summitGroup)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            _summitGroupDao.Save(summitGroup);
        }
    }
}