using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelManagement.DesignViewModels
{
    /// <summary>
    /// Design View Model eines Items mit Namen und Punktezahl
    /// </summary>
    public class ItemWIthNameAndScoreDesignViewModel: ItemWithNameDesignViewModel<DifficultyLevel>, IItemWithNameAndScoreViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ItemWIthNameAndScoreDesignViewModel()
        {
            Score = 100;
        }

        /// <summary>
        ///     Liefert die Punktezahl
        /// </summary>
        public int Score { get; }
    }
}