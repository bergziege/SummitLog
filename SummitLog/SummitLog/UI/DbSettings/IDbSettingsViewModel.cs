using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.DbSettings
{
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
        event EventHandler RequestClose;

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        void LoadData();
    }
}