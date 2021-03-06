﻿using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services;
using SummitLog.Services.Model;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.NameAndLevelInput.ViewCommands
{
    /// <summary>
    /// View Command um die Eingabemaske für Name und Schwierigkeitsgrad anzuzeigen
    /// </summary>
    public class NameAndLevelInputViewCommand : ViewCommandBase
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

            NameAndLevelInputView view = GenericFactory.Resolve<NameAndLevelInputView>();
            INameAndLevelInputViewModel vm = GenericFactory.Resolve<INameAndLevelInputViewModel>();
            view.DataContext = vm;

            vm.RequestCloseAfterCancel += delegate { view.Close(); };
            vm.RequestCloseAfterOk += delegate
            {
                view.Close();
                Name = vm.Name;
                DifficultyLevel = vm.SelectedDifficultyLevel;
            };

            vm.LoadData();

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }

        public void Execute(string name, DifficultyLevelScale scale, DifficultyLevel level)
        {
            Name = name;
            DifficultyLevel = level;


            NameAndLevelInputView view = GenericFactory.Resolve<NameAndLevelInputView>();
            INameAndLevelInputViewModel vm = GenericFactory.Resolve<INameAndLevelInputViewModel>();
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

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NameAndLevelInputViewCommand(IGenericFactory genericFactory, IWindowParentHelper windowParentHelper) : base(genericFactory, windowParentHelper)
        {
        }
    }
}