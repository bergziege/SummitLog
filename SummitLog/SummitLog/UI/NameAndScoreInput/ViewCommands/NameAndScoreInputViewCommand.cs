using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.UI.Main;
using SummitLog.UI.NameInput;

namespace SummitLog.UI.NameAndScoreInput.ViewCommands
{
    public class NameAndScoreInputViewCommand
    {
        /// <summary>
        /// Liefert oder setzt den eingegebenen Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die eingegebene Punktezahl
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Führt das View Command aus
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {
            Name = string.Empty;
            Score = 0;
            NameAndScoreInputView view = AppContext.Container.Resolve<NameAndScoreInputView>();
            INameAndScoreInputViewModel vm = AppContext.Container.Resolve<INameAndScoreInputViewModel>();
            view.DataContext = vm;

            vm.RequestCloseAfterCancel += delegate { view.Close(); };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
                Score = vm.Score;
            };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }

        public void Execute(string name, int score)
        {
            NameAndScoreInputView view = AppContext.Container.Resolve<NameAndScoreInputView>();
            INameAndScoreInputViewModel vm = AppContext.Container.Resolve<INameAndScoreInputViewModel>();
            view.DataContext = vm;

            vm.Name = name;
            vm.Score = score;

            vm.RequestCloseAfterCancel += delegate { view.Close(); };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
                Score = vm.Score;
            };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }
    }
}