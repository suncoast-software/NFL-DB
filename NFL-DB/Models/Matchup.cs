using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Models
{
    public class Matchup
    {
        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public string AwayScore { get; set; }
        public string HomeScore { get; set; }
        public string[] awayQuarterScores { get; set; }
        public string[] homeQuarterScores { get; set; }

        public Matchup()
        {
        }

        public Matchup(Team awayTeam, Team homeTeam, string awayScore, string homeScore, string[] awayQuarterScores, string[] homeQuarterScores)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            AwayScore = awayScore;
            HomeScore = homeScore;
            this.awayQuarterScores = awayQuarterScores;
            this.homeQuarterScores = homeQuarterScores;
        }

    }
}
