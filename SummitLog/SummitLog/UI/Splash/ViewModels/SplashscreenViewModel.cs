using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using ReactiveUI;

namespace SummitLog.UI.Splash.ViewModels
{
    /// <summary>
    ///     View Model für einen Startbildschirm
    /// </summary>
    public class SplashscreenViewModel : ReactiveObject, ISplashscreenViewModel
    {
        private int _maxProgress;
        private string _message;
        private int _progress;

        /// <summary>
        ///     Liefert eine Nachricht
        /// </summary>
        public string Message
        {
            get { return _message; }
            private set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        /// <summary>
        ///     Liefert den Fortschritt
        /// </summary>
        public int Progress
        {
            get { return _progress; }
            private set { this.RaiseAndSetIfChanged(ref _progress, value); }
        }

        /// <summary>
        ///     Liefert den maximalen Fortschritt
        /// </summary>
        public int MaxProgress
        {
            get { return _maxProgress; }
            private set { this.RaiseAndSetIfChanged(ref _maxProgress, value); }
        }

        /// <summary>
        ///     Führt die übergebenen Actionen aus
        /// </summary>
        /// <param name="actions"></param>
        public void Run(IDictionary<string, Action> actions)
        {
            if (actions == null) throw new ArgumentNullException(nameof(actions));
            RunAction(() => InitProgress(actions));

            foreach (KeyValuePair<string, Action> keyValuePair in actions)
            {
                RunAction(() => Message = keyValuePair.Key);
                RunAction(() => Progress ++);
                RunAction(keyValuePair.Value);
            }
        }

        private void RunAction(Action action)
        {
            Application.Current.Dispatcher.Invoke(
                DispatcherPriority.Render,
                action);
        }

        private void InitProgress(IDictionary<string, Action> actions)
        {
            MaxProgress = actions.Count;
            Progress = 0;
        }
    }
}