using System;
using System.Collections.Generic;
using System.Linq;
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
            return _logEntryDao.GetAllIn(variation).OrderByDescending(x=>x.DateTime).ToList();
        }

        /// <summary>
        ///     Erstellt ein neuen Logeintrag zur Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="date"></param>
        /// <param name="memo"></param>
        public LogEntry Create(Variation variation, DateTime date, string memo)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            return _logEntryDao.Create(variation, new LogEntry() {Memo = memo, DateTime = date});
        }

        /// <summary>
        /// Löscht den übergebenen Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        public void Delete(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));
            
            _logEntryDao.Delete(logEntry);
        }

        /// <summary>
        ///     Speichert einen Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        public void Save(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));
            _logEntryDao.Save(logEntry);
        }
    }
}