using System;
using ReactiveUI;
using SummitLog.UI.DifficultyLevelManagement;
using SummitLog.UI.DifficultyLevelManagement.DesignViewModels;
using SummitLog.UI.DifficultyLevelScaleManagement;
using SummitLog.UI.DifficultyLevelScaleManagement.DesignViewModels;

namespace SummitLog.UI.DifficultyManagement.DesignViewModels
{
    /// <summary>
    ///     Design View Model für eine Ansicht zur Verwaltung von Schwierigkeitsgraden und Skalen
    /// </summary>
    public class DifficultyManagementDesignViewModel: ReactiveObject, IDifficultyManagementViewModel
    {
        /// <summary>
        /// Liefert ein neues Design View Model
        /// </summary>
        public DifficultyManagementDesignViewModel()
        {
            DifficultyLevelManagementViewModel = new DifficultyLevelManagementDesignViewModel();
            DifficultyLevelScaleManagementViewModel = new DifficultyLevelScaleManagementDesignViewModel();
        }

        /// <summary>
        /// Receives events from the centralized event manager.
        /// </summary>
        /// <returns>
        /// true if the listener handled the event. It is considered an error by the <see cref="T:System.Windows.WeakEventManager"/> handling in WPF to register a listener for an event that the listener does not handle. Regardless, the method should return false if it receives an event that it does not recognize or handle.
        /// </returns>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager"/> calling this method.</param><param name="sender">Object that originated the event.</param><param name="e">Event data.</param>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            return true;
        }

        /// <summary>
        ///     Liefert ein View Model zur Schwierigkeitsgradverwaltung
        /// </summary>
        public IDifficultyLevelManagementViewModel DifficultyLevelManagementViewModel { get; }

        /// <summary>
        ///     Liefert ein View Model zu Schwierigkeitsgradskalenverwaltung
        /// </summary>
        public IDifficultyLevelScaleManagementViewModel DifficultyLevelScaleManagementViewModel { get; }
    }
}