using System;
using ReactiveUI;
using SummitLog.Extensions;
using SummitLog.Services.Dtos;
using SummitLog.Services.Services;
using SummitLog.UI.Common;

namespace SummitLog.UI.DbSettings.ViewModels
{
    /// <summary>
    ///     View Model für Datenbankeinstellungen
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DbSettingsViewModel : ReactiveObject, IDbSettingsViewModel
    {
        private readonly ISettingsService _settingsService;
        private RelayCommand _cancelCommand;
        private string _dbPassword;
        private string _dbUrl;
        private string _dbUser;
        private RelayCommand _saveCommand;
        private string _startBat;

        /// <summary>
        ///     Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="settingsService"></param>
        public DbSettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        /// <summary>
        ///     Liefert oder setzt die URL des DB Servers
        /// </summary>
        public string DbUrl
        {
            get { return _dbUrl; }
            set { this.RaiseAndSetIfChanged(ref _dbUrl, value); }
        }

        /// <summary>
        ///     Liefert oder setzt den Nutzernamen
        /// </summary>
        public string DbUser
        {
            get { return _dbUser; }
            set { this.RaiseAndSetIfChanged(ref _dbUser, value); }
        }

        /// <summary>
        ///     Liefert oder setzt das Passwort
        /// </summary>
        public string DbPassword
        {
            get { return _dbPassword; }
            set { this.RaiseAndSetIfChanged(ref _dbPassword, value); }
        }

        /// <summary>
        ///     Liefert oder setzt den Pfad zur DB Startdatei
        /// </summary>
        public string StartBat
        {
            get { return _startBat; }
            set { this.RaiseAndSetIfChanged(ref _startBat, value); }
        }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu speichern
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save, CanSave);
                }
                return _saveCommand;
            }
        }

        /// <summary>
        ///     Liefert ein Command um die Änderungen zu verwerfen
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(Cancel, CanCancel);
                }
                return _cancelCommand;
            }
        }

        /// <summary>
        ///     Wird ausgelöst, wenn das Fenster geschlossen werden soll.
        /// </summary>
        public event EventHandler RequestCloseOnSave;

        /// <summary>
        ///     Wird ausgelöst, wenn das Fenster bei einem Abbruch geschlossen werden soll
        /// </summary>
        public event EventHandler RequestCloseOnCancel;

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DbSettingsDto settings = _settingsService.LoadDbSettings();
            DbUrl = settings.Url;
            DbUser = settings.User;
            DbPassword = settings.Pwd;
            StartBat = settings.StartBat;
        }

        private bool CanSave()
        {
            return DbUrl.IsNotNullOrWhitespace() && DbUser.IsNotNullOrWhitespace() && DbPassword.IsNotNullOrWhitespace();
        }

        private void Save()
        {
            DbSettingsDto dbSettingsDto = new DbSettingsDto
            {
                Url = DbUrl,
                User = DbUser,
                Pwd = DbPassword,
                StartBat = StartBat
            };
            _settingsService.Save(dbSettingsDto);
            OnRequestCloseOnSave();
        }

        private bool CanCancel()
        {
            return true;
        }

        private void Cancel()
        {
            OnRequestCloseOnCancel();
        }

        protected virtual void OnRequestCloseOnSave()
        {
            RequestCloseOnSave?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRequestCloseOnCancel()
        {
            RequestCloseOnCancel?.Invoke(this, EventArgs.Empty);
        }
    }
}