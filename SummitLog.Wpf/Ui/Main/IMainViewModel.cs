﻿using Com.QueoFlow.Commons.MVVM.Commands;
using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main {
    /// <summary>
    ///     Interface für ein View Model des Hauptfensters.
    /// </summary>
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Command um die Anwendung zu beenden.
        /// </summary>
        RelayCommand ApplicationExitCommand { get; }

        /// <summary>
        ///     Liefert oder setzt das aktuelle View Model
        /// </summary>
        IPageViewModel CurrentViewModel { get; set; }

        /// <summary>
        ///     Liefert das Command um die Einstellungen anzuzeigen
        /// </summary>
        RelayCommand ShowSettingsCommand { get; }
    }
}