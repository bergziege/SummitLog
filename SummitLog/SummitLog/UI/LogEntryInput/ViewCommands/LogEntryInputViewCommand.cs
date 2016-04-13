using System;
using Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils;
using Microsoft.Practices.Unity;
using SummitLog.UI.Common;
using SummitLog.UI.Main;

namespace SummitLog.UI.LogEntryInput.ViewCommands
{
    /// <summary>
    /// Command zur Anzeige von <see cref="LogEntryInputView"/>
    /// </summary>
    public class LogEntryInputViewCommand: ViewCommandBase
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
            LogEntryInputView view = GenericFactory.Resolve<LogEntryInputView>();
            ILogEntryInputViewModel vm = GenericFactory.Resolve<ILogEntryInputViewModel>();
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

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return closedAfterOk;
        }

        /// <summary>
        /// Führt das Commando mit den übergebenen Daten aus.
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool Execute(string memo, DateTime date)
        {

            Memo = memo;
            Date = date;

            LogEntryInputView view = GenericFactory.Resolve<LogEntryInputView>();
            ILogEntryInputViewModel vm = GenericFactory.Resolve<ILogEntryInputViewModel>();
            view.DataContext = vm;

            vm.Memo = memo;
            vm.Date = date;

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

            view.Owner = WindowParentHelper.GetWindowBySpecificType(typeof(MainView));

            view.ShowDialog();

            return closedAfterOk;
        }
    }
}