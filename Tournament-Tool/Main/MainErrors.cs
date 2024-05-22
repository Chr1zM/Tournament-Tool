using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tournament_Tool.Main
{
    public class MainErrors
    {
        private const string TournamentTypeEmptyError = "Fehler: Keine Turnierart ausgewählt";
        private const string TournamentTypeEmptyMessage = "Es muss eine Turnierart ausgewählt sein bevor man ein Turnier startet!";

        public static void ShowTournamentTypeEmptyError()
        {
            MessageBox.Show(TournamentTypeEmptyMessage, TournamentTypeEmptyError, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
