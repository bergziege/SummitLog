using System;
using System.ComponentModel;
using Microsoft.Practices.Unity;
using Neo4jClient;
using SummitLog.Services.Dtos;
using SummitLog.Services.Persistence;
using SummitLog.Services.Persistence.Impl;
using SummitLog.Services.Services;
using SummitLog.Services.Services.Impl;

namespace SummitLog.Services
{
    /// <summary>
    ///     Bootloader für den Servicebereich
    /// </summary>
    public static class ServicesBootloader
    {
        public static bool IsDbAvailable()
        {
            ISettingsService settingsService = new SettingsService(new IniFielDao());
            DbSettingsDto dbSettings = settingsService.LoadDbSettings();

            GraphClient client = new GraphClient(new Uri(dbSettings.Url), dbSettings.User, dbSettings.Pwd);
            bool isDbAvailable = false;
            try
            {
                client.Connect();
                isDbAvailable = true;
                client.Dispose();
            }
            catch
            {
                // ignored
            }
            return isDbAvailable;
        }

        /// <summary>
        ///     Initialisiert den Bootloader
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dbUrl"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbPassword"></param>
        /// <returns></returns>
        public static IUnityContainer Init(IUnityContainer container)
        {
            /* TODO: Services usw. sollten eigentlich über den Containedr bezogen werden. 
             * Dazu muss jedoch der Client bereits fertig sein, zu dem hier aber erst noch die Einstellungen über
             * einen Service geladen werden müssen */
            ISettingsService settingsService = new SettingsService(new IniFielDao());
            DbSettingsDto dbSettings = settingsService.LoadDbSettings();

            GraphClient client = new GraphClient(new Uri(dbSettings.Url), dbSettings.User, dbSettings.Pwd);
            client.Connect();

            container.RegisterInstance(client);

            RegisterDaos(container);
            RegisterServices(container);

            return container;
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IAreaService, AreaService>();
            container.RegisterType<ISummitGroupService, SummitGroupService>();
            container.RegisterType<ISummitService, SummitService>();
            container.RegisterType<IRouteService, RouteService>();
            container.RegisterType<IVariationService, VariationService>();
            container.RegisterType<ILogEntryService, LogEntryService>();
            container.RegisterType<IDifficultyLevelScaleService, DifficultyLevelScaleService>();
            container.RegisterType<IDifficultyLevelService, DifficultyLevelService>();
            container.RegisterType<ISettingsService, SettingsService>();
        }

        private static void RegisterDaos(IUnityContainer container)
        {
            container.RegisterType<ICountryDao, CountryDao>();
            container.RegisterType<IAreaDao, AreaDao>();
            container.RegisterType<ISummitGroupDao, SummitGroupDao>();
            container.RegisterType<ISummitDao, SummitDao>();
            container.RegisterType<IRoutesDao, RouteDao>();
            container.RegisterType<IDifficultyLevelScaleDao, DifficultyLevelScaleDao>();
            container.RegisterType<IDifficultyLevelDao, DifficultyLevelDao>();
            container.RegisterType<IVariationDao, VariationDao>();
            container.RegisterType<ILogEntryDao, LogEntryDao>();
            container.RegisterType<IIniFileDao, IniFielDao>();
        }
    }
}