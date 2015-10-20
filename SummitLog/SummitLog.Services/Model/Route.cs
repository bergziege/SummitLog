namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Ein Weg in einem Land, einer Gegend, ...
    /// </summary>
    public class Route
    {
        /// <summary>
        ///     Liefert die ID des Weges
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Liefert den Namen des Weges
        /// </summary>
        public string Name { get; set; }
    }
}