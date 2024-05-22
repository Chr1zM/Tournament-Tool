using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tournament_Tool.Participants
{
    public class ParticipantErrors
    {
        private const string NameAlreadyExistsError = "Fehler: Doppelte Teilnehmer";
        private const string NameAlreadyExistsMessage = "Es gibt schon einen Teilnehmer mit diesem Namen!\nNamen können nicht doppelt vorkommen.";

        private const string NameIsEmptyError = "Fehler: Leerer Name";
        private const string NameIsEmptyMessage = "Der Name darf nicht leer sein!";

        public static void ShowNameAlreadyExistsError()
        {
            MessageBox.Show(NameAlreadyExistsMessage, NameAlreadyExistsError, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowNameIsEmptyError()
        {
            MessageBox.Show(NameIsEmptyMessage, NameIsEmptyError, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
