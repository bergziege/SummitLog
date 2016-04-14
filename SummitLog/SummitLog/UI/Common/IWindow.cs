using System.Windows;
using Xceed.Wpf.DataGrid;

namespace SummitLog.UI.Common
{
    public interface IWindow
    {
        bool? ShowDialog();
        object DataContext { get; set; }
        void Close();
        Window Owner { get; set; }
    }
}