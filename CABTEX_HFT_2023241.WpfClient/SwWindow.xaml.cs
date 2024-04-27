using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for SwWindow.xaml
    /// </summary>
    public partial class SwWindow : Window
    {
        private int lastIndexbeforeRefresh = 0;
        public SwWindow()
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

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    if (e.Key != Key.Back) e.Handled = true;
                }
            }
        }
    }
}
