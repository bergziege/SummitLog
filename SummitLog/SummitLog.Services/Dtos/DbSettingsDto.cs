namespace SummitLog.Services.Dtos
{
    /// <summary>
    ///     Dto für Datenbankeinstellungen
    /// </summary>
    public class DbSettingsDto
    {
        /// <summary>
        ///     Liefert oder setzt die URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Liefert oder setzt den Nutzernamen
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Liefert oder setzt das Passwort
        /// </summary>
        public string Pwd { get; set; }
    }
}