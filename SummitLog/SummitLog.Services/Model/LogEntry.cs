using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Logeintrag
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        ///     Liefert oder setzt die ID
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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