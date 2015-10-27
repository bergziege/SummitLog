using System;
using System.Collections.Generic;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Logeinträge
    /// </summary>
    public class LogEntryService : ILogEntryService
    {
        private readonly ILogEntryDao _logEntryDao;

        /// <summary>
        ///     Liefert eine neue Instanz des Services
        /// </summary>
        /// <param name="logEntryDao"></param>
        public LogEntryService(ILogEntryDao logEntryDao)
        {
            _logEntryDao = logEntryDao;
        }

        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        public IList<LogEntry> GetAllIn(Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            return _logEntryDao.GetAllIn(variation);
        }

        /// <summary>
        ///     Erstellt ein neuen Logeintrag zur Variation
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="date"></param>
        /// <param name="variation"></param>
        public void Create(string memo, DateTime date, Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            if (string.IsNullOrWhiteSpace(memo)) throw new ArgumentNullException(nameof(memo));
            _logEntryDao.Create(variation, new LogEntry() {Memo = memo, DateTime = date});
        }
    }
}