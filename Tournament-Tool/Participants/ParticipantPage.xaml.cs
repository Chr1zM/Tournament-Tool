using System.Windows;
using System.Windows.Controls;
using Tournament_Tool.Main;

namespace Tournament_Tool.Participants
{
    /// <summary>
    /// Interaktionslogik für ParticipantPage.xaml
    /// </summary>
    public partial class ParticipantPage : Page
    {
        public ParticipantPage()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).NavigateToPage(new MenuPage());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ParticipantViewModel)DataContext;
            viewModel.AddNewParticipant();
        }

        private void SaveParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            var participant = (Participant)((Button)sender).DataContext;
            var viewModel = (ParticipantViewModel)DataContext;
            viewModel.ChangeParticipantName(participant);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var participant = (Participant)((Button)sender).DataContext;
            var viewModel = (ParticipantViewModel)DataContext;
            viewModel.DeleteParticipant(participant.Id);
        }
    }
}
