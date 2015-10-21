using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Ein einzelner Gipfel
    /// </summary>
    public class Summit
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}