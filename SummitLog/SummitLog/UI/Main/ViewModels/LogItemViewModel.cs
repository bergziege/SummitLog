using System;
using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;

namespace SummitLog.UI.Main.ViewModels
{
    public class LogItemViewModel : ReactiveObject, ILogItemViewModel
    {
        private DateTimeOffset _date;
        private string _memo;

        /// <summary>
        ///     Liefert das Memo
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            private set { this.RaiseAndSetIfChanged(ref _memo, value); }
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
        public LogEntry LogEntry { get; private set; }

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public ILogItemViewModel LoadData(LogEntry logEntry)
        {
            if (logEntry == null) throw new ArgumentNullException(nameof(logEntry));
            LogEntry = logEntry;
            Memo = LogEntry.Memo;
            Date = LogEntry.DateTime;

            return this;
        }

        /// <summary>
        ///     Aktualisiert die Daten des Logeintrages
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="dateTime"></param>
        public void Update(string memo, DateTime dateTime)
        {
            Memo = LogEntry.Memo = memo;
            Date = LogEntry.DateTime = dateTime;
        }
    }
}