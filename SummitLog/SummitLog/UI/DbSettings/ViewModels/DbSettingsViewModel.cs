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
    public class DbSettingsViewModel : ReactiveObject, IDbSettingsViewModel
    {
        private readonly ISettingsService _settingsService;
        private RelayCommand _cancelCommand;
        private string _dbPassword;
        private string _dbUrl;
        private string _dbUser;
        private RelayCommand _saveCommand;

        /// <summary>
        ///     Liefert eine neue INstanz des View Models
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

        public event EventHandler RequestClose;

        /// <summary>
        ///     Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DbSettingsDto settings = _settingsService.LoadDbSettings();
            DbUrl = settings.Url;
            DbUser = settings.User;
            DbPassword = settings.Pwd;
        }

        private bool CanSave()
        {
            return DbUrl.IsNotNullOrWhitespace() && DbUser.IsNotNullOrWhitespace() && DbPassword.IsNotNullOrWhitespace();
        }

        private void Save()
        {
            DbSettingsDto dbSettingsDto = new DbSettingsDto {Url = DbUrl, User = DbUser, Pwd = DbPassword};
            _settingsService.Save(dbSettingsDto);
            OnRequestClose();
        }

        private bool CanCancel()
        {
            return true;
        }

        private void Cancel()
        {
            OnRequestClose();
        }

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}