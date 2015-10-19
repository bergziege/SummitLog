using System.Collections.Generic;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Model des Dateiformats mit allen SummitLog Daten
    /// </summary>
    public class SummitLogFileMode
    {
        /// <summary>
        ///     Liefert oder setzt die Gesteinstypen
        /// </summary>
        public IList<StoneType> StoneTypes { get; set; }
    }
}