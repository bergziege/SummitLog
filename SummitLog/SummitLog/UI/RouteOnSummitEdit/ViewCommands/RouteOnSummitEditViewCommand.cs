using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.Services.Model;
using SummitLog.UI.Main;
using SummitLog.UI.SummitEdit;

namespace SummitLog.UI.RouteOnSummitEdit.ViewCommands
{
    public class RouteOnSummitEditViewCommand
    {
        
        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public Route Execute(Route routeToEdit)
        {
            RouteOnSummitEditView view = AppContext.Container.Resolve<RouteOnSummitEditView>();
            ISummitEditViewModel vm = AppContext.Container.Resolve<ISummitEditViewModel>();
            view.DataContext = vm;
            vm.Name = routeToEdit.Name;
            vm.Rating = routeToEdit.Rating;

            vm.RequestCloseAfterCancel += delegate { view.Close();};
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                routeToEdit.Name = vm.Name;
                routeToEdit.Rating = vm.Rating;
            };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return routeToEdit;
        }
    }
}