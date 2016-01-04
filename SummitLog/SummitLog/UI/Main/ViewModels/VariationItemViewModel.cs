using ReactiveUI;
using SummitLog.Services.Model;
using SummitLog.Services.Services;
using SummitLog.UI.Common;

namespace SummitLog.UI.Main.ViewModels
{
    /// <summary>
    ///     View Model einer Variation
    /// </summary>
    public class VariationItemViewModel : ItemWithNameViewModel<Variation>, IVariationItemViewModel
    {
        private readonly IDifficultyLevelService _difficultyLevelService;
        private DifficultyLevel _difficultyLevel;

        /// <summary>
        ///     Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="difficultyLevelService"></param>
        public VariationItemViewModel(IDifficultyLevelService difficultyLevelService)
        {
            _difficultyLevelService = difficultyLevelService;
        }

        /// <summary>
        ///     Liefert den Schwierigkeitsgrad der Variation
        /// </summary>
        public DifficultyLevel DifficultyLevel
        {
            get { return _difficultyLevel; }
            private set { this.RaiseAndSetIfChanged(ref _difficultyLevel, value); }
        }

        /// <summary>
        ///     LÄdt die VM relevanten Daten
        /// </summary>
        /// <param name="item"></param>
        public override void LoadData(Variation item)
        {
            base.LoadData(item);
            DifficultyLevel = _difficultyLevelService.GetForVariation(item);
        }

        /// <summary>
        ///     Aktualisiert das View Model
        /// </summary>
        public override void DoUpdate()
        {
            base.DoUpdate();
            DifficultyLevel = _difficultyLevelService.GetForVariation(Item);
        }
    }
}