using System.Windows;

namespace MyLibrary.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnShowModalClick(object sender, RoutedEventArgs e)
        {
            modal.IsOpen = true;
        }
        private void OnCloseModalClick(object sender, RoutedEventArgs e)
        {
            modal.IsOpen = false;
        }
    }
}
