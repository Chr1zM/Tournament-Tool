using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Tournament_Tool.Participants;
using Tournament_Tool.Tournaments;

namespace Tournament_Tool.Main
{
    /// <summary>
    /// Interaktionslogik für MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public ObservableCollection<string> TournamentTypeStrings { get; set; }
        public string? SelectedTournamentTypeString { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            DataContext = this;
            TournamentTypeStrings = new ObservableCollection<string>
            {
                TournamentType.RoundOf32.GetDescription(),
                TournamentType.RoundOf16.GetDescription(),
                TournamentType.QuarterFinals.GetDescription(),
                TournamentType.SemiFinals.GetDescription()
            };
        }

        private void TournamentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTournamentTypeString == null)
            {
                MainErrors.ShowTournamentTypeEmptyError();
                return;
            }
            var selectedType = TournamentTypeExtensions.ValueOfString(SelectedTournamentTypeString);
            ((MainWindow)Application.Current.MainWindow).NavigateToPage(new TournamentPage(selectedType));
        }

        private void EditParticipantsButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToPage(new ParticipantPage());
        }
    }
}
