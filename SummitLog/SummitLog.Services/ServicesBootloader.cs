using System;
using DryIoc;
using Neo4jClient;
using SummitLog.Services.Model;
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
        /// <returns></returns>
        public static Container Init(Container container)
        {

            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
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
        }
    }
}