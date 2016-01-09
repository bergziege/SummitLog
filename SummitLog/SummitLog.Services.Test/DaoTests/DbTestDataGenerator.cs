using System;
using Neo4jClient;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    public class DbTestDataGenerator
    {
        private readonly GraphClient _graphClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DbTestDataGenerator(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public Country CreateCountry(string name = "Land")
        {
            CountryDao dao = new CountryDao(_graphClient);
            Country newCountry = new Country() { Name = name };
            return dao.Create(newCountry);
        }

        public Area CreateArea(string name = "Gebiet", Country country = null)
        {
            if (country == null)
            {
                country = CreateCountry();
            }
            AreaDao areaDao = new AreaDao(_graphClient);
            Area newArea = new Area() { Name = name };
            return areaDao.Create(country, newArea);
        }

        public SummitGroup CreateSummitGroup(string name = "Gipfelgruppe", Area area = null)
        {
            if (area == null)
            {
                area = CreateArea();
            }
            SummitGroupDao summitGroupDao = new SummitGroupDao(_graphClient);
            SummitGroup newSummitGroup = new SummitGroup() { Name = name };
            return summitGroupDao.Create(area, newSummitGroup);
        }

        public Summit CreateSummit(string name = "Gipfel", string summitNumber = "100A", double rating = 0, SummitGroup summitGroup = null)
        {
            if (summitGroup == null)
            {
                summitGroup = CreateSummitGroup();
            }
            SummitDao summitDao = new SummitDao(_graphClient);
            Summit newSummit = new Summit() { Name = name, SummitNumber = summitNumber, Rating = rating};
            return summitDao.Create(summitGroup, newSummit);
        }

        public Route CreateRouteInCountry(string name = "Route im Land", Country country = null)
        {
            if (country == null)
            {
                country = CreateCountry();
            }
            return new RouteDao(_graphClient).CreateIn(country, GetRouteWithName(name));
        }

        private static Route GetRouteWithName(string name)
        {
            Route route = new Route() { Name = name };
            return route;
        }

        public Route CreateRouteInArea(string name = "Route im Gebiet", Area area = null)
        {
            if (area == null)
            {
                area = CreateArea();
            }
            return new RouteDao(_graphClient).CreateIn(area, GetRouteWithName(name));
        }

        public Route CreateRouteInSummitGroup(string name = "Route in Gipfelgruppe", SummitGroup summitGroup = null)
        {
            //if (summitGroup == null)
            //{
            //    summitGroup = CreateSummitGroup();
            //}
            return new RouteDao(_graphClient).CreateIn(summitGroup, GetRouteWithName(name));
        }

        public Route CreateRouteInSummit(string name = "Route auf Gipfel", Summit summit = null)
        {
            //if (summit == null)
            //{
            //    summit = CreateSummit();
            //}
            return new RouteDao(_graphClient).CreateIn(summit, GetRouteWithName(name));
        }

        public DifficultyLevelScale CreateDifficultyLevelScale(string name = "Schwierigkeitsgradskala")
        {
            return new DifficultyLevelScaleDao(_graphClient).Create(new DifficultyLevelScale() { Name = name });
        }

        public DifficultyLevel CreateDifficultyLevel(string name = "Schwierigkeitsgrad", int score = 10, DifficultyLevelScale difficultyLevelScale = null)
        {
            if (difficultyLevelScale == null)
            {
                difficultyLevelScale = CreateDifficultyLevelScale();
            }
            return new DifficultyLevelDao(_graphClient).Create(difficultyLevelScale, new DifficultyLevel() { Name = name, Score = score });
        }

        public Variation CreateVariation(DifficultyLevel difficultyLevel = null,Route route = null, string name = "Variation")
        {
            if (difficultyLevel == null)
            {
                difficultyLevel = CreateDifficultyLevel();
            }
            if (route == null)
            {
                route = CreateRouteInCountry();
            }
            return new VariationDao(_graphClient).Create(new Variation() {Name = name}, route, difficultyLevel);
        }

        public LogEntry CreateLogEntry(Variation variation = null, string memo = "Ich war hier", DateTime? logDate = null)
        {
            //if (variation == null)
            //{
            //    variation = CreateVariation();
            //}
            if (!logDate.HasValue)
            {
                logDate = new DateTime(2015,01,01);
            }
            return new LogEntryDao(_graphClient).Create(variation, new LogEntry()
            {
                DateTime = logDate.Value, Memo = memo
            });
        }
    }
}