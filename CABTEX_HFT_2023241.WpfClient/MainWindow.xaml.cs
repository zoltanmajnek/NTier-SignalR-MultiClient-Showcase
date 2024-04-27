using System.Windows;
using System.Windows.Controls;

namespace CABTEX_HFT_2023241.WpfClient
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

        private void OpenMyDisks(object sender, RoutedEventArgs e)
        {
            var DiskWindow = new MyDisks();
            DiskWindow.Show();
        }

        private void OpenFriends(object sender, RoutedEventArgs e)
        {
            var FriendWindow = new Friends();
            FriendWindow.Show();

        }
    }
}
