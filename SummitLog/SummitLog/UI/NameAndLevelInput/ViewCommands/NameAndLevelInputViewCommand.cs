using DryIoc;
using SummitLog.Services.Model;

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
            view.ShowDialog();
        }
    }
}