using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Entität mit einer ID
    /// </summary>
    public class EntityWithId
    {
        /// <summary>
        ///     Liefert oder setzt die Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}