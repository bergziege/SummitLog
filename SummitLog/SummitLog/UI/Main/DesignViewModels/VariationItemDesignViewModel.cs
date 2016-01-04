using SummitLog.Services.Model;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.DesignViewModels
{
    /// <summary>
    ///     Design View Model eines Variation Items
    /// </summary>
    public class VariationItemDesignViewModel : ItemWithNameDesignViewModel<Variation>, IVariationItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VariationItemDesignViewModel()
        {
            DifficultyLevel = new DifficultyLevel() {Name = "7b", Score = 100};
        }

        /// <summary>
        ///     Liefert den Schwierigkeitsgrad der Variation
        /// </summary>
        public DifficultyLevel DifficultyLevel { get; }
    }
}