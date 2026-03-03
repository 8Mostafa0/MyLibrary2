using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModalControl
{
    public class Modal : ContentControl, IModal
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(Modal), new PropertyMetadata(false));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        static Modal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(typeof(Modal)));
            BackgroundProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(CreateDefaultBackGround()));

        }

        private static object CreateDefaultBackGround()
        {
            return new SolidColorBrush(Colors.Black)
            {
                Opacity = 0.5
            };
        }
    }
}
