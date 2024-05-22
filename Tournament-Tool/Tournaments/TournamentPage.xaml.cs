using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tournament_Tool.Main;

namespace Tournament_Tool.Tournaments
{
    /// <summary>
    /// Interaktionslogik für TournamentPage.xaml
    /// </summary>
    public partial class TournamentPage : Page
    {
        public TournamentPage(TournamentType tournamentType)
        {
            InitializeComponent();
            DataContext = new TournamentViewModel(tournamentType);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToPage(new MenuPage());
        }
    }
}
