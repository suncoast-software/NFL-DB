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
        public static void Save_Player_Data(List<player> players)
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"\data\xml\playerData.xml";

            if (!File.Exists(xmlPath))
            {
                foreach (var player in players)
                {
                    XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                                new XElement("players",
                                                    new XElement("player",
                                                        new XElement("playerName", player.PlayerName),
                                                        new XElement("pos", player.Pos),
                                                        new XElement("team", player.Team),
                                                        new XElement("college", player.College)
                                                        )));

                    xmlDoc.Save(xmlPath);
                }
                
            }
            else
            {
                foreach (var player in players)
                {
                    var playerElement = new XElement("player",
                                            new XElement("playerName", player.PlayerName),
                                            new XElement("pos", player.Pos),
                                            new XElement("team", player.Team),
                                            new XElement("college", player.College)
                                            );

                    var xmlDoc = XDocument.Load(xmlPath);
                    xmlDoc.Add(playerElement);
                    xmlDoc.Save(xmlPath);

                }
                
            }
        }
    }
}
