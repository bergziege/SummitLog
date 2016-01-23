using IniParser;
using IniParser.Model;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    /// Dao für Ini Files
    /// </summary>
    public class IniFielDao:IIniFileDao
    {
        /// <summary>
        ///     Lädt die Inhalte der Ini Datei
        /// </summary>
        /// <returns></returns>
        public IniData Load()
        {
            return new FileIniDataParser().ReadFile("Configuration.ini");
        }

        /// <summary>
        ///     Speichert die Inhalte in eine Ini Datei
        /// </summary>
        /// <param name="iniData"></param>
        public void Save(IniData iniData)
        {
            new FileIniDataParser().WriteFile("Configuration.ini", iniData);
        }
    }
}