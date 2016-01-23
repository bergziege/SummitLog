using System;
using DryIoc;
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
        /// <summary>
        ///     Initialisiert den Bootloader
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dbUrl"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbPassword"></param>
        /// <returns></returns>
        public static Container Init(Container container)
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

        private static void RegisterServices(Container container)
        {
            container.Register<ICountryService, CountryService>();
            container.Register<IAreaService, AreaService>();
            container.Register<ISummitGroupService, SummitGroupService>();
            container.Register<ISummitService, SummitService>();
            container.Register<IRouteService, RouteService>();
            container.Register<IVariationService, VariationService>();
            container.Register<ILogEntryService, LogEntryService>();
            container.Register<IDifficultyLevelScaleService, DifficultyLevelScaleService>();
            container.Register<IDifficultyLevelService, DifficultyLevelService>();
            container.Register<ISettingsService, SettingsService>();
        }

        private static void RegisterDaos(Container container)
        {
            container.Register<ICountryDao, CountryDao>();
            container.Register<IAreaDao, AreaDao>();
            container.Register<ISummitGroupDao, SummitGroupDao>();
            container.Register<ISummitDao, SummitDao>();
            container.Register<IRoutesDao, RouteDao>();
            container.Register<IDifficultyLevelScaleDao, DifficultyLevelScaleDao>();
            container.Register<IDifficultyLevelDao, DifficultyLevelDao>();
            container.Register<IVariationDao, VariationDao>();
            container.Register<ILogEntryDao, LogEntryDao>();
            container.Register<IIniFileDao, IniFielDao>();
        }
    }
}