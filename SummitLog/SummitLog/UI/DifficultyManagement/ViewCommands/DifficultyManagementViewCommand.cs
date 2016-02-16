using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.Main;

namespace SummitLog.UI.DifficultyManagement.ViewCommands
{
    public class DifficultyManagementViewCommand
    {
        public void Execute()
        {
            DifficultyManagementView view = AppContext.Container.Resolve<DifficultyManagementView>();
            IDifficultyManagementViewModel vm = AppContext.Container.Resolve<IDifficultyManagementViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof (MainView));

            view.ShowDialog();
        }
    }
}