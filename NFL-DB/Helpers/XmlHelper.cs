using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFL_DB.Models;

namespace NFL_DB.Helpers
{
    public static class XmlHelper
    {
        /// <summary>
        /// SAVE PLAYER DATA TO XML
        /// </summary>
        /// <param name="players"></param>
        #region SAVE PLAYER DATA TO XML
        public static void Save_Player_Data(List<Player> players)
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"\data\xml\current_nfl_players_Data.xml";

            
            foreach (var player in players)
            {
                if (!File.Exists(xmlPath))
                {
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                        new XElement("players",
                                            new XElement("player",
                                                new XElement("playerName", player.Name),
                                                new XElement("pos", player.Pos),
                                                new XElement("team", player.Team),
                                                new XElement("college", player.College)
                                                )));

                xmlDoc.Save(xmlPath);
                }
                else
                {
                    var playerElement = new XElement("player",
                                            new XElement("playerName", player.Name),
                                            new XElement("pos", player.Pos),
                                            new XElement("team", player.Team),
                                            new XElement("college", player.College)
                                            );

                    var xmlDoc = XDocument.Load(xmlPath);
                    xmlDoc.Element("players").AddFirst(playerElement);
                    xmlDoc.Save(xmlPath);
                }
                   
            }
               
        }
        #endregion

        /// <summary>
        /// Method to Save The Player Links To XML.
        /// builds a xml file with all links to each players page.
        /// </summary>
        /// <param name="links"></param>
        #region SAVE PLAYER LINKS TO XML
        public static void Save_Player_Links_To_XML(Dictionary<string, string> links)
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"\data\xml\player_links.xml";

            foreach (KeyValuePair<string, string> link in links)
            {
                if (!File.Exists(xmlPath))
                {

                    XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                                new XElement("links",
                                                    new XElement("player",
                                                    new XAttribute("player", link.Key),
                                                    new XAttribute("link", link.Value))));

                    xDoc.Save(xmlPath);
                }
                else
                {
                    XDocument xDoc = XDocument.Load(xmlPath);
                    var el = new XElement("player",
                                        new XAttribute("player", link.Key),
                                        new XAttribute("link", link.Value));

                    xDoc.Element("links").AddFirst(el);
                    xDoc.Save(xmlPath);
                }
            }
        }
        #endregion

        /// <summary>
        /// Method to Save Week Scores to XML
        /// </summary>
        /// <returns>Void</returns>
        #region SAVE SCORES TO XML
        public static void Save_Scores_To_XML(List<Matchup> list, string year, string week)
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"\data\xml\scores.xml";

            foreach (var matchup in list)
            {


                if (!File.Exists(xmlPath))
                {
                    try
                    {
                        XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                                        new XElement("matchups",
                                                        new XElement("matchup",
                                                            new XAttribute("year", year),
                                                            new XAttribute("week", week),
                                                            new XAttribute("away-team", matchup.AwayTeam.Name),
                                                            new XAttribute("home-team", matchup.HomeTeam.Name),
                                                                new XElement("quarter-scores",
                                                                        new XElement("away-team-scores",
                                                                            new XAttribute("q1", matchup.awayQuarterScores[0]),
                                                                            new XAttribute("q2", matchup.awayQuarterScores[1]),
                                                                            new XAttribute("q3", matchup.awayQuarterScores[2]),
                                                                            new XAttribute("q4", matchup.awayQuarterScores[3]),
                                                                            new XAttribute("total", matchup.AwayScore),
                                                                        new XElement("home-team-scores",
                                                                            new XAttribute("q1", matchup.homeQuarterScores[0]),
                                                                            new XAttribute("q2", matchup.homeQuarterScores[1]),
                                                                            new XAttribute("q3", matchup.homeQuarterScores[2]),
                                                                            new XAttribute("q4", matchup.homeQuarterScores[3]),
                                                                            new XAttribute("total", matchup.HomeScore)))))));
                        xDoc.Save(xmlPath);
                    }
                    catch (Exception ex)
                    {


                    }
                }
                else
                {
                    try
                    {
                        XDocument xDoc = XDocument.Load(xmlPath);
                        var matchupElement = new XElement("matchup",
                                                            new XAttribute("year", year),
                                                            new XAttribute("week", week),
                                                            new XAttribute("away-team", matchup.AwayTeam.Name),
                                                            new XAttribute("home-team", matchup.HomeTeam.Name),
                                                                new XElement("quarter-scores",
                                                                        new XElement("away-team-scores",
                                                                            new XAttribute("q1", matchup.awayQuarterScores[0]),
                                                                            new XAttribute("q2", matchup.awayQuarterScores[1]),
                                                                            new XAttribute("q3", matchup.awayQuarterScores[2]),
                                                                            new XAttribute("q4", matchup.awayQuarterScores[3]),
                                                                            new XAttribute("total", matchup.AwayScore)),
                                                                        new XElement("home-team-scores",
                                                                            new XAttribute("q1", matchup.homeQuarterScores[0]),
                                                                            new XAttribute("q2", matchup.homeQuarterScores[1]),
                                                                            new XAttribute("q3", matchup.homeQuarterScores[2]),
                                                                            new XAttribute("q4", matchup.homeQuarterScores[3]),
                                                                            new XAttribute("total", matchup.HomeScore))));

                        xDoc.Element("matchups").AddFirst(matchupElement);
                        xDoc.Save(xmlPath);

                    }
                    catch (Exception ex)
                    {


                    }
                }
            }

        }
        #endregion
    }
}
