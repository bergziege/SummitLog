using System;

namespace SummitLog.Services.Model
{
    public class Area
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}