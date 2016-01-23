using SummitLog.Services.Dtos;

namespace SummitLog.Services.Services
{
    /// <summary>
    ///     Schnittstelle für Services für Einstellungen
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        ///     Liefert die Datenbankeinstellungen
        /// </summary>
        /// <returns></returns>
        DbSettingsDto LoadDbSettings();

        /// <summary>
        ///     Speichert die Datenbankeinstellungen
        /// </summary>
        /// <param name="dbSettings"></param>
        void Save(DbSettingsDto dbSettings);
    }
}