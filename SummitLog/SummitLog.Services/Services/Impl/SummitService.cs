using System;
using System.Collections.Generic;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Gipfel
    /// </summary>
    public class SummitService : ISummitService
    {
        private readonly ISummitDao _summitDao;

        /// <summary>
        ///     Erstellt eine neue Instanz des Service
        /// </summary>
        /// <param name="summitDao"></param>
        public SummitService(ISummitDao summitDao)
        {
            _summitDao = summitDao;
        }

        /// <summary>
        ///     Liefert alle Gipfel innerhalb einer Gruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        public IList<Summit> GetAllIn(SummitGroup summitGroup)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            return _summitDao.GetAllIn(summitGroup);
        }

        /// <summary>
        ///     Erstellt einen neuen Gipfel innerhalb einer Gruppe mit dem übergebenen Namen.
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="name"></param>
        public void Create(SummitGroup summitGroup, string name)
        {
            if (summitGroup == null) throw new ArgumentNullException(nameof(summitGroup));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            _summitDao.Create(summitGroup, new Summit {Name = name});
        }
    }
}