using DryIoc;

namespace SummitLog
{
    /// <summary>
    ///     App Context
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        ///     Liefert oder setzt den Container
        /// </summary>
        public static Container Container { get; set; }
    }
}