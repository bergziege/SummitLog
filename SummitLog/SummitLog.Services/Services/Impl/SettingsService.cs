using IniParser.Model;
using SummitLog.Services.Dtos;
using SummitLog.Services.Persistence;

namespace SummitLog.Services.Services.Impl
{
    /// <summary>
    ///     Service für Einstellungen
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly IIniFileDao _iniFileDao;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public SettingsService(IIniFileDao iniFileDao)
        {
            _iniFileDao = iniFileDao;
        }

        /// <summary>
        ///     Liefert die Datenbankeinstellungen
        /// </summary>
        /// <returns></returns>
        public DbSettingsDto LoadDbSettings()
        {
            IniData settingsData = _iniFileDao.Load();
            return new DbSettingsDto
            {
                Url = settingsData["DB"]["Url"],
                User = settingsData["DB"]["User"],
                Pwd = settingsData["DB"]["Pwd"],
                StartBat = settingsData["DB"]["StartBat"]
            };
        }

        /// <summary>
        ///     Speichert die Datenbankeinstellungen
        /// </summary>
        /// <param name="dbSettings"></param>
        public void Save(DbSettingsDto dbSettings)
        {
            IniData settingsData = _iniFileDao.Load();
            settingsData["DB"]["Url"] = dbSettings.Url;
            settingsData["DB"]["User"] = dbSettings.User;
            settingsData["DB"]["Pwd"] = dbSettings.Pwd;
            settingsData["DB"]["StartBat"] = dbSettings.StartBat;
            _iniFileDao.Save(settingsData);
        }
    }
}