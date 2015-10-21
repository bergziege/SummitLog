﻿using System;

namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Eine Gipfelgruppe
    /// </summary>
    public class SummitGroup
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}