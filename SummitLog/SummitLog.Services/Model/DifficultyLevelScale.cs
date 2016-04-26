namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Schwierigkeitsgradskala
    /// </summary>
    public class DifficultyLevelScale : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert ob es sich um die Standardskala handelt.
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        ///     Setzt die Skala als Standard
        /// </summary>
        public void SetAsDefault()
        {
            IsDefault = true;
        }

        /// <summary>
        /// Setzt die Skala nicht mehr als Standard
        /// </summary>
        public void RemoveDefaultState()
        {
            IsDefault = false;
        }
    }
}