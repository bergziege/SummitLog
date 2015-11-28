using System;
using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using DryIoc;
using SummitLog.UI.Main;

namespace SummitLog.UI.LogEntryInput.ViewCommands
{
    /// <summary>
    /// Command zur Anzeige von <see cref="LogEntryInputView"/>
    /// </summary>
    public class LogEntryInputViewCommand
    {
        /// <summary>
        /// Liefert den Memo Text
        /// </summary>
        public string Memo { get; private set; }
        /// <summary>
        /// Liefert das Datum
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Führt das Command aus
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            LogEntryInputView view = AppContext.Container.Resolve<LogEntryInputView>();
            ILogEntryInputViewModel vm = AppContext.Container.Resolve<ILogEntryInputViewModel>();
            view.DataContext = vm;

            bool closedAfterOk = false;
            vm.RequestCloseAfterOk += delegate
            {
                Memo = vm.Memo;
                Date = vm.Date;
                closedAfterOk = true;
                view.Close();
            };
            vm.RequestCloseAfterCancel += delegate
            {
                view.Close();
            };

            view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return closedAfterOk;
        }
    }
}