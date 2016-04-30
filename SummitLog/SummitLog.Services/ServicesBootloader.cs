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
        public static bool IsDbAvailable(IGenericFactory genericFactory)
        {
            DbSettingsDto dbSettings = genericFactory.Resolve<ISettingsService>().LoadDbSettings();

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
        /// <returns></returns>
        public static IUnityContainer Init(IUnityContainer container)
        {
            DbSettingsDto dbSettings = container.Resolve<ISettingsService>().LoadDbSettings();

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
        }

        /// <summary>
        /// Initialisiert grundlegende Services
        /// </summary>
        /// <param name="container"></param>
        public static void InitBasics(IUnityContainer container)
        {
            container.RegisterType<IIniFileDao, IniFielDao>();
            container.RegisterType<ISettingsService, SettingsService>();
        }
    }
}