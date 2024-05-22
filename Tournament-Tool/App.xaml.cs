using System.Configuration;
using System.Data;
using System.Windows;
using Tournament_Tool.Database;

namespace Tournament_Tool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DatabaseManager.InitializeDatabase();
        }
    }
}
