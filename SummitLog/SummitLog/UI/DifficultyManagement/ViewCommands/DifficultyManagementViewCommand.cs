using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.DifficultyManagement.ViewCommands
{
    public class DifficultyManagementViewCommand: ViewCommandBase
    {
        public void Execute()
        {
            DifficultyManagementView view = GenericFactory.Resolve<DifficultyManagementView>();
            IDifficultyManagementViewModel vm = GenericFactory.Resolve<IDifficultyManagementViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof (MainView));

            view.ShowDialog();
        }
    }
}