using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.NameInput.ViewModels
{
    /// <summary>
    ///     View Model einer Ansicht zur Namenseingabe
    /// </summary>
    public class NameInputViewModel : ReactiveObject, INameInputViewModel
    {
        private RelayCommand _cancelCommand;
        private RelayCommand _okCommand;
        private string _name;

        /// <summary>
        ///     Líefert oder setzt den Namen
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        /// <summary>
        ///     Liefert ein OK Command
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

        /// <summary>
        ///     Liefert das Cancel Command
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
        ///     Wenn die Ansicht nach OK geschlossen werden soll
        /// </summary>
        public event EventHandler RequestCloseAfterOk;

        /// <summary>
        ///     Wenn die Ansicht nach Cancel geschlossen werden soll
        /// </summary>
        public event EventHandler RequestCloseAfterCancel;

        private bool CanOk()
        {
            return !string.IsNullOrWhiteSpace(Name) && MoreCanOkCriterias();
        }

        /// <summary>
        /// Weitere Kriterien, die zur Prüfung herangezogen werden ob das OK Command ausgeführt werden darf.
        /// </summary>
        /// <returns></returns>
        public virtual bool MoreCanOkCriterias()
        {
            return true;
        }

        private void Ok()
        {
            OnRequestCloseAfterOk();
        }

        private bool CanCancel()
        {
            return true;
        }

        private void Cancel()
        {
            OnRequestCloseAfterCancel();
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