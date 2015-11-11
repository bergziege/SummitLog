using System;
using System.ComponentModel;
using ReactiveUI;
using SummitLog.UI.DifficultyLevelManagement;
using SummitLog.UI.DifficultyLevelScaleManagement;

namespace SummitLog.UI.DifficultyManagement.ViewModels
{
    /// <summary>
    /// View Model für eine Ansicht zur Verwaltung von Schwierigkeitsgradskalen und Levels
    /// </summary>
    public class DifficultyManagementViewModel : ReactiveObject, IDifficultyManagementViewModel
    {
        /// <summary>
        /// Liefert eine neue Instanz des View Models
        /// </summary>
        /// <param name="difficultyLevelManagementViewModel"></param>
        /// <param name="difficultyLevelScaleManagementViewModel"></param>
        public DifficultyManagementViewModel(IDifficultyLevelManagementViewModel difficultyLevelManagementViewModel, IDifficultyLevelScaleManagementViewModel difficultyLevelScaleManagementViewModel)
        {
            DifficultyLevelManagementViewModel = difficultyLevelManagementViewModel;
            DifficultyLevelScaleManagementViewModel = difficultyLevelScaleManagementViewModel;
            PropertyChangedEventManager.AddListener(DifficultyLevelScaleManagementViewModel, this, "SelectedDifficultyLevelScale");
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
            if (DifficultyLevelScaleManagementViewModel.SelectedDifficultyLevelScale != null)
            {
                DifficultyLevelManagementViewModel.LoadData(DifficultyLevelScaleManagementViewModel.SelectedDifficultyLevelScale);
            }
            return true;
        }

        /// <summary>
        ///     Liefert ein View Model zur Schwierigkeitsgradverwaltung
        /// </summary>
        public IDifficultyLevelManagementViewModel DifficultyLevelManagementViewModel { get; private set; }

        /// <summary>
        ///     Liefert ein View Model zu Schwierigkeitsgradskalenverwaltung
        /// </summary>
        public IDifficultyLevelScaleManagementViewModel DifficultyLevelScaleManagementViewModel { get; private set; }

        /// <summary>
        /// Lädt die VM relevanten Daten
        /// </summary>
        public void LoadData()
        {
            DifficultyLevelScaleManagementViewModel.LoadData();
        }
    }
}