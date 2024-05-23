﻿using System;
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


            var gridHeight = viewModel.TournamentType == TournamentType.RoundOf32 ? new GridLength(28) : new GridLength(1, GridUnitType.Star);
            var comboboxHeight = viewModel.TournamentType == TournamentType.RoundOf32 ? 25 : 30;

            // Define columns
            for (int col = 0; col < rounds; col++)
            {
                TournamentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
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
                        ItemsSource = viewModel.Participants,
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        DataContext = roundSlots[slot],
                        Margin = new Thickness(10)
                    };
                    BindingOperations.SetBinding(comboBox, ComboBox.SelectedItemProperty, new Binding("Participant"));

                    Grid.SetColumn(comboBox, round);
                    Grid.SetRow(comboBox, slot * rowSpan);
                    Grid.SetRowSpan(comboBox, rowSpan);

                    TournamentGrid.Children.Add(comboBox);
                }
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToPage(new MenuPage());
        }
    }
}
