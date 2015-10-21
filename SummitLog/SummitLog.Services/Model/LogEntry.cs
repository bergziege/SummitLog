using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Logeintrag
    /// </summary>
    public class LogEntry : EntityWithId
    {
        /// <summary>
        ///     Liefert oder setzt das Datum
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     LIefert oder setzt den Text
        /// </summary>
        public string Memo { get; set; }
    }
}