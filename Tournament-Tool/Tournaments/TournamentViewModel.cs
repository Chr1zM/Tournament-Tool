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
            // TODO: Implement
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
