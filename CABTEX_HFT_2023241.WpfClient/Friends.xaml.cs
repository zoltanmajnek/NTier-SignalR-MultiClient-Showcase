using System.Windows;
using System.Windows.Controls;

namespace CABTEX_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for Friends.xaml
    /// </summary>
    public partial class Friends : Window
    {
        private int lastIndexbeforeRefresh = 0;
        public Friends()
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
