using System;
using ReactiveUI;
using SummitLog.Services.Model;

namespace SummitLog.UI.Main.ViewModels
{
    public class LogItemViewModel:ReactiveObject, ILogItemViewModel
    {
        private string _memo;
        private DateTimeOffset _date;
        private LogEntry _logEntry;

        /// <summary>
        ///     Liefert das Memo
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            private set { this.RaiseAndSetIfChanged(ref _memo, value);}
        }

        /// <summary>
        ///     Liefert das Datum
        /// </summary>
        public DateTimeOffset Date
        {
            get { return _date; }
            private set { this.RaiseAndSetIfChanged(ref _date, value); }
        }

        /// <summary>
        ///     Liefert den Logeintrag
        /// </summary>
        public LogEntry LogEntry
        {
            get { return _logEntry; }
        }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public ILogItemViewModel LoadData(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));
            _logEntry = logEntry;
            Memo = _logEntry.Memo;
            Date = _logEntry.DateTime;

            return this;
        }

        /// <summary>
        ///     Aktualisiert die Daten des Logeintrages
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="dateTime"></param>
        public void Update(string memo, DateTime dateTime)
        {
            Memo = _logEntry.Memo = memo;
            Date = _logEntry.DateTime = dateTime;
        }
    }
}