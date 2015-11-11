using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.LogEntryInput.DesignViewModels
{
    /// <summary>
    /// Design View Model einer Ansicht zum Eingeben von Informationen zu einem Logeintrag
    /// </summary>
    public class LogEntryInputDesignViewModel : ReactiveObject, ILogEntryInputViewModel
    {
        /// <summary>
        /// Liefert eine neue Instanz des Design View Models
        /// </summary>
        public LogEntryInputDesignViewModel()
        {
            Memo = "Ein etwas längerer Text" + Environment.NewLine + "mit einem Zeilenumbruch";
            Date = DateTime.Now;
        }

        /// <summary>
        /// Liefert das OK Command
        /// </summary>
        public RelayCommand OkCommand { get; }

        /// <summary>
        /// Liefert das Abbrechen kommand
        /// </summary>
        public RelayCommand CancelCommand { get; }

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
        public string Memo { get; set; }

        /// <summary>
        /// Liefert oder setzt das Datum des Logeintrags
        /// </summary>
        public DateTime Date { get; set; }
    }
}