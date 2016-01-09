namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Ein einzelner Gipfel
    /// </summary>
    public class Summit : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert oder setzt die Gipfelnummer
        /// </summary>
        public string SummitNumber { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Bewertung
        /// </summary>
        public double Rating { get; set; }
    }
}