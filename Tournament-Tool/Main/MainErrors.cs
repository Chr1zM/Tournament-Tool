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

        private const string TournamentLoadError = "Fehler: Turnier konnte nicht geladen werden";
        private const string TournamentLoadMessage = "Das Turnier konnte nicht geladen werden. Die Datei ist im falschen Format.";

        public static void ShowTournamentTypeEmptyError()
        {
            MessageBox.Show(TournamentTypeEmptyMessage, TournamentTypeEmptyError, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowTournamentLoadError()
        {
            MessageBox.Show(TournamentLoadMessage, TournamentLoadError, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
