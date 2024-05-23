using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Tool;
using Tournament_Tool.Participants;

namespace Tournament_Tool.Tournaments
{
    public class TournamentViewModel
    {
        private readonly ParticipantRepository _participantRepository;
        public ObservableCollection<Participant> Participants { get; set; }
        public ObservableCollection<ObservableCollection<ParticipantSlot>> Rounds { get; set; }
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
            Rounds = [];

            var numberOfRounds = TournamentType switch
            {
                TournamentType.RoundOf32 => 6,
                TournamentType.RoundOf16 => 5,
                TournamentType.QuarterFinals => 4,
                TournamentType.SemiFinals => 3,
                _ => 0
            };

            var initialSlots = TournamentType switch
            {
                TournamentType.RoundOf32 => 32,
                TournamentType.RoundOf16 => 16,
                TournamentType.QuarterFinals => 8,
                TournamentType.SemiFinals => 4,
                _ => 0
            };

            for (int round = 0; round < numberOfRounds; round++)
            {
                var roundSlots = new ObservableCollection<ParticipantSlot>();
                var slots = round == numberOfRounds - 1 ? 1 : initialSlots / (int)Math.Pow(2, round);
                for (int i = 0; i < slots; i++)
                {
                    roundSlots.Add(new ParticipantSlot());
                }
                Rounds.Add(roundSlots);
            }
        }

        public void SaveTournament(string filePath)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static TournamentViewModel? LoadTournament(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var tournament = JsonConvert.DeserializeObject<TournamentViewModel>(json);
            tournament?.UpdateParticipantReferences();
            return tournament;
        }

        private void UpdateParticipantReferences()
        {
            var participantDict = Participants.ToDictionary(p => p.Id);

            foreach (var round in Rounds)
            {
                foreach (var slot in round)
                {
                    if (slot.Participant != null && participantDict.TryGetValue(slot.Participant.Id, out var participant))
                    {
                        slot.Participant = participant;
                    }
                }
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
