using System;
using System.Collections.Generic;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Variations
    /// </summary>
    public class VariationService : IVariationService
    {
        private readonly IVariationDao _variationDao;

        /// <summary>
        /// Liefert eine neue Instanz des Services
        /// </summary>
        /// <param name="variationDao"></param>
        public VariationService(IVariationDao variationDao)
        {
            _variationDao = variationDao;
        }

        /// <summary>
        ///     Liefert alle Variationen einer Route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public IList<Variation> GetAllOn(Route route)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            return _variationDao.GetAllOn(route);
        }

        /// <summary>
        ///     Erstellt eine neue Variation einer Route mit einem Schwierigkeitsgrad
        /// </summary>
        /// <param name="variationName"></param>
        /// <param name="route"></param>
        /// <param name="difficultyLevel"></param>
        public void Create(string variationName, Route route, DifficultyLevel difficultyLevel)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (difficultyLevel == null) throw new ArgumentNullException(nameof(difficultyLevel));
            if (string.IsNullOrWhiteSpace(variationName)) throw new ArgumentNullException(nameof(variationName));
            _variationDao.Create(new Variation() {Name = variationName}, route, difficultyLevel);
        }
    }
}