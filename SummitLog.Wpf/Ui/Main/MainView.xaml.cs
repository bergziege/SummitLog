using System.ComponentModel;

using MahApps.Metro.Controls;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main {
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : MetroWindow {
        /// <summary>
        /// </summary>
        public MainView() {
            InitializeComponent();
        }

        private void WindowClosing(object sender, CancelEventArgs e) {
            if (DataContext is IMainViewModel) {
                IMainViewModel vm = DataContext as IMainViewModel;
                vm.ApplicationExitCommand.Execute(null);
            }
        }
    }
}