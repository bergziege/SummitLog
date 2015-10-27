using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für DAOs für Logeinträge
    /// </summary>
    public interface ILogEntryDao
    {
        /// <summary>
        ///     Liefert alle Logeinträge einer Variation
        /// </summary>
        /// <returns></returns>
        IList<LogEntry> GetAllIn(Variation variation);

        /// <summary>
        ///     Erstellt einen neuen Logeintrag einer Variation
        /// </summary>
        void Create(Variation variation, LogEntry logEntry);
    }
}