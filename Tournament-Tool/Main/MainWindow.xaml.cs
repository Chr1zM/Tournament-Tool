using System.Windows;
using System.Windows.Controls;

namespace Tournament_Tool.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MenuPage());
        }

        public void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
        }
    }
}