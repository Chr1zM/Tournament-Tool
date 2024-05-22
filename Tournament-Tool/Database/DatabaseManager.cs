using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Tool.Database
{
    public class DatabaseManager
    {
        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection("Data Source=tournament.db"))
            {
                connection.Open();
                var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Participants (Id INTEGER PRIMARY KEY, Name TEXT UNIQUE)", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
