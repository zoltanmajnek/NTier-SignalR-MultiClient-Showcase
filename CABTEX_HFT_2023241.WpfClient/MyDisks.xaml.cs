using System.Windows;
using System.Windows.Controls;

namespace CABTEX_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for MyDisks.xaml
    /// </summary>
    public partial class MyDisks : Window
    {
        private int lastIndexbeforeRefresh = 0;
        public MyDisks()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var myBox = sender as ListBox;
            if (myBox != null)
            {
                if (myBox.SelectedIndex == -1)
                {
                    myBox.SelectedIndex = lastIndexbeforeRefresh;
                }
                else lastIndexbeforeRefresh = myBox.SelectedIndex;
            }
        }
    }
}
