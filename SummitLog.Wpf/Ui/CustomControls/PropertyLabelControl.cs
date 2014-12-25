using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace De.BerndNet2000.SummitLog.Wpf.Ui.CustomControls {
    /// <summary>
    ///     Label Control zur Darstellung eines Proprties.
    /// </summary>
    public class PropertyLabelControl : Control {
        /// <summary>
        ///     Property Name.
        /// </summary>
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName",
                typeof(string),
                typeof(PropertyLabelControl),
                new FrameworkPropertyMetadata("Property Name",
                        FrameworkPropertyMetadataOptions.AffectsRender
                        | FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        /// <summary>
        ///     Anzuzeigender Wert.
        /// </summary>
        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register("PropertyValue",
                typeof(string),
                typeof(PropertyLabelControl),
                new FrameworkPropertyMetadata("Property Value",
                        FrameworkPropertyMetadataOptions.AffectsRender
                        | FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        /// <summary>
        ///     Text Trim.
        /// </summary>
        public static readonly DependencyProperty TextTrimmingProperty =
                DependencyProperty.Register("TextTrimming",
                        typeof(TextTrimming),
                        typeof(PropertyLabelControl),
                        new PropertyMetadata(TextTrimming.None));
        /// <summary>
        ///     Text Wrap.
        /// </summary>
        public static readonly DependencyProperty TextWrappingProperty =
                DependencyProperty.Register("TextWrapping",
                        typeof(TextWrapping),
                        typeof(PropertyLabelControl),
                        new PropertyMetadata(TextWrapping.Wrap));

        static PropertyLabelControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyLabelControl),
                    new FrameworkPropertyMetadata(typeof(PropertyLabelControl)));
        }

        /// <summary>
        ///     Anzuzeigender Name.
        /// </summary>
        [Category("Anzeige")]
        public string PropertyName {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        /// <summary>
        ///     Anzuzeigender Wert.
        /// </summary>
        [Category("Anzeige")]
        public string PropertyValue {
            get { return (string)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }
        /// <summary>
        ///     Text Trim.
        /// </summary>
        public TextTrimming TextTrimming {
            get { return (TextTrimming)GetValue(TextTrimmingProperty); }
            set { SetValue(TextTrimmingProperty, value); }
        }
        /// <summary>
        ///     Text Wrap.
        /// </summary>
        public TextWrapping TextWrapping {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }
    }
}