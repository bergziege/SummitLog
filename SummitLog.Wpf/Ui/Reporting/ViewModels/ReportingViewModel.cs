using System.Threading.Tasks;

using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Reporting.ViewModels {
    /// <summary>
    ///     ViewModel der Reportingansicht
    /// </summary>
    public class ReportingViewModel : ViewModelBase, IReportingViewModel {
        /// <summary>
        ///     Lädt die Daten des View Models asynchron
        /// </summary>
        /// <returns></returns>
        public virtual async Task LoadData() {
        }
    }
}