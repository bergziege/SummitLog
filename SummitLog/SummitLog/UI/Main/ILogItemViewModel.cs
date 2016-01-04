using System;
using SummitLog.Services.Model;

namespace SummitLog.UI.Main
{
    /// <summary>
    ///     Schnittstelle für View Models eines Logeintrags
    /// </summary>
    public interface ILogItemViewModel
    {
        /// <summary>
        ///     Liefert das Memo
        /// </summary>
        string Memo { get; }

        /// <summary>
        ///     Liefert das Datum
        /// </summary>
        DateTimeOffset Date { get; }

        /// <summary>
        ///     Liefert den Logeintrag
        /// </summary>
        LogEntry LogEntry { get; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        ILogItemViewModel LoadData(LogEntry logEntry);

        /// <summary>
        ///     Aktualisiert die Daten des Logeintrages
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="dateTime"></param>
        void Update(string memo, DateTime dateTime);
    }
}