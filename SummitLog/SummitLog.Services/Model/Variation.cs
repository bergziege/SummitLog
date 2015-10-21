using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    /// Variation eines Weges. Bzw. Verbindung von Weg und Schwierigkeitsgrad
    /// </summary>
    public class Variation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}