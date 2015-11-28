using System;
using System.Collections.Generic;
using System.Linq;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Schwierigkeitsgrade
    /// </summary>
    public class DifficultyLevelService : IDifficultyLevelService
    {
        private readonly IDifficultyLevelDao _difficultyLevelDao;

        /// <summary>
        ///     Erstellt eine neue Instanz des Service
        /// </summary>
        /// <param name="difficultyLevelDao"></param>
        public DifficultyLevelService(IDifficultyLevelDao difficultyLevelDao)
        {
            _difficultyLevelDao = difficultyLevelDao;
        }


        /// <summary>
        ///     Liefert alle Schwierigkeitsgrade einer Skala
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public IList<DifficultyLevel> GetAllIn(DifficultyLevelScale scale)
        {
            if (scale == null) throw new ArgumentNullException(nameof(scale));
            return _difficultyLevelDao.GetAllIn(scale).OrderBy(x=>x.Score).ToList();
        }

        /// <summary>
        ///     Erstellt einen neuen Schwierigkeitsgrad innerhalb einers Skala.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void Create(DifficultyLevelScale scale, string name, int score)
        {
            if (scale == null) throw new ArgumentNullException(nameof(scale));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            _difficultyLevelDao.Create(scale, new DifficultyLevel {Name = name, Score = score});
        }

        /// <summary>
        ///     Liefert ob der Schwierigkeitsgrad verwendet wird
        /// </summary>
        /// <param name="difficultyLevel"></param>
        /// <returns></returns>
        public bool IsInUse(DifficultyLevel difficultyLevel)
        {
            return _difficultyLevelDao.IsInUse(difficultyLevel);
        }

        /// <summary>
        ///     Löscht den Schwierigkeitsgrad, wenn dieser nicht verwendet wird.
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void Delete(DifficultyLevel difficultyLevel)
        {
            if (_difficultyLevelDao.IsInUse(difficultyLevel))
            {
                throw new NodeInUseException();
            }
            _difficultyLevelDao.Delete(difficultyLevel);
        }
    }
}