using System.Windows;

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

        private void OpenDiskTypeWindow(object sender, RoutedEventArgs e)
        {
            var Window = new TypesWindow();
            Window.Show();
        }

        private void DirectoriesInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DiskDirectory: Ez csak egy kapcsolótábla, próbálja a többi funkciót!");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(MessageBox.Show("Biztosan kilép?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) e.Cancel = true;
        }

        private void OpenSwWindow(object sender, RoutedEventArgs e)
        {
            var Window = new SwWindow();
            Window.Show();

        }
    }
}
