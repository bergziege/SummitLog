using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelManagement
{
    /// <summary>
    ///     Schnittstelle für View Models eines Items mit Namen und Punktezahl
    /// </summary>
    public interface IItemWithNameAndScoreViewModel : IItemWithNameViewModel<DifficultyLevel>
    {
        /// <summary>
        ///     Liefert die Punktezahl
        /// </summary>
        int Score { get; }
    }
}