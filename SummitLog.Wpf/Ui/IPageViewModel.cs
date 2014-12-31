using System.Threading.Tasks;

namespace De.BerndNet2000.SummitLog.Wpf.Ui {
    /// <summary>
    ///     Schnittstelle für View Models für Seiten der Hauptanwendung
    /// </summary>
    public interface IPageViewModel {
        /// <summary>
        ///     Lädt die Daten des View Models asynchron
        /// </summary>
        /// <returns></returns>
        Task LoadData();
    }
}