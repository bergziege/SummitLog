using System.Windows;

namespace SummitLog.UI.Common
{
    /// <summary>
    ///     Schnittstelle für Fenster
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        ///     Liefert oder setzt den DataContext
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        ///     Liefert oder setzt den Owner
        /// </summary>
        Window Owner { get; set; }

        /// <summary>
        ///     Zeigt das Fenster modal
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();

        /// <summary>
        ///     Schließt das Fenster
        /// </summary>
        void Close();
    }
}