using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services.Model;
using SummitLog.UI.Common;
using SummitLog.UI.Main;
using SummitLog.UI.SummitEdit;

namespace SummitLog.UI.RouteOnSummitEdit.ViewCommands
{
    public class RouteOnSummitEditViewCommand: ViewCommandBase
    {
        
        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public Route Execute(Route routeToEdit)
        {
            RouteOnSummitEditView view = GenericFactory.Resolve<RouteOnSummitEditView>();
            ISummitEditViewModel vm = GenericFactory.Resolve<ISummitEditViewModel>();
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

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return routeToEdit;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RouteOnSummitEditViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper) : base(genericFactory, windowParentHelper)
        {
        }
    }
}