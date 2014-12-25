using De.BerndNet2000.SummitLog.Wpf.Ui.About.ViewCommand;

namespace De.BerndNet2000.SummitLog.Wpf.Factories {
    /// <summary>
    ///     Library für View Commands.
    /// </summary>
    public static class ViewCommandLibrary {
        
        /// <summary>
        /// Liefert das Command um die AboutView zu zeigen.
        /// </summary>
        public static readonly  AboutViewCommand ShowAboutView = new AboutViewCommand();
    }
}