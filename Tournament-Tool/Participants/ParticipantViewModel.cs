using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Media;

namespace Tournament_Tool.Participants
{
    public class ParticipantViewModel : BaseViewModel
    {
        private ParticipantRepository _repository;

        private ObservableCollection<Participant> _participants;
        public ObservableCollection<Participant> Participants
        {
            get => _participants;
            set { _participants = value; OnPropertyChanged(); }
        }

        private string _newParticipantName;
        public string NewParticipantName
        {
            get => _newParticipantName;
            set { _newParticipantName = value; OnPropertyChanged(); }
        }
        private int _newParticipantRating;
        public int NewParticipantRating
        {
            get => _newParticipantRating;
            set { _newParticipantRating = value; OnPropertyChanged(); }
        }

        public ParticipantViewModel()
        {
            _repository = new ParticipantRepository();
            Participants = [];
            LoadParticipants();
        }

        private void LoadParticipants()
        {
            using (var connection = new SQLiteConnection("Data Source=tournament.db"))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Participants", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Participants.Add(new Participant
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Rating = reader.GetInt32(2)
                        });
                    }
                }
            }
        }

        public void AddNewParticipant()
        {
            if (string.IsNullOrWhiteSpace(NewParticipantName))
            {
                ParticipantErrors.ShowNameIsEmptyError();
                return;
            }

            try
            {
                var newParticipant = new Participant
                {
                    Name = NewParticipantName,
                    Rating = NewParticipantRating
                };
                _repository.CreateParticipant(newParticipant);
                Participants.Add(newParticipant);
                NewParticipantName = string.Empty;
            }
            catch (SQLiteException e)
            {
                ParticipantErrors.ShowNameAlreadyExistsError();
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        public void UpdateParticipant(Participant participant)
        {
            if (string.IsNullOrWhiteSpace(participant.Name))
            {
                ParticipantErrors.ShowNameIsEmptyError();
                return;
            }

            try
            {
                _repository.UpdateParticipant(participant);
            }
            catch (SQLiteException e)
            {
                ParticipantErrors.ShowNameAlreadyExistsError();
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        public void DeleteParticipant(int id)
        {
            _repository.DeleteParticipant(id);
            Participants.Remove(Participants.First(p => p.Id == id));
        }
    }
}
