using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using HtmlAgilityPack;
using NFL_DB.Models;

namespace NFL_DB.Helpers
{
    public static class DataHelper
    {
        //https://www.footballdb.com/scores/index.html?lg=NFL&yr=2018&type=reg&wk=1

        public static DataClasses_PlayerDataDataContext playersDB = new DataClasses_PlayerDataDataContext();

        /// <summary>
        /// Get Each Players - Name, Team, POS and College
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
                    //string test = "";
                }
 
            }

            return players;
        }
        #endregion

        /// <summary>
        /// Get Player Links
        /// </summary>
        /// <returns>Dictionary</returns>
        #region GET PLAYER LINKS
        public static Dictionary<string, string> Get_Player_Links()
        {
            Dictionary<string, string> playerLinks = new Dictionary<string, string>();
            string lastNameLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < lastNameLetters.Length; i++)
            {
                string currentLetter = lastNameLetters.Substring(i, 1);
                string mainPage = "https://www.footballdb.com/players/players.html?letter=" + currentLetter;

                HtmlWeb page = new HtmlWeb();
                HtmlDocument doc = page.Load(mainPage);

                HtmlNodeCollection pagesNode = doc.DocumentNode.SelectNodes("//div[@class='dropdown']//ul//li");

                if (pagesNode != null)
                {
                    for (int x = 0; x < pagesNode.Count; x++)
                    {
                        int pageNum = x + 1;
                        mainPage = "https://www.footballdb.com/players/players.html?page=" + pageNum.ToString() + "&letter=" + currentLetter;

                        page = new HtmlWeb();
                        doc = page.Load(mainPage);

                        HtmlNodeCollection playersTbl = doc.DocumentNode.SelectNodes("//table//tbody//tr//td//a");

                        for (int y = 0; y < playersTbl.Count; y++)
                        {
                            if (playersTbl[y].HasAttributes && playersTbl[y].Attributes.Count > 1)
                            {
                                string link = playersTbl[y].Attributes["href"].Value;
                                string playerName = playersTbl[y].Attributes["title"].Value.Replace("Stats", "").Trim();

                                if (!playerLinks.ContainsKey(playerName))
                                {
                                    playerLinks.Add(playerName, link);
                                }
                            }
                        }
                       
                    }
                }
                else
                {
                    mainPage = "https://www.footballdb.com/players/players.html?&letter=" + currentLetter;

                    page = new HtmlWeb();
                    doc = page.Load(mainPage);

                    HtmlNodeCollection playersTbl = doc.DocumentNode.SelectNodes("//table//tbody//tr//td//a");

                    for (int y = 0; y < playersTbl.Count; y++)
                    {
                        if (playersTbl[y].HasAttributes && playersTbl[y].Attributes.Count > 1)
                        {
                            string link = playersTbl[y].Attributes["href"].Value;
                            string playerName = playersTbl[y].Attributes["title"].Value.Replace("Stats", "").Trim();

                            if (!playerLinks.ContainsKey(playerName))
                            {
                                playerLinks.Add(playerName, link);
                            }
                        }
                    }
                }
                
                
            }
            return playerLinks;
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
          
            for (int i = 0; i < teamNodes.Count - 1; i+=2)
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
        /// Private Method To Get Team Season Schedule Links
        /// </summary>
        /// <returns>Dictionary</returns>
        #region TEAM SCHEDULE LINKS DICTIONARY

        private static Dictionary<string, string> Team_Schedule_Links()
        {
            
            string indexUrl = "https://www.footballdb.com/teams/index.html";
            Dictionary<string, string> linksDictionary = new Dictionary<string, string>();

            HtmlWeb page = new HtmlWeb();
            HtmlDocument doc = page.Load(indexUrl);
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//table//tr");

            for (int i = 1; i < 40; i++)
            {
                HtmlNode currentNode = linkNodes[i];

                string mainLink = "https://www.footballdb.com";
                string teamName = currentNode.ChildNodes[1].InnerText;

                foreach (var node in currentNode.ChildNodes[3].ChildNodes)
                {
                    
                    if (node.Name == "a" && node.HasAttributes)
                    {
                        if (node.InnerText == "Schedule")
                        {
                            string link = node.Attributes["href"].Value;
                            linksDictionary.Add(teamName, mainLink + link);

                        }

                    }
                        
                }  
            }
            return linksDictionary;
        }

        #endregion

        /// <summary>
        /// Private Method To Get Team Stats Links
        /// </summary>
        /// <returns>Dictionary</returns>
        #region TEAM STATS LINKS DICTIONARY

        private static Dictionary<string, string> Team_Stats_Links()
        {

            string indexUrl = "https://www.footballdb.com/teams/index.html";
            Dictionary<string, string> linksDictionary = new Dictionary<string, string>();

            HtmlWeb page = new HtmlWeb();
            HtmlDocument doc = page.Load(indexUrl);
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//table//tr");

            for (int i = 1; i < 40; i++)
            {
                HtmlNode currentNode = linkNodes[i];
                
                string mainLink = "https://www.footballdb.com";
                string teamName = currentNode.ChildNodes[1].InnerText;

                foreach (var node in currentNode.ChildNodes[3].ChildNodes)
                {
                    string test = "";
                    if (node.Name == "a" && node.HasAttributes)
                    {
                        if (node.InnerText == "Stats")
                        {
                            string link = node.Attributes["href"].Value;
                            linksDictionary.Add(teamName, mainLink + link);

                        }

                    }

                }
            }
            return linksDictionary;
        }

        #endregion

        /// <summary>
        /// Method to load a list with Player Links
        /// </summary>
        /// <returns>List</returns>
        #region GET TOTAL TEAM STATS
        public static Dictionary<string, string> Get_Player_Links(string year)
        {
            List<Stat> stats = new List<Stat>();
            Dictionary<string, string> statLinks = Team_Stats_Links();

            foreach (KeyValuePair<string, string> link in statLinks)
            {
                string teamName = link.Key;
                string url = link.Value + "/" + year;

                HtmlWeb page = new HtmlWeb();
                HtmlDocument doc = page.Load(url);

                HtmlNodeCollection playerLinks = doc.DocumentNode.SelectNodes("//table//tbody//tr//td//a");

                foreach (HtmlNode node in playerLinks)
                {
                    string preUrl = "https://www.footballdb.com";
                    string playerLink = node.Attributes[0].Value;
                    string builtUrl = preUrl + playerLink + "/stat-splits";

                    HtmlWeb playerPage = new HtmlWeb();
                    HtmlDocument playerDoc = playerPage.Load(builtUrl);

                    HtmlNodeCollection statNode = playerDoc.DocumentNode.SelectNodes("//table[1]//tbody//tr");
                    string test = "";
                    for (int i = 0; i < statNode.Count; i++)
                    {
                        foreach (var item in statNode[i].ChildNodes)
                        {

                        }
                        
                    }
                    test = "";
                }

                
                
               
            }

            return statLinks;
           
        }

        #endregion

        /// <summary>
        /// GET PLAYER STATS
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        #region GET PLAYER STATS    

        public static List<Player> Get_Player_Stats(string player)
        {
            List<Player> playersStats = new List<Player>();

            return playersStats;
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
