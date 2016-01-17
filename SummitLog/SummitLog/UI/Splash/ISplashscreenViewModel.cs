using System;
using System.Collections.Generic;
using ReactiveUI;

namespace SummitLog.UI.Splash
{
    /// <summary>
    ///     Schnittstelle für View Models des Startbildschirms
    /// </summary>
    public interface ISplashscreenViewModel : IReactiveObject
    {
        /// <summary>
        ///     Liefert eine Nachricht
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Liefert den Fortschritt
        /// </summary>
        int Progress { get; }

        /// <summary>
        ///     Liefert den maximalen Fortschritt
        /// </summary>
        int MaxProgress { get; }

        /// <summary>
        ///     Führt die übergebenen Actionen aus
        /// </summary>
        /// <param name="actions"></param>
        void Run(IDictionary<string, Action> actions);
    }
}