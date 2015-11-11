using System;
using ReactiveUI;
using SummitLog.UI.Common;

namespace SummitLog.UI.NameInput
{
    /// <summary>
    ///     Schnittstelle für View Models für die Ansicht zur Namenseingabe
    /// </summary>
    public interface INameInputViewModel: IReactiveObject
    {
        /// <summary>
        ///     Líefert oder setzt den Namen
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Liefert ein OK Command
        /// </summary>
        RelayCommand OkCommand { get; }

        /// <summary>
        ///     Liefert das Cancel Command
        /// </summary>
        RelayCommand CancelCommand { get; }

        /// <summary>
        ///     Wenn die Ansicht nach OK geschlossen werden soll
        /// </summary>
        event EventHandler RequestCloseAfterOk;

        /// <summary>
        ///     Wenn die Ansicht nach Cancel geschlossen werden soll
        /// </summary>
        event EventHandler RequestCloseAfterCancel;

        /// <summary>
        /// Weitere Kriterien, die zur Prüfung herangezogen werden ob das OK Command ausgeführt werden darf.
        /// </summary>
        /// <returns></returns>
        bool MoreCanOkCriterias();
    }
}