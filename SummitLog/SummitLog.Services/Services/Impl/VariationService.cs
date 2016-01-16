using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
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
            return _variationDao.GetAllOn(route).ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Variation einer Route mit einem Schwierigkeitsgrad
        /// </summary>
        /// <param name="route"></param>
        /// <param name="difficultyLevel"></param>
        /// <param name="variationName"></param>
        public Variation Create(Route route, DifficultyLevel difficultyLevel, string variationName)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (difficultyLevel == null) throw new ArgumentNullException(nameof(difficultyLevel));
            return _variationDao.Create(new Variation() {Name = variationName}, route, difficultyLevel);
        }

        /// <summary>
        ///     Löscht eine Variation, wenn diese nicht mehr verwendet wird.
        /// </summary>
        /// <param name="variation"></param>
        public void Delete(Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            if (_variationDao.IsInUse(variation))
            {
                throw new NodeInUseException();
            }
            _variationDao.Delete(variation);
        }

        /// <summary>
        ///     Prüft, ob eine Varation noch in Verwendung ist.
        /// </summary>
        /// <param name="variation"></param>
        /// <returns></returns>
        public bool IsInUse(Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            return _variationDao.IsInUse(variation);
        }

        /// <summary>
        ///     Speichert die Variante
        /// </summary>
        /// <param name="variation"></param>
        public void Save(Variation variation)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            _variationDao.Save(variation);
        }

        /// <summary>
        ///     Ändert den Schwierigkeitsgrad einer Variation
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="newLevel"></param>
        public void ChangeDifficultyLevel(Variation variation, DifficultyLevel newLevel)
        {
            if (variation == null) throw new ArgumentNullException(nameof(variation));
            if (newLevel == null) throw new ArgumentNullException(nameof(newLevel));
            _variationDao.ChangeDifficultyLevel(variation, newLevel);
        }
    }
}