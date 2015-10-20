using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Ein Weg in einem Land, einer Gegend, ...
    /// </summary>
    public class Route
    {
        private Guid _id = Guid.NewGuid();

        /// <summary>
        ///     Liefert die ID des Weges
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        ///     Liefert den Namen des Weges
        /// </summary>
        public string Name { get; set; }
    }
}