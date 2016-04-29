using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using SummitLog.Services;
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
        public bool Execute()
        {
            bool dialogResult = false;
            IDbSettingsView view = GenericFactory.Resolve<IDbSettingsView>();
            IDbSettingsViewModel vm = GenericFactory.Resolve<IDbSettingsViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            vm.RequestCloseOnSave += delegate
            {
                dialogResult = true;
                view.Close();
            };

            vm.RequestCloseOnCancel += delegate
            {
                dialogResult = false;
                view.Close();
            };

            WindowParentHelper.SetOwner<MainView>(view);
            view.ShowDialog();
            return dialogResult;
        }
    }
}