using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.Services.Model;
using SummitLog.UI.Main;

namespace SummitLog.UI.NameAndLevelInput.ViewCommands
{
    /// <summary>
    /// View Command um die Eingabemaske für Name und Schwierigkeitsgrad anzuzeigen
    /// </summary>
    public class NameAndLevelInputViewCommand
    {
        /// <summary>
        /// Liefert den eingegebenen Namen
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Liefert den gewählten Schwierigkeitsgrad
        /// </summary>
        public DifficultyLevel DifficultyLevel { get; private set; }

        /// <summary>
        /// Zeigt die Eingabemaske für Namen und Schwierigkeitsgrad
        /// </summary>
        public void Execute()
        {
            Name = null;
            DifficultyLevel = null;

            NameAndLevelInputView view = AppContext.Container.Resolve<NameAndLevelInputView>();
            INameAndLevelInputViewModel vm = AppContext.Container.Resolve<INameAndLevelInputViewModel>();
            view.DataContext = vm;

            vm.RequestCloseAfterCancel += delegate { view.Close(); };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
                DifficultyLevel = vm.SelectedDifficultyLevel;
            };

            vm.LoadData();

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }

        public void Execute(string name, DifficultyLevelScale scale, DifficultyLevel level)
        {
            Name = name;
            DifficultyLevel = level;


            NameAndLevelInputView view = AppContext.Container.Resolve<NameAndLevelInputView>();
            INameAndLevelInputViewModel vm = AppContext.Container.Resolve<INameAndLevelInputViewModel>();
            view.DataContext = vm;

            vm.RequestCloseAfterCancel += delegate { view.Close(); };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
                DifficultyLevel = vm.SelectedDifficultyLevel;
            };

            vm.LoadData();

            vm.PresetValues(name, scale, level);

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }
    }
}