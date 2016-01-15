namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Ein Weg in einem Land, einer Gegend, ...
    /// </summary>
    public class Route : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert oder setzt die Bewertung einer Route
        /// </summary>
        public double Rating { get; set; }
    }
}