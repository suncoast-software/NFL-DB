using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using NFL_DB.Models;

namespace NFL_DB.Helpers
{
    public static class DataHelper
    {
        //https://www.footballdb.com/scores/index.html?lg=NFL&yr=2018&type=reg&wk=1

        public static DataClasses_PlayerDataDataContext playersDB = new DataClasses_PlayerDataDataContext();

        /// <summary>
        /// Get Matchups for the Desired Week
        /// </summary>
        /// <param name="week"></param>
        /// <returns>List</returns>
        #region WEEK MATCHUPS

        public static List<Team> Get_Matchups(string week)
        {
            List<Team> matchups = new List<Team>();
            HtmlWeb page = new HtmlWeb();
            HtmlDocument doc = page.Load("https://www.footballdb.com/scores/index.html?lg=NFL&yr=2018&type=reg&wk=1");

            HtmlNodeCollection teamNodes = doc.DocumentNode.SelectNodes("//table//tr");

            foreach (HtmlNode node in teamNodes)
            {
                string homeTeam = teamNodes[0].InnerText;
                string awayTeam = teamNodes[2].InnerText;
                string test = "";
            }
            
            

            return matchups;
        }
        #endregion

        /// <summary>
        /// Get Stats For Each Player in the League all time.
        /// </summary>
        /// <returns>List</returns>
        #region PLAYER STATS
        public static List<Player> Get_Current_Player_Stats()
        {
            string lastNameLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<Player> players = new List<Player>();
            
            for (int i = 0; i < lastNameLetters.Length; i++)
            {
                string currentLetter = lastNameLetters.Substring(i, 1);
                string mainPage = "https://www.footballdb.com/players/current.html?letter=" + currentLetter;

                HtmlWeb page = new HtmlWeb();
                HtmlDocument doc = page.Load(mainPage);
               
                HtmlNodeCollection playersTbl = doc.DocumentNode.SelectNodes("//div[@class='divtable divtable-striped']//div[@class='tr']");

                foreach (HtmlNode node in playersTbl)
                {
                    string playerName = node.ChildNodes[0].InnerText;
                    string pos = node.ChildNodes[1].InnerText;
                    string team = node.ChildNodes[2].InnerText;
                    string college = node.ChildNodes[3].InnerText;

                    players.Add(new Player(playerName, pos, team, college));
                    try
                    {
                        playersDB.insertPlayer(playerName, pos, team, college);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("an Error occured", "INSERT ERROR\r\n" + ex.Message, MessageBoxButton.OK, MessageBoxImage.Information);
                        continue;
                    }
                    string test = "";
                }
 
            }

            return players;
        }
        #endregion

        /// <summary>
        /// Get Weeks Matchups and Scores
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="season"></param>
        /// <returns>List</returns>
        #region GET SCORES

        public static List<Matchup> Get_Week_Scores(string year, string week, SeasonType season)
        {
            string seasonType = "";

            switch (season)
            {
                case SeasonType.PRE:
                    seasonType = "pre";
                    break;
                case SeasonType.POST:
                    seasonType = "post";
                    break;
                case SeasonType.REG:
                    seasonType = "reg";
                    break;
            }

            string url = "https://www.footballdb.com/scores/index.html?lg=NFL&yr=" + year + "&type=" + seasonType + "&wk=" + week;
            List<Matchup> matchups = new List<Matchup>();

            HtmlWeb page = new HtmlWeb();
            HtmlDocument doc = page.Load(url);

            HtmlNodeCollection teamNodes = doc.DocumentNode.SelectNodes("//table//tbody//tr");

            for (int i = 0; i < teamNodes.Count - 1; i++)
            {
               
                //away team data
                string awayTeamName = teamNodes[i].ChildNodes[1].InnerText;
                int awayIndex = awayTeamName.LastIndexOf('(');
                string awayRecord = awayTeamName.Substring(awayIndex);
                string awayTeamNameFinal = awayTeamName.Replace(awayRecord, "").Trim();
                string aqOneScore = teamNodes[i].ChildNodes[3].InnerText;
                string aqTwoScore = teamNodes[i].ChildNodes[4].InnerText;
                string aqThreeScore = teamNodes[i].ChildNodes[5].InnerText;
                string aqFourScore = teamNodes[i].ChildNodes[6].InnerText;
                string aFinalScore = teamNodes[i].ChildNodes[7].InnerText;
                string[] awayQuarterScores = new string[] { aqOneScore, aqTwoScore, aqThreeScore, aqFourScore };

                //home team data
                string homeTeamName = teamNodes[i + 1].ChildNodes[1].InnerText;
                int homeIndex = homeTeamName.LastIndexOf('(');
                string homeRecord = homeTeamName.Substring(homeIndex);
                string homeTeamNameFinal = homeTeamName.Replace(homeRecord, "").Trim();
                string hqOneScore = teamNodes[i + 1].ChildNodes[3].InnerText;
                string hqTwoScore = teamNodes[i + 1].ChildNodes[4].InnerText;
                string hqThreeScore = teamNodes[i + 1].ChildNodes[5].InnerText;
                string hqFourScore = teamNodes[i + 1].ChildNodes[6].InnerText;
                string hFinalScore = teamNodes[i + 1].ChildNodes[7].InnerText;
                string[] homeQuarterScores = new string[] { hqOneScore, hqTwoScore, hqThreeScore, hqFourScore };

                //add data to list
                matchups.Add(new Matchup(new Team(awayTeamNameFinal, awayRecord), new Team(homeTeamNameFinal, homeRecord), aFinalScore, hFinalScore, awayQuarterScores, homeQuarterScores));
                

            }

            return matchups;
        }

        #endregion

        /// <summary>
        /// Send Message to Form
        /// </summary>
        /// <param name="type"></param>
        /// <returns>string</returns>
        #region MESSAGE
        public static string Message(MessageType type)
        {
            string message = "";

            switch (type)
            {
                case MessageType.success:
                    message = "Success"; 
                    break;
                case MessageType.failure:
                    message = "Failure";
                    break;
                case MessageType.info:
                    message = "Info";
                    break;
                case MessageType.warning:
                    message = "Warning";
                    break;
            }

            return message;
        }
        #endregion


    }
}
