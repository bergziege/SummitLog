using System;
using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Logeinträge
    /// </summary>
    public interface ILogEntryService
    {
        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        IList<LogEntry> GetAllIn(Variation variation);

        /// <summary>
        ///     Erstellt ein neuen Logeintrag zur Variation
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="date"></param>
        /// <param name="variation"></param>
        void Create(string memo, DateTime date, Variation variation);

        /// <summary>
        /// Löscht den übergebenen Logeintrag
        /// </summary>
        /// <param name="logEntry"></param>
        void Delete(LogEntry logEntry);
    }
}