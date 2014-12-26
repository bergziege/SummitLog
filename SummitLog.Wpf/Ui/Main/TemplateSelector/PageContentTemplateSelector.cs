using System.Windows;
using System.Windows.Controls;

using De.BerndNet2000.SummitLog.Wpf.Ui.Settings;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.Main.TemplateSelector {
    /// <summary>
    ///     Data Template Selector für die Seiten der Hauptansicht
    /// </summary>
    public class PageContentTemplateSelector : DataTemplateSelector {
        /// <summary>
        ///     Liefert ode rsetzt das Tempalte für die EInstellungsseite
        /// </summary>
        public DataTemplate SettingsPageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            //return base.SelectTemplate(item, container);

            if (item is ISettingsViewModel) {
                return SettingsPageTemplate;
            }

            return null;
        }
    }
}