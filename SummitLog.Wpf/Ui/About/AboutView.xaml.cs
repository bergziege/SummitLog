using System.Windows;

namespace De.BerndNet2000.SummitLog.Ui.About {
    /// <summary>
    ///   Interaktionslogik für HelpView.xaml
    /// </summary>
    public partial class AboutView : Window {
        /// <summary>
        /// Erzeugt eine neue Instanz
        /// </summary>
        public AboutView() {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}