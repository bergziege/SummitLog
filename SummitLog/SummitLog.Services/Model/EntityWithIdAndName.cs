namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Entität mit einer Id und einem Namen
    /// </summary>
    public class EntityWithIdAndName : EntityWithId
    {
        /// <summary>
        ///     Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }
    }
}