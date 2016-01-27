﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.Services;
using SummitLog.Services.Dtos;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;
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
                {"Start Database", StartDatabase},
                {"Setup Container", CreateContainer},
                {"Setup Services", AddServicesToContainer},
                {"Setup UI", AddUiToContainer},
                {"Hauptfenster vorbereiten", InitAndShowMainView}
            });
            splashscreen.Close();
        }

        private void StartDatabase()
        {
            if (!ServicesBootloader.IsDbAvailable())
            {
                ISettingsService settingsService = new SettingsService(new IniFielDao());
                DbSettingsDto loadDbSettings = settingsService.LoadDbSettings();
                Process.Start(loadDbSettings.StartBat);
                while (!ServicesBootloader.IsDbAvailable())
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void CreateContainer()
        {
            Thread.Sleep(250);
            AppContext.Container = new Container();
        }
        
        private void AddServicesToContainer()
        {
            Thread.Sleep(250);

            ServicesBootloader.Init(AppContext.Container);
        }

        private void AddUiToContainer()
        {
            Thread.Sleep(250);
            UiBootloader.Init(AppContext.Container);
        }

        private void InitAndShowMainView()
        {
            Thread.Sleep(250);
            MainView mainView = AppContext.Container.Resolve<MainView>();
            IMainViewModel mainViewModel = AppContext.Container.Resolve<IMainViewModel>();
            WindowParentHelper.Instance.RegisterWindow(mainView);
            mainView.DataContext = mainViewModel;
            mainViewModel.LoadData();
            MainWindow = mainView;
            MainWindow.Show();
        }
    }
}