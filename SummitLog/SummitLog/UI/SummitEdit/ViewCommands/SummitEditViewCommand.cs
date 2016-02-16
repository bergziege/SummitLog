using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services.Model;
using SummitLog.UI.Main;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.SummitEdit.ViewCommands
{
    public class SummitEditViewCommand
    {
        
        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public Summit Execute(Summit summitToEdit)
        {
            SummitEditView view = AppContext.Container.Resolve<SummitEditView>();
            ISummitEditViewModel vm = AppContext.Container.Resolve<ISummitEditViewModel>();
            view.DataContext = vm;
            vm.Name = summitToEdit.Name;
            vm.SummitNumber = summitToEdit.SummitNumber;
            vm.Rating = summitToEdit.Rating;

            vm.RequestCloseAfterCancel += delegate { view.Close();};
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                summitToEdit.Name = vm.Name;
                summitToEdit.SummitNumber = vm.SummitNumber;
                summitToEdit.Rating = vm.Rating;
            };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return summitToEdit;
        }
    }
}