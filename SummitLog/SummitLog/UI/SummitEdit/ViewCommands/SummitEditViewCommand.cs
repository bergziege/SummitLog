using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services.Model;
using SummitLog.UI.Common;
using SummitLog.UI.Main;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.SummitEdit.ViewCommands
{
    public class SummitEditViewCommand: ViewCommandBase
    {
        
        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public Summit Execute(Summit summitToEdit)
        {
            SummitEditView view = GenericFactory.Resolve<SummitEditView>();
            ISummitEditViewModel vm = GenericFactory.Resolve<ISummitEditViewModel>();
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

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return summitToEdit;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public SummitEditViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper) : base(genericFactory, windowParentHelper)
        {
        }
    }
}