using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Schwierigkeitsgradskala
    /// </summary>
    public class DifficultyLevelScale
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}