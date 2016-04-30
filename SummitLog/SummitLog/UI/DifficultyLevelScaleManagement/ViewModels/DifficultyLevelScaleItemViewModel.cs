using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelScaleManagement.ViewModels
{
    /// <summary>
    ///     View Model einer einzelnen Schwierigkeitsgradskala
    /// </summary>
    public class DifficultyLevelScaleItemViewModel : ItemWithNameViewModel<DifficultyLevelScale>,
        IDifficultyLevelScaleItemViewModel
    {
        /// <summary>
        ///     Liefert ob es sich um eine Standardskala handelt.
        /// </summary>
        public bool IsDefault
        {
            get { return Item.IsDefault; }
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public override void DoUpdate()
        {
            base.DoUpdate();
            this.RaisePropertyChanged(nameof(IsDefault));
        }
    }
}