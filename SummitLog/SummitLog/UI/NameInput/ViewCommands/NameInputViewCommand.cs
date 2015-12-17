using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.UI.Main;

namespace SummitLog.UI.NameInput
{
    public class NameInputViewCommand
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
            NameInputView view = AppContext.Container.Resolve<NameInputView>();
            INameInputViewModel vm = AppContext.Container.Resolve<INameInputViewModel>();
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

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }
    }
}