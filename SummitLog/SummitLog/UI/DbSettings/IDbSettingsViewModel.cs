using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.DbSettings
{
    /// <summary>
    /// Schnittstelle für View Models für <see cref="IDbSettingsView"/>
    /// </summary>
    public interface IDbSettingsViewModel : IReactiveObject
    {
        /// <summary>
        ///     Liefert oder setzt die URL des DB Servers
        /// </summary>
        string DbUrl { get; set; }

        /// <summary>
        ///     Liefert oder setzt den Nutzernamen
        /// </summary>
        string DbUser { get; set; }

        /// <summary>
        ///     Liefert oder setzt das Passwort
        /// </summary>
        string DbPassword { get; set; }

        /// <summary>
        ///     Liefert oder setzt den Pfad zur DB Startdatei
        /// </summary>
        string StartBat { get; set; }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu speichern
        /// </summary>
        RelayCommand SaveCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu verwerfen
        /// </summary>
        RelayCommand CancelCommand { get; }

        /// <summary>
        ///     Wird ausgelöst, wenn das Fenster geschlossen werden soll.
        /// </summary>
        event EventHandler RequestCloseOnSave;

        /// <summary>
        ///     Wird ausgelöst, wenn das Fenster bei einem Abbruch geschlossen werden soll
        /// </summary>
        event EventHandler RequestCloseOnCancel;

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        void LoadData();
    }
}