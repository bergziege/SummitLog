using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.LogEntryInput
{
    /// <summary>
    /// Schnittstelle für View Models einer Ansicht zur EIngabe von Informationen zu einem Logeintrag
    /// </summary>
    public interface ILogEntryInputViewModel : IReactiveObject
    {
        /// <summary>
        /// Liefert das OK Command
        /// </summary>
        RelayCommand OkCommand { get; }

        /// <summary>
        /// Liefert das Abbrechen kommand
        /// </summary>
        RelayCommand CancelCommand { get; }

        /// <summary>
        /// Wird ausgelöst, wenn die Ansicht nach OK geschlossen werden soll
        /// </summary>
        event EventHandler RequestCloseAfterOk;

        /// <summary>
        /// Wird ausgelöst, wenn die Ansicht nach Abbrechen geschlossen werden soll
        /// </summary>
        event EventHandler RequestCloseAfterCancel;

        /// <summary>
        /// Liefert oder setzt den Text des Logeintrages
        /// </summary>
        string Memo { get; set; }

        /// <summary>
        /// Liefert oder setzt das Datum des Logeintrags
        /// </summary>
        DateTime Date { get; set; }

    }
}