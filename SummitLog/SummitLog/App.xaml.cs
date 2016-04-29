using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.Services;
using SummitLog.Services.Dtos;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;
using SummitLog.UI.Common;
using SummitLog.UI.DbSettings;
using SummitLog.UI.Main;
using SummitLog.UI.Splash;
using SummitLog.UI.Splash.ViewModels;

namespace SummitLog
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            Splashscreen splashscreen = new Splashscreen();
            SplashscreenViewModel vm = new SplashscreenViewModel();
            splashscreen.DataContext = vm;
            splashscreen.Show();
            vm.Run(new Dictionary<string, Action>
            {
                {"Setup Container", CreateContainer},
                {"Setup Basic Services", AddBasicServicesToContainer},
                {"Setup Basic UI", AddBasicUiToContainer},
                {"Start Database", StartDatabase},
                {"Setup Services", AddServicesToContainer},
                {"Setup UI", AddUiToContainer},
                {"Hauptfenster vorbereiten", InitAndShowMainView}
            });
            splashscreen.Close();
        }

        private void AddBasicUiToContainer()
        {
            UiBootloader.InitBasics(AppContext.Container);
        }

        private void AddBasicServicesToContainer()
        {
            ServicesBootloader.InitBasics(AppContext.Container);
        }

        private void StartDatabase()
        {
            IGenericFactory genericFactory = AppContext.Container.Resolve<IGenericFactory>();

            IDbSettingsViewCommand dbSettingsViewCommand = genericFactory.Resolve<IDbSettingsViewCommand>();

            if (!ServicesBootloader.IsDbAvailable(genericFactory))
            {
                ISettingsService settingsService = new SettingsService(new IniFielDao());
                DbSettingsDto loadDbSettings = settingsService.LoadDbSettings();
                string startupBatchFile = loadDbSettings.StartBat;
                while (!File.Exists(startupBatchFile))
                {
                    bool dialogResult = dbSettingsViewCommand.Execute();
                    if(!dialogResult)
                    {
                        Environment.Exit(0);
                    }
                    loadDbSettings = settingsService.LoadDbSettings();
                    startupBatchFile = loadDbSettings.StartBat;

                }
                Process dbProcess = Process.Start(startupBatchFile);
                bool tryStartDatabase = true;
                while (tryStartDatabase)
                {
                    try
                    {
                        while (!ServicesBootloader.IsDbAvailable(genericFactory))
                        {
                            Thread.Sleep(1000);

                        }
                        tryStartDatabase = false;
                    }
                    catch (Exception e)
                    {
                        bool dialogResult = dbSettingsViewCommand.Execute();
                        if (!dialogResult)
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                AppContext.Container.RegisterInstance(dbProcess);

            }
        }

        private void CreateContainer()
        {
            AppContext.Container = new UnityContainer();
            AppContext.Container.RegisterType<IGenericFactory, UnityResolver>(new ContainerControlledLifetimeManager());
            AppContext.Container.RegisterType<IWindowParentHelper, WindowParentHelper>(new ContainerControlledLifetimeManager());
        }

        private void AddServicesToContainer()
        {
            ServicesBootloader.Init(AppContext.Container);
        }

        private void AddUiToContainer()
        {
            UiBootloader.Init(AppContext.Container);
        }

        private void InitAndShowMainView()
        {
            MainView mainView = AppContext.Container.Resolve<MainView>();
            IMainViewModel mainViewModel = AppContext.Container.Resolve<IMainViewModel>();
            AppContext.Container.Resolve<IWindowParentHelper>().RegisterWindow(mainView);
            mainView.DataContext = mainViewModel;
            mainViewModel.LoadData();
            MainWindow = mainView;
            MainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            /* TODO: [SUMMITLOG-15] - DB Schließen, wenn  möglich. */
        }
    }
}