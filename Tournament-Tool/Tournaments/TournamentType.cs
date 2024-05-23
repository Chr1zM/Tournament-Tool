using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Tool.Tournaments
{
    public enum TournamentType
    {
        [Description("Sechzehntelfinale")]
        RoundOf32,
        [Description("Achtelfinale")]
        RoundOf16,
        [Description("Viertelfinale")]
        QuarterFinals,
        [Description("Halbfinale")]
        SemiFinals
    }

    public static class TournamentTypeExtensions
    {
        public static string GetDescription(this TournamentType tournamentType)
        {
            var type = typeof(TournamentType);
            var memInfo = type.GetMember(tournamentType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }

        public static TournamentType ValueOfString(string description)
        {
            var type = typeof(TournamentType);
            foreach (var name in Enum.GetNames(type))
            {
                var memInfo = type.GetMember(name);
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (((DescriptionAttribute)attributes[0]).Description == description)
                {
                    return (TournamentType)Enum.Parse(type, name);
                }
            }
            throw new ArgumentException("No TournamentType found for description", nameof(description));
        }
    }
}
