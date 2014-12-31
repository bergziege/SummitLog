using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

using Com.QueoFlow.Commons.MVVM;
using Com.QueoFlow.Commons.MVVM.Events;

using De.BerndNet2000.SummitLog.Wpf.Factories;
using De.BerndNet2000.SummitLog.Wpf.Ui.Main;
using De.BerndNet2000.SummitLog.Wpf.Ui.Main.ViewModel;

using Spring.Context.Support;

namespace De.BerndNet2000.SummitLog.Wpf {
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App {
        /// <summary>
        ///     Wird für die Anzeige von Fehlermeldungen verwendet. TODO: Fehlermeldungen in einem geeigneten Fenster anzeigen.
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="exception"> </param>
        private static void ShowError(string message, Exception exception) {
            //ViewCommandLibrary.ShowMessageBox.Execute(message, exception);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
#if DEBUG
#else
            ShowError(e.Exception);
#endif
        }

        private async void ApplicationStartup(object sender, StartupEventArgs e) {
            try {
                if (ConfigurationManager.AppSettings["AutoUpdate"] == "1") {
                    if (UpdateService.Update()) {
                        Environment.Exit(-1);
                    }
                }
            } catch (Exception ex) {
                ShowError("Fehler beim Update", ex);
            }

            //NHibernateProfiler.Initialize();

            EventAggregator.Instance.Subscribe<ExceptionEvent>(OnExceptionEvent);

            DispatcherUnhandledException += AppDispatcherUnhandledException;

#if !DEBUG

            try {
#endif

            MainView mainView = ViewFactory.Get<MainView>();
            IViewModelFactory viewModelFactory =
                    (IViewModelFactory)ContextRegistry.GetContext().GetObject("viewModelFactory");
            MainViewModel mainViewModel = viewModelFactory.Get<MainViewModel>();

            mainView.DataContext = mainViewModel;

            mainViewModel.RequestClose += delegate {
                Environment.Exit(0);
            };

            MainWindow = mainView;
            MainWindow.Show();

            await mainViewModel.LoadDataAsync();

            MainWindow.Show();

#if !DEBUG
            } catch (Exception ex) {
                ShowError(ex);
            }
#endif
        }

        private void OnExceptionEvent(ExceptionEvent exceptionEvent) {
            ShowError(exceptionEvent.Exception);
        }

        private void ShowError(Exception exception) {
            ShowError(null, exception);
        }
    }
}