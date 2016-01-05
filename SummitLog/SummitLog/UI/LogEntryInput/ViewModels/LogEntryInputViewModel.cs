using System;
using ReactiveUI;
using SummitLog.Extensions;
using SummitLog.UI.Common;

namespace SummitLog.UI.LogEntryInput.ViewModels
{
    /// <summary>
    /// View Model einer Ansicht zur Eingabe von Informationen zu einem Logeintrag
    /// </summary>
    public class LogEntryInputViewModel : ReactiveObject, ILogEntryInputViewModel
    {
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;
        private string _memo;
        private DateTime _date = DateTime.Now;

        /// <summary>
        /// Liefert das OK Command
        /// </summary>
        public RelayCommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(Ok, CanOk);
                }
                return _okCommand;
            }
        }

        private bool CanOk()
        {
            return true;
        }

        private void Ok()
        {
            OnRequestCloseAfterOk();
        }

        /// <summary>
        /// Liefert das Abbrechen kommand
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(Cancel, null);
                }
                return _cancelCommand;
            }
        }

        private void Cancel()
        {
            OnRequestCloseAfterCancel();
        }

        /// <summary>
        /// Wird ausgelöst, wenn die Ansicht nach OK geschlossen werden soll
        /// </summary>
        public event EventHandler RequestCloseAfterOk;

        /// <summary>
        /// Wird ausgelöst, wenn die Ansicht nach Abbrechen geschlossen werden soll
        /// </summary>
        public event EventHandler RequestCloseAfterCancel;

        /// <summary>
        /// Liefert oder setzt den Text des Logeintrages
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { this.RaiseAndSetIfChanged(ref _memo, value); }
        }

        /// <summary>
        /// Liefert oder setzt das Datum des Logeintrags
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }

        private void OnRequestCloseAfterOk()
        {
            RequestCloseAfterOk?.Invoke(this, EventArgs.Empty);
        }

        private void OnRequestCloseAfterCancel()
        {
            RequestCloseAfterCancel?.Invoke(this, EventArgs.Empty);
        }
    }
}