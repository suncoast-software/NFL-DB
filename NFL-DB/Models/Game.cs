using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Models
{
    public class Game
    {
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayFinalScore { get; set; }
        public string HomeFinalScore { get; set; }
        public string AwayTeamRecord { get; set; }
        public string HomeTeamRecord { get; set; }

        public string AQ1 { get; set; }
        public string AQ2 { get; set; }
        public string AQ3 { get; set; }
        public string AQ4 { get; set; }
       

        public string HQ1 { get; set; }
        public string HQ2 { get; set; }
        public string HQ3 { get; set; }
        public string HQ4 { get; set; }
  

        public string Year { get; set; }
        public string Week { get; set; }

        public Game()
        {
        }

        public Game(string awayTeamName, string homeTeamName, string awayFinalScore, string homeFinalScore, 
                    string awayTeamRecord, string homeTeamRecord, string aQ1, string aQ2, string aQ3, string aQ4, 
                    string hQ1, string hQ2, string hQ3, string hQ4, string year, string week)
        {
            AwayTeamName = awayTeamName;
            HomeTeamName = homeTeamName;
            AwayFinalScore = awayFinalScore;
            HomeFinalScore = homeFinalScore;
            AwayTeamRecord = awayTeamRecord;
            HomeTeamRecord = homeTeamRecord;
            AQ1 = aQ1;
            AQ2 = aQ2;
            AQ3 = aQ3;
            AQ4 = aQ4;
            HQ1 = hQ1;
            HQ2 = hQ2;
            HQ3 = hQ3;
            HQ4 = hQ4;
            Year = year;
            Week = week;
        }
    }
}
