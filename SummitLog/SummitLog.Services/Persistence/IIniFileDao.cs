using IniParser.Model;

namespace SummitLog.Services.Persistence
{
    /// <summary>
    ///     Schnittstelle für einen Dao für Ini Dateien
    /// </summary>
    public interface IIniFileDao
    {
        /// <summary>
        ///     Lädt die Inhalte der Ini Datei
        /// </summary>
        /// <returns></returns>
        IniData Load();

        /// <summary>
        ///     Speichert die Inhalte in eine Ini Datei
        /// </summary>
        /// <param name="iniData"></param>
        void Save(IniData iniData);
    }
}