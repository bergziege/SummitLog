using System.ComponentModel;
using Microsoft.Practices.Unity;
using SummitLog.UI.DbSettings;
using SummitLog.UI.DbSettings.ViewCommands;
using SummitLog.UI.DbSettings.ViewModels;
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
using SummitLog.UI.RouteOnSummitEdit;
using SummitLog.UI.RouteOnSummitEdit.ViewCommands;
using SummitLog.UI.RouteOnSummitEdit.ViewModels;
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
        public static IUnityContainer Init(IUnityContainer container)
        {
            SetupViews(container);
            SetupViewModels(container);
            SetupViewCommands(container);
            return container;
        }

        private static void SetupViewCommands(IUnityContainer container)
        {
            container.RegisterType<NameInputViewCommand>();
            container.RegisterType<NameAndScoreInputViewCommand>();
            container.RegisterType<DifficultyManagementViewCommand>();
            container.RegisterType<NameAndLevelInputViewCommand>();
            container.RegisterType<LogEntryInputViewCommand>();
            container.RegisterType<SummitEditViewCommand>();
            container.RegisterType<RouteOnSummitEditViewCommand>();
            container.RegisterType<DbSettingsViewCommand>();
        }

        private static void SetupViewModels(IUnityContainer container)
        {
            container.RegisterType<IMainViewModel, MainViewModel>();
            container.RegisterType<INameInputViewModel, NameInputViewModel>();
            container.RegisterType<IDifficultyLevelScaleManagementViewModel, DifficultyLevelScaleManagementViewModel>();
            container.RegisterType<IDifficultyLevelManagementViewModel, DifficultyLevelManagementViewModel>();
            container.RegisterType<INameAndScoreInputViewModel, NameAndScoreInputViewModel>();
            container.RegisterType<IDifficultyManagementViewModel, DifficultyManagementViewModel>();
            container.RegisterType<INameAndLevelInputViewModel, NameAndLevelInputViewModel>();
            container.RegisterType<ILogEntryInputViewModel, LogEntryInputViewModel>();
            container.RegisterType<ILogItemViewModel, LogItemViewModel>();
            container.RegisterType<IVariationItemViewModel, VariationItemViewModel>();
            container.RegisterType<ISummitEditViewModel, SummitEditViewModel>();
            container.RegisterType<IRouteOnSummitEditViewModel, RouteOnSummitEditViewModel>();
            container.RegisterType<IDbSettingsViewModel, DbSettingsViewModel>();
        }

        private static void SetupViews(IUnityContainer container)
        {
            container.RegisterType<MainView>();
            container.RegisterType<NameInputView>();
            container.RegisterType<NameAndScoreInputView>();
            container.RegisterType<DifficultyManagementView>();
            container.RegisterType<NameAndLevelInputView>();
            container.RegisterType<LogEntryInputView>();
            container.RegisterType<SummitEditView>();
            container.RegisterType<RouteOnSummitEditView>();
            container.RegisterType<DbSettingsView>();
        }
    }
}