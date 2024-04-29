using System.Windows;
using System.Windows.Controls;

namespace CABTEX_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for DirWindow.xaml
    /// </summary>
    public partial class DirWindow : Window
    {
        public DirWindow()
        {
            InitializeComponent();
        }
        private int lastDiskIndex = 0;
        private int lastSwIndex = 0;

        private void DiskSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myBox = sender as ListBox;
            if (myBox != null)
            {
                if (myBox.SelectedIndex == -1)
                {
                    myBox.SelectedIndex = lastDiskIndex;
                }
                else lastDiskIndex = myBox.SelectedIndex;
            }
        }

        private void SwSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myBox = sender as ListBox;
            if (myBox != null)
            {
                if (myBox.SelectedIndex == -1)
                {
                    myBox.SelectedIndex = lastSwIndex;
                }
                else lastSwIndex = myBox.SelectedIndex;
            }
        }
    }
}
