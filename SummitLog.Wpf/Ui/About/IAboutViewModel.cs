using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.About {
    /// <summary>
    /// Schnitstelle für ein View Model zur Anzeige eines About Dialogs.
    /// </summary>
    public interface IAboutViewModel:IViewModelBase {
        /// <summary>
        ///     Liefert die Adresse.
        /// </summary>
        string Address { get; }
        /// <summary>
        ///     Liefert den Anwendungsnamen.
        /// </summary>
        string ApplicationName { get; }
        /// <summary>
        ///     Liefert den Stadtnamen.
        /// </summary>
        string City { get; }
        /// <summary>
        ///     Liefert den Firmennamen.
        /// </summary>
        string CompanyName { get; }
        /// <summary>
        ///     Liefert die Mail Adresse.
        /// </summary>
        string EMail { get; }
        /// <summary>
        ///     Liefert die Telefonnummer.
        /// </summary>
        string Phone { get; }
        /// <summary>
        ///     Liefert die Straße.
        /// </summary>
        string Street { get; }
        /// <summary>
        ///     Liefert die Versionsnummer.
        /// </summary>
        string Version { get; }
        /// <summary>
        ///     Liefert die Webadresse.
        /// </summary>
        string Web { get; }
    }
}