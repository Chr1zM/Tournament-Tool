using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Tool.Participants;

namespace Tournament_Tool.Tournaments
{
    public class TournamentViewModel
    {
        private readonly ParticipantRepository _participantRepository;
        public ObservableCollection<Participant> Participants { get; set; }
        public ObservableCollection<ParticipantSlot> ParticipantSlots { get; set; }
        public int GridColumns { get; set; }
        public TournamentType TournamentType { get; }

        public TournamentViewModel(TournamentType tournamentType)
        {
            _participantRepository = new ParticipantRepository();
            Participants = new ObservableCollection<Participant>(_participantRepository.GetAllParticipants());
            TournamentType = tournamentType;
            SetupParticipantSlots();
        }

        private void SetupParticipantSlots()
        {
            ParticipantSlots = [];
            var numberOfSlots = TournamentType switch
            {
                TournamentType.RoundOf32 => 32,
                TournamentType.RoundOf16 => 16,
                TournamentType.QuarterFinals => 8,
                TournamentType.SemiFinals => 4,
                _ => 0
            };

            GridColumns = TournamentType switch
            {
                TournamentType.RoundOf32 => 4,
                TournamentType.RoundOf16 => 4,
                TournamentType.QuarterFinals => 2,
                TournamentType.SemiFinals => 2,
                _ => 1
            };

            for (int i = 0; i < numberOfSlots; i++)
            {
                ParticipantSlots.Add(new ParticipantSlot());
            }
        }
    }

    public class ParticipantSlot : BaseViewModel
    {
        private Participant _participant;
        public Participant Participant
        {
            get => _participant;
            set { _participant = value; OnPropertyChanged(); }
        }
    }
}
