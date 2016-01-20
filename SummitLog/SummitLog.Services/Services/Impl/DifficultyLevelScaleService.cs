using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Schwierigkeitsgradskalen
    /// </summary>
    public class DifficultyLevelScaleService : IDifficultyLevelScaleService
    {
        private readonly IDifficultyLevelScaleDao _difficultyLevelScaleDao;

        /// <summary>
        ///     Erstellt eine neue Instanz des DifficultyLevelScaleServices
        /// </summary>
        /// <param name="difficultyLevelScaleDao"></param>
        public DifficultyLevelScaleService(IDifficultyLevelScaleDao difficultyLevelScaleDao)
        {
            _difficultyLevelScaleDao = difficultyLevelScaleDao;
        }

        /// <summary>
        ///     Liefert alle Schwierigkeitsgradskalen
        /// </summary>
        /// <returns></returns>
        public IList<DifficultyLevelScale> GetAll()
        {
            return _difficultyLevelScaleDao.GetAll().OrderBy(x=>x.Name).ToList();
        }

        /// <summary>
        ///     Erstellt eine neue Schwiergkeitsgradskala mit dem übergebenen Namen
        /// </summary>
        /// <param name="scaleName"></param>
        public DifficultyLevelScale Create(string scaleName)
        {
            if (string.IsNullOrWhiteSpace(scaleName))
                throw new ArgumentNullException(nameof(scaleName));
            return _difficultyLevelScaleDao.Create(new DifficultyLevelScale{Name = scaleName});
        }

        /// <summary>
        ///     Liefert ob eine Skale verwendet wird
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        /// <returns></returns>
        public bool IsInUse(DifficultyLevelScale difficultyLevelScale)
        {
            return _difficultyLevelScaleDao.IsInUse(difficultyLevelScale);
        }

        /// <summary>
        ///     Löscht die Skale, wenn nicht mehr verwendet
        /// </summary>
        /// <param name="difficultyLevelScale"></param>
        public void Delete(DifficultyLevelScale difficultyLevelScale)
        {
            if (_difficultyLevelScaleDao.IsInUse(difficultyLevelScale))
            {
                throw new NodeInUseException();
            }
            _difficultyLevelScaleDao.Delete(difficultyLevelScale);
        }

        /// <summary>
        ///     Speichert die Schwierigkeitsgradskale
        /// </summary>
        /// <param name="scaleToSave"></param>
        public void Save(DifficultyLevelScale scaleToSave)
        {
            if (scaleToSave == null) throw new ArgumentNullException(nameof(scaleToSave));
            _difficultyLevelScaleDao.Save(scaleToSave);
        }

        /// <summary>
        ///     Liefert die Sakale zu einer Skala
        /// </summary>
        /// <param name="difficultyLevel"></param>
        /// <returns></returns>
        public DifficultyLevelScale GetForDifficultyLevel(DifficultyLevel difficultyLevel)
        {
            if (difficultyLevel == null) throw new ArgumentNullException(nameof(difficultyLevel));
            return _difficultyLevelScaleDao.GetForDifficultyLevel(difficultyLevel);
        }
    }
}