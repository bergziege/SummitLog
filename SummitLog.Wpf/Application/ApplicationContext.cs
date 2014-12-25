namespace De.BerndNet2000.SummitLog.Wpf.Application {
    /// <summary>
    ///     Anwendungskontext.
    ///     Enthällt verschiedene, programmweit gültige Daten.
    /// </summary>
    public class ApplicationContext {
        private const string VERSION = "2.2.2";
        private static readonly ApplicationContext instance = new ApplicationContext();

        static ApplicationContext() {
        }

        private ApplicationContext() {
        }

        /// <summary>
        ///     Liefert die einzige Instanz des Application Context
        /// </summary>
        public static ApplicationContext Instance {
            get { return instance; }
        }

        /// <summary>
        ///     Liefert die aktuelle Versionsnummer des Programms.
        ///     Gegen diese Nummer wird der Datenbankabgleich durchgeführt.
        /// </summary>
        public string Version {
            get { return VERSION; }
        }
    }
}