using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.Common;
using SummitLog.UI.DbSettings.ViewModels;
using SummitLog.UI.Main;

namespace SummitLog.UI.DbSettings.ViewCommands
{
    public class DbSettingsViewCommand: ViewCommandBase
    {

        public void Execute()
        {
            IWindow view = GenericFactory.ResolveAsIWindow<DbSettingsView>();
            IDbSettingsViewModel vm = GenericFactory.Resolve<IDbSettingsViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            vm.RequestClose += delegate { view.Close(); };

            WindowParentHelper.SetOwner<MainView>(view);
            view.ShowDialog();
        }
    }
}