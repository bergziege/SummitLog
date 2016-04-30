using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement.DesignViewModels
{
    /// <summary>
    ///     Design View Model einer Schwierigkeitgradskala
    /// </summary>
    public class DifficultyLevelScaleItemDesignViewModel : ItemWithNameDesignViewModel<DifficultyLevelScale>,
        IDifficultyLevelScaleItemViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DifficultyLevelScaleItemDesignViewModel()
        {
            IsDefault = true;
        }

        /// <summary>
        ///     Liefert ob es sich um eine Standardskala handelt.
        /// </summary>
        public bool IsDefault { get; }
    }
}