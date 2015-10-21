using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Schwierigkeitsgrad
    /// </summary>
    public class DifficultyLevel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int Score { get; set; }
    }
}