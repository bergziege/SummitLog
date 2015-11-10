using DryIoc;

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
            view.ShowDialog();
        }
    }
}