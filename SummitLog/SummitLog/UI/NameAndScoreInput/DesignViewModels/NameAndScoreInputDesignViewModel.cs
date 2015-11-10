using SummitLog.UI.NameInput.DesignViewModels;

namespace SummitLog.UI.NameAndScoreInput.DesignViewModels
{
    /// <summary>
    /// Design View Model einer Ansicht zur Eingabe eines Namens und einer Punktezahl
    /// </summary>
    public class NameAndScoreInputDesignViewModel : NameInputDesignViewModel, INameAndScoreInputViewModel
    {
        /// <summary>
        /// Liefert eine neue Instanz des Design View Models
        /// </summary>
        public NameAndScoreInputDesignViewModel()
        {
            Score = 42;
        }

        /// <summary>
        /// Liefert oder setzt die Punktezahl
        /// </summary>
        public int Score { get; set; }
    }
}