using System;
using DryIoc;
using SummitLog.UI.Main;
using SummitLog.UI.Main.DesignViewModels;

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
            return container;
        }

        private static void SetupViewModels(Container container)
        {
            container.Register<IMainViewModel, MainDesignViewModel>();
        }

        private static void SetupViews(Container container)
        {
            container.Register<MainView>();
        }
    }
}