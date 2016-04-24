using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.DbSettings.ViewCommands
{
    /// <summary>
    ///     View Command um den Datenbankeinstellungsdialog anzuzeigen
    /// </summary>
    public class DbSettingsViewCommand : ViewCommandBase, IDbSettingsViewCommand
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DbSettingsViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper)
            : base(genericFactory, windowParentHelper)
        {
        }

        /// <summary>
        ///     Verbindet die View mit dem View Model und zeigt diese an.
        /// </summary>
        public void Execute()
        {
            IDbSettingsView view = GenericFactory.Resolve<IDbSettingsView>();
            IDbSettingsViewModel vm = GenericFactory.Resolve<IDbSettingsViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            vm.RequestClose += delegate { view.Close(); };

            WindowParentHelper.SetOwner<MainView>(view);
            view.ShowDialog();
        }
    }
}