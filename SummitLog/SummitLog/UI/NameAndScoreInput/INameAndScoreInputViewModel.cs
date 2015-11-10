using SummitLog.UI.NameInput;

namespace SummitLog.UI.NameAndScoreInput
{
    /// <summary>
    /// Schnittstelle für View Models einer Ansicht zur Eingabe von Namen und einer Zahl
    /// </summary>
    public interface INameAndScoreInputViewModel : INameInputViewModel
    {
        /// <summary>
        /// Liefert oder setzt die Punktezahl
        /// </summary>
        int Score { get; set; }
    }
}