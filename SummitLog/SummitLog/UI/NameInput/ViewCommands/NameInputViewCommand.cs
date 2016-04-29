using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.NameInput.ViewCommands
{
    public class NameInputViewCommand: ViewCommandBase
    {
        /// <summary>
        /// Liefert oder setzt den eingegebenen Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public void Execute(string name = "")
        {
            Name = name;
            NameInputView view = GenericFactory.Resolve<NameInputView>();
            INameInputViewModel vm = GenericFactory.Resolve<INameInputViewModel>();
            view.DataContext = vm;
            vm.Name = name;

            vm.RequestCloseAfterCancel += delegate { view.Close();
                                                       Name = null;
            };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
            };

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NameInputViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper) : base(genericFactory, windowParentHelper)
        {
        }
    }
}