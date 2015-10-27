namespace SummitLog.Services.Model
{
    /// <summary>
    ///     Schwierigkeitsgrad
    /// </summary>
    public class DifficultyLevel : EntityWithIdAndName
    {
        /// <summary>
        ///     Liefert oder setzt die Punktezahl, mit der dieser Schwierigkeitsgrad mit anderen Graden anderer Skalen verglichen
        ///     werden kann
        /// </summary>
        public int Score { get; set; }
    }
}