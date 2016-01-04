using System;
using SummitLog.Services.Model;

namespace SummitLog.UI.Main.DesignViewModels
{
    /// <summary>
    ///     Design View Model eines Logeintrages
    /// </summary>
    public class LogItemDesignViewModel : ILogItemViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public LogItemDesignViewModel()
        {
            Memo = "Memo";
            Date = DateTime.Now;
        }

        /// <summary>
        ///     Liefert das Memo
        /// </summary>
        public string Memo { get; }

        /// <summary>
        ///     Liefert das Datum
        /// </summary>
        public DateTimeOffset Date { get; }

        /// <summary>
        ///     Liefert den Logeintrag
        /// </summary>
        public LogEntry LogEntry { get; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public ILogItemViewModel LoadData(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Aktualisiert die Daten des Logeintrages
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="dateTime"></param>
        public void Update(string memo, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}