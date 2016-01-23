using System;
using IniParser;
using IniParser.Model;
using ReactiveUI;
using SummitLog.Extensions;
using SummitLog.Properties;
using SummitLog.UI.Common;

namespace SummitLog.UI.DbSettings.ViewModels
{
    public class DbSettingsViewModel : ReactiveObject, IDbSettingsViewModel
    {
        private RelayCommand _cancelCommand;
        private string _dbPassword;
        private string _dbUrl;
        private string _dbUser;
        private RelayCommand _saveCommand;
        private FileIniDataParser _parser;
        private IniData _data;

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
            if (_parser == null)
            {
                _parser = new FileIniDataParser();
            }
            if (_data == null)
            {
                _data = _parser.ReadFile("Configuration.ini");
            }
            DbUrl = _data["DB"]["Url"];
            DbUser = _data["DB"]["User"];
            DbPassword = _data["DB"]["Pwd"];
        }

        private bool CanSave()
        {
            return DbUrl.IsNotNullOrWhitespace() && DbUser.IsNotNullOrWhitespace() && DbPassword.IsNotNullOrWhitespace();
        }

        private void Save()
        {
            _data["DB"]["Url"] = DbUrl;
            _data["DB"]["User"] = DbUser;
            _data["DB"]["Pwd"] = DbPassword;
            _parser.WriteFile("Configuration.ini", _data);
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