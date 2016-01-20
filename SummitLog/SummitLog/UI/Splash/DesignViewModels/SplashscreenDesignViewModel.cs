using System;
using System.Collections.Generic;
using ReactiveUI;

namespace SummitLog.UI.Splash.DesignViewModels
{
    /// <summary>
    ///     Design View Model des Startbildschirms
    /// </summary>
    public class SplashscreenDesignViewModel : ReactiveObject, ISplashscreenViewModel
    {
        /// <summary>
        ///     Liefert eine neue Instanz des View Modells
        /// </summary>
        public SplashscreenDesignViewModel()
        {
            Message = "Playing Cities Skylines";
            MaxProgress = 4;
            Progress = 2;
        }

        /// <summary>
        ///     Liefert eine Nachricht
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Liefert den Fortschritt
        /// </summary>
        public int Progress { get; }

        /// <summary>
        ///     Liefert den maximalen Fortschritt
        /// </summary>
        public int MaxProgress { get; }

        /// <summary>
        ///     Führt die übergebenen Actionen aus
        /// </summary>
        /// <param name="actions"></param>
        public void Run(IDictionary<string, Action> actions)
        {
            throw new NotImplementedException();
        }
    }
}