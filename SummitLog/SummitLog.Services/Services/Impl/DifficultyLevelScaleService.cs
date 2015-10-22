using System;
using System.Collections.Generic;
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
            return _difficultyLevelScaleDao.GetAll();
        }

        /// <summary>
        ///     Erstellt eine neue Schwiergkeitsgradskala mit dem übergebenen Namen
        /// </summary>
        /// <param name="scaleName"></param>
        public void Create(string scaleName)
        {
            if (string.IsNullOrWhiteSpace(scaleName))
                throw new ArgumentNullException(nameof(scaleName));
            _difficultyLevelScaleDao.Create(new DifficultyLevelScale{Name = scaleName});
        }
    }
}