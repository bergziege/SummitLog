using ReactiveUI;
using SummitLog.UI.NameInput.ViewModels;

namespace SummitLog.UI.NameAndScoreInput.ViewModels
{
    /// <summary>
    /// View Model einer Ansicht zur Eingabe eines Namens und Punktezahl
    /// </summary>
    public class NameAndScoreInputViewModel : NameInputViewModel, INameAndScoreInputViewModel
    {
        private int _score;

        /// <summary>
        /// Liefert oder setzt die Punktezahl
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { this.RaiseAndSetIfChanged(ref _score, value); }
        }
    }
}