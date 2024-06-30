using System.Data.SQLite;

namespace Tournament_Tool.Participants
{
    public class ParticipantRepository
    {
        private const string ConnectionString = "Data Source=tournament.db";

        public List<Participant> GetAllParticipants()
        {
            var participants = new List<Participant>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Participants", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        participants.Add(new Participant
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Rating = 0
                        });
                    }
                }
            }
            return participants;
        }

        public void CreateParticipant(Participant participant)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Participants (Name, Rating) VALUES (@Name, @Rating)", connection);
                command.Parameters.AddWithValue("@Name", participant.Name);
                command.Parameters.AddWithValue("@Rating", participant.Rating);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateParticipant(Participant participant)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("UPDATE Participants SET Name = @Name, Rating = @Rating WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Name", participant.Name);
                command.Parameters.AddWithValue("@Rating", participant.Rating);
                command.Parameters.AddWithValue("@Id", participant.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteParticipant(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("DELETE FROM Participants WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
