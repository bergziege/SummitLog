using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SummitLog.UI.Main
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private IMainViewModel GetViewModel()
        {
            if (DataContext is IMainViewModel)
            {
                return (IMainViewModel) DataContext;
            }
            return null;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IMainViewModel vm = GetViewModel();
            if (vm != null && vm.EditSelectedCountryCommand.CanExecute(null))
            {
                vm.EditSelectedCountryCommand.Execute(null);
            }
        }
    }
}
