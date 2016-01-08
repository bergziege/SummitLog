using DryIoc;
using SummitLog.UI.DifficultyLevelManagement;
using SummitLog.UI.DifficultyLevelManagement.ViewModels;
using SummitLog.UI.DifficultyLevelScaleManagement;
using SummitLog.UI.DifficultyLevelScaleManagement.ViewModels;
using SummitLog.UI.DifficultyManagement;
using SummitLog.UI.DifficultyManagement.ViewCommands;
using SummitLog.UI.DifficultyManagement.ViewModels;
using SummitLog.UI.LogEntryInput;
using SummitLog.UI.LogEntryInput.ViewCommands;
using SummitLog.UI.LogEntryInput.ViewModels;
using SummitLog.UI.Main;
using SummitLog.UI.Main.ViewModels;
using SummitLog.UI.NameAndLevelInput;
using SummitLog.UI.NameAndLevelInput.ViewCommands;
using SummitLog.UI.NameAndLevelInput.ViewModels;
using SummitLog.UI.NameAndScoreInput;
using SummitLog.UI.NameAndScoreInput.ViewCommands;
using SummitLog.UI.NameAndScoreInput.ViewModels;
using SummitLog.UI.NameInput;
using SummitLog.UI.NameInput.ViewModels;
using SummitLog.UI.SummitEdit;
using SummitLog.UI.SummitEdit.ViewCommands;
using SummitLog.UI.SummitEdit.ViewModels;

namespace SummitLog
{
    /// <summary>
    ///     Bootloader für UI Relevante Klassen
    /// </summary>
    public static class UiBootloader
    {
        /// <summary>
        ///     Initialisiert den UI Bootloader
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Container Init(Container container)
        {
            SetupViews(container);
            SetupViewModels(container);
            SetupViewCommands(container);
            return container;
        }

        private static void SetupViewCommands(Container container)
        {
            container.Register<NameInputViewCommand>();
            container.Register<NameAndScoreInputViewCommand>();
            container.Register<DifficultyManagementViewCommand>();
            container.Register<NameAndLevelInputViewCommand>();
            container.Register<LogEntryInputViewCommand>();
            container.Register<SummitEditViewCommand>();
        }

        private static void SetupViewModels(Container container)
        {
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<INameInputViewModel, NameInputViewModel>();
            container.Register<IDifficultyLevelScaleManagementViewModel, DifficultyLevelScaleManagementViewModel>();
            container.Register<IDifficultyLevelManagementViewModel, DifficultyLevelManagementViewModel>();
            container.Register<INameAndScoreInputViewModel, NameAndScoreInputViewModel>();
            container.Register<IDifficultyManagementViewModel, DifficultyManagementViewModel>();
            container.Register<INameAndLevelInputViewModel, NameAndLevelInputViewModel>();
            container.Register<ILogEntryInputViewModel, LogEntryInputViewModel>();
            container.Register<ILogItemViewModel, LogItemViewModel>();
            container.Register<IVariationItemViewModel, VariationItemViewModel>();
            container.Register<ISummitEditViewModel, SummitEditViewModel>();
        }

        private static void SetupViews(Container container)
        {
            container.Register<MainView>();
            container.Register<NameInputView>();
            container.Register<NameAndScoreInputView>();
            container.Register<DifficultyManagementView>();
            container.Register<NameAndLevelInputView>();
            container.Register<LogEntryInputView>();
            container.Register<SummitEditView>();
        }
    }
}