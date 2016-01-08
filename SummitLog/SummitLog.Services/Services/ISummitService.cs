using System.Collections.Generic;
using SummitLog.Services.Model;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Gipfel
    /// </summary>
    public interface ISummitService
    {
        /// <summary>
        ///     Liefert alle Gipfel innerhalb einers Gruppe
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <returns></returns>
        IList<Summit> GetAllIn(SummitGroup summitGroup);

        /// <summary>
        ///     Erstellt einen neuen Gipfel innerhalb einer Gruppe mit dem übergebenen Namen.
        /// </summary>
        /// <param name="summitGroup"></param>
        /// <param name="name"></param>
        /// <param name="summitNumber"></param>
        void Create(SummitGroup summitGroup, string name, string summitNumber);

        /// <summary>
        ///     Liefert ob ein Gipfel verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        /// <returns></returns>
        bool IsInUse(Summit summit);

        /// <summary>
        ///     Löscht einen Gipfel wenn dieser nicht mehr verwendet wird
        /// </summary>
        /// <param name="summit"></param>
        void Delete(Summit summit);

        /// <summary>
        ///     Speichert ein Gipfel
        /// </summary>
        /// <param name="summit"></param>
        void Save(Summit summit);
    }
}