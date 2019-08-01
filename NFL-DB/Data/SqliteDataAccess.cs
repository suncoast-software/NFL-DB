using Dapper;
using NFL_DB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Data
{
    public class SqliteDataAccess
    {
        /// <summary>
        /// LOAD PLAYER STATS
        /// </summary>
        /// <returns>List Player</returns>
        public static List<Player> LoadPlayers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Player>("select * from Player", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// LOAD ALL GAMES DATA
        /// </summary>
        /// <returns>List Game</returns>
        public static List<Game> LoadGames()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Game>("select * from Game", new DynamicParameters());
                return output.ToList();
            }
        }
        /// <summary>
        /// SAVE PLAYER STATS
        /// </summary>
        /// <param name="player"></param>
        public static void Save_Player(Player player)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Player (Name, Pos, Team, College, Link) values (@Name, @Pos, @Team, @College, @Link)", player);
            }
        }

        /// <summary>
        /// SAVE GAME DATA
        /// </summary>
        /// <param name="game"></param>
        public static void Save_Game(Game game)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Game (AwayTeamName, HomeTeamName, AwayFinalScore, HomeFinalScore, AwayTeamRecord, HomeTeamRecord, AQ1, AQ2, AQ3, AQ4, HQ1, HQ2, HQ3, HQ4, Year, Week) values (@AwayTeamName, @HomeTeamName, @AwayFinalScore, @HomeFinalScore, @AwayTeamRecord, @HomeTeamRecord, @AQ1, @AQ2, @AQ3, @AQ4, @HQ1, @HQ2, @HQ3, @HQ4, @Year, @Week)", game);


            }
        }

        /// <summary>
        /// DATABASE CONNECTION STRING
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
