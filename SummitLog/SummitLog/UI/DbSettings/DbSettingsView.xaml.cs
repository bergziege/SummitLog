using System.Windows;

namespace SummitLog.UI.DbSettings
{
    /// <summary>
    ///     Interaktionslogik für DbSettingsView.xaml
    /// </summary>
    public partial class DbSettingsView : Window, IDbSettingsView
    {
        /// <summary>
        ///     Liefert eine neue Instanz der View
        /// </summary>
        public DbSettingsView()
        {
            InitializeComponent();
            this.Loaded += DbSettingsView_Loaded;
        }

        private void DbSettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner == null)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Activate();
            }
        }
    }
}