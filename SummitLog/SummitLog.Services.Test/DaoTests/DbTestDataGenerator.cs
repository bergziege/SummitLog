using System;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Impl;

namespace SummitLog.Services.Test.DaoTests
{
    public class DbTestDataGenerator
    {
        private readonly AreaDao _areaDao;
        private readonly CountryDao _countryDao;
        private readonly DifficultyLevelDao _difficultyLevelDao;
        private readonly DifficultyLevelScaleDao _difficultyLevelScaleDao;
        private readonly RouteDao _routeDao;
        private readonly SummitDao _summitDao;
        private readonly SummitGroupDao _summitGroupDao;
        private readonly VariationDao _variationDao;
        private readonly LogEntryDao _logEntryDao;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public DbTestDataGenerator(CountryDao countryDao, AreaDao areaDao, SummitGroupDao summitGroupDao,
            SummitDao summitDao, RouteDao routeDao, DifficultyLevelScaleDao difficultyLevelScaleDao,
            DifficultyLevelDao difficultyLevelDao, VariationDao variationDao, LogEntryDao logEntryDao)
        {
            _countryDao = countryDao;
            _areaDao = areaDao;
            _summitGroupDao = summitGroupDao;
            _summitDao = summitDao;
            _routeDao = routeDao;
            _difficultyLevelScaleDao = difficultyLevelScaleDao;
            _difficultyLevelDao = difficultyLevelDao;
            _variationDao = variationDao;
            _logEntryDao = logEntryDao;
        }

        public Country CreateCountry(string name = "Land")
        {
            Country newCountry = new Country {Name = name};
            return _countryDao.Create(newCountry);
        }

        public Area CreateArea(string name = "Gebiet", Country country = null)
        {
            if (country == null)
            {
                country = CreateCountry();
            }
            Area newArea = new Area {Name = name};
            return _areaDao.Create(country, newArea);
        }

        public SummitGroup CreateSummitGroup(string name = "Gipfelgruppe", Area area = null)
        {
            if (area == null)
            {
                area = CreateArea();
            }
            SummitGroup newSummitGroup = new SummitGroup {Name = name};
            return _summitGroupDao.Create(area, newSummitGroup);
        }

        public Summit CreateSummit(string name = "Gipfel", string summitNumber = "100A", double rating = 0,
            SummitGroup summitGroup = null)
        {
            if (summitGroup == null)
            {
                summitGroup = CreateSummitGroup();
            }
            Summit newSummit = new Summit {Name = name, SummitNumber = summitNumber, Rating = rating};
            return _summitDao.Create(summitGroup, newSummit);
        }

        public Route CreateRouteInCountry(string name = "Route im Land", double rating = 3.5, Country country = null)
        {
            if (country == null)
            {
                country = CreateCountry();
            }
            return _routeDao.CreateIn(country, GetRouteEntity(name, rating));
        }

        private static Route GetRouteEntity(string name, double rating)
        {
            Route route = new Route {Name = name, Rating = rating};
            return route;
        }

        public Route CreateRouteInArea(string name = "Route im Gebiet", double rating = 3.5, Area area = null)
        {
            if (area == null)
            {
                area = CreateArea();
            }
            return _routeDao.CreateIn(area, GetRouteEntity(name, rating));
        }

        public Route CreateRouteInSummitGroup(string name = "Route in Gipfelgruppe", double rating = 3.5,
            SummitGroup summitGroup = null)
        {
            //if (summitGroup == null)
            //{
            //    summitGroup = CreateSummitGroup();
            //}
            return _routeDao.CreateIn(summitGroup, GetRouteEntity(name, rating));
        }

        public Route CreateRouteInSummit(string name = "Route auf Gipfel", double rating = 3.5, Summit summit = null)
        {
            //if (summit == null)
            //{
            //    summit = CreateSummit();
            //}
            return _routeDao.CreateIn(summit, GetRouteEntity(name, rating));
        }

        public DifficultyLevelScale CreateDifficultyLevelScale(string name = "Schwierigkeitsgradskala", bool isDefault = false)
        {
            DifficultyLevelScale scale = new DifficultyLevelScale {Name = name};
            if (isDefault)
            {
                scale.SetAsDefault();
            }
            return _difficultyLevelScaleDao.Create(scale);
        }

        public DifficultyLevel CreateDifficultyLevel(string name = "Schwierigkeitsgrad", int score = 10,
            DifficultyLevelScale difficultyLevelScale = null)
        {
            if (difficultyLevelScale == null)
            {
                difficultyLevelScale = CreateDifficultyLevelScale();
            }
            return _difficultyLevelDao.Create(difficultyLevelScale, new DifficultyLevel {Name = name, Score = score});
        }

        public Variation CreateVariation(DifficultyLevel difficultyLevel = null, Route route = null,
            string name = "Variation")
        {
            if (difficultyLevel == null)
            {
                difficultyLevel = CreateDifficultyLevel();
            }
            if (route == null)
            {
                route = CreateRouteInCountry();
            }
            return _variationDao.Create(new Variation {Name = name}, route, difficultyLevel);
        }

        public LogEntry CreateLogEntry(Variation variation = null, string memo = "Ich war hier",
            DateTime? logDate = null)
        {
            //if (variation == null)
            //{
            //    variation = CreateVariation();
            //}
            if (!logDate.HasValue)
            {
                logDate = new DateTime(2015, 01, 01);
            }
            return _logEntryDao.Create(variation, new LogEntry
            {
                DateTime = logDate.Value,
                Memo = memo
            });
        }
    }
}