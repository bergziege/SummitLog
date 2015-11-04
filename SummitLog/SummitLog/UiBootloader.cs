using System;
using DryIoc;
using SummitLog.UI.Main;
using SummitLog.UI.Main.DesignViewModels;
using SummitLog.UI.Main.ViewModels;
using SummitLog.UI.NameInput;
using SummitLog.UI.NameInput.ViewModels;

namespace SummitLog
{
    /// <summary>
    ///     Bootloader für UI Relevante Klassen
    /// </summary>
    public static class UiBootloader
    {
        /// <summary>
        ///     Initialisiert den UI Bootloader
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Container Init(Container container)
        {
            SetupViews(container);
            SetupViewModels(container);
            SetupViewCommands(container);
            return container;
        }

        private static void SetupViewCommands(Container container)
        {
            container.Register<NameInputViewCommand>();
        }

        private static void SetupViewModels(Container container)
        {
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<INameInputViewModel, NameInputViewModel>();
        }

        private static void SetupViews(Container container)
        {
            container.Register<MainView>();
            container.Register<NameInputView>();
        }
    }
}