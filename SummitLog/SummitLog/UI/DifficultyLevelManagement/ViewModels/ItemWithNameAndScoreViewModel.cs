using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.DifficultyLevelManagement.ViewModels
{
    /// <summary>
    /// View Model eines Items mit Namen und Punktezahl
    /// </summary>
    public class ItemWithNameAndScoreViewModel:ItemWithNameViewModel<DifficultyLevel>, IItemWithNameAndScoreViewModel
    {
        private int _score;

        /// <summary>
        ///     Liefert die Punktezahl
        /// </summary>
        public int Score
        {
            get { return _score; }
            private set { this.RaiseAndSetIfChanged(ref _score, value); }
        }

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public void LoadData(DifficultyLevel item)
        {
            base.LoadData(item);
            Score = item.Score;
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public void DoUpdate()
        {
            base.DoUpdate();
            Score = Item.Score;

        }
    }
}