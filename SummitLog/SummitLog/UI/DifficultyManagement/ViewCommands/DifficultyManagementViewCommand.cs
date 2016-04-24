using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.DifficultyManagement.ViewCommands
{
    public class DifficultyManagementViewCommand : ViewCommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DifficultyManagementViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper)
            : base(genericFactory, windowParentHelper)
        {
        }

        public void Execute()
        {
            DifficultyManagementView view = GenericFactory.Resolve<DifficultyManagementView>();
            IDifficultyManagementViewModel vm = GenericFactory.Resolve<IDifficultyManagementViewModel>();
            view.DataContext = vm;
            vm.LoadData();

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }
    }
}