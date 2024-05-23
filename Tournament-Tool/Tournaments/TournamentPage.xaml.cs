using Microsoft.Win32;
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
using Tournament_Tool.Participants;

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
            Loaded += TournamentPage_Loaded;
        }

        private void TournamentPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetupGrid();
        }

        private void SetupGrid()
        {
            if (DataContext is not TournamentViewModel viewModel) return;

            TournamentGrid.Children.Clear();
            TournamentGrid.RowDefinitions.Clear();
            TournamentGrid.ColumnDefinitions.Clear();

            var rounds = viewModel.Rounds.Count;
            var initialSlots = viewModel.Rounds[0].Count;


            var gridHeight = viewModel.TournamentType == TournamentType.RoundOf32 ? new GridLength(30) : new GridLength(1, GridUnitType.Star);
            var comboboxHeight = viewModel.TournamentType == TournamentType.RoundOf32 ? 28 : 30;
            var fontSize = viewModel.TournamentType == TournamentType.RoundOf32 ? 10 : 14;

            // Define columns
            for (int col = 0; col < rounds; col++)
            {
                TournamentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(240) });
            }

            // Define rows
            for (int row = 0; row < initialSlots; row++)
            {
                TournamentGrid.RowDefinitions.Add(new RowDefinition { Height = gridHeight });
            }

            // Add Comboboxes
            for (int round = 0; round < rounds; round++)
            {
                var roundSlots = viewModel.Rounds[round];
                var slots = roundSlots.Count;
                var rowSpan = initialSlots / slots;

                for (int slot = 0; slot < slots; slot++)
                {
                    var comboBox = new ComboBox
                    {
                        Width = 200,
                        Height = comboboxHeight,
                        FontSize = fontSize,
                        ItemsSource = viewModel.Participants,
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        DataContext = roundSlots[slot],
                        Margin = new Thickness(10),
                    };
                    BindingOperations.SetBinding(comboBox, ComboBox.SelectedItemProperty, new Binding("Participant"));

                    Grid.SetColumn(comboBox, round);
                    Grid.SetRow(comboBox, slot * rowSpan);
                    Grid.SetRowSpan(comboBox, rowSpan);

                    TournamentGrid.Children.Add(comboBox);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Save Tournament"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var viewModel = DataContext as TournamentViewModel;
                viewModel?.SaveTournament(saveFileDialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Load Tournament"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var tournament = TournamentViewModel.LoadTournament(openFileDialog.FileName);

                if (tournament == null)
                {
                    MainErrors.ShowTournamentLoadError();
                    return;
                }

                DataContext = tournament;
                SetupGrid();
            }
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not TournamentViewModel viewModel) return;

            ShuffleParticipants(viewModel);
        }

        private void ShuffleParticipants(TournamentViewModel viewModel)
        {
            var random = new Random();
            var availableParticipants = new List<Participant>(viewModel.Participants);

            foreach (var slot in viewModel.Rounds.FirstOrDefault())
            {
                if (availableParticipants.Count == 0) break;

                var participantIndex = random.Next(availableParticipants.Count);
                slot.Participant = availableParticipants[participantIndex];
                availableParticipants.RemoveAt(participantIndex);
            }
        }


        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToPage(new MenuPage());
        }
    }
}
