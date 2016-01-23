using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.DbSettings.DesignViewModels
{
    public class DbSettingsDesignViewModel:ReactiveObject, IDbSettingsViewModel
    {
        public DbSettingsDesignViewModel()
        {
            DbUrl = "Url";
            DbUser = "user";
            DbPassword = "pass";
        }

        /// <summary>
        ///     Liefert oder setzt die URL des DB Servers
        /// </summary>
        public string DbUrl { get; set; }

        /// <summary>
        ///     Liefert oder setzt den Nutzernamen
        /// </summary>
        public string DbUser { get; set; }

        /// <summary>
        ///     Liefert oder setzt das Passwort
        /// </summary>
        public string DbPassword { get; set; }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu speichern
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu verwerfen
        /// </summary>
        public RelayCommand CancelCommand { get; }
        public event EventHandler RequestClose;

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            throw new NotImplementedException();
        }
    }
}