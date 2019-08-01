using NFL_DB.Data;
using NFL_DB.Helpers;
using NFL_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NFL_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // DataClasses_PlayerDataDataContext db = new DataClasses_PlayerDataDataContext();
        List<Player> players = new List<Player>();
        List<Game> games = new List<Game>();
       // Dictionary<string, string> links = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
            //DirectoryHelper.Create_Dir();
            // List<Player> playersList = DataHelper.Get_Current_Player_Stats();
            //DataHelper.Get_Matchups("1");
            //playerData.ItemsSource = DataHelper.Get_Current_Player_Stats();
            //DataHelper.Get_Week_Scores("1970", "1", SeasonType.REG);
            //XmlHelper.Save_Player_Data(playersList);
            //DataHelper.Get_Player_Links("2018");
            DataHelper.Get_All_Player_Stats_Links();

            //DataHelper.Get_Player_Links();
            //players_data.ItemsSource = SqliteDataAccess.LoadPlayers();

            // DataHelper.Get_All_Player_Stats_Links();
            //players_data.ItemsSource = SqliteDataAccess.LoadPlayers();

            //players_data.ItemsSource = SqliteDataAccess.LoadGames();
            // games = SqliteDataAccess.LoadGames();
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            //DataHelper.Get_Week_Scores("2010", "8", SeasonType.REG);
            //string[] queryDetails = txtQuery.Text.Split(' ');

            //if (queryDetails.Length > 1)
            //{
            //    var _games = (from game in games
            //                  where game.Year.Equals(queryDetails[0]) && game.Week.Equals(queryDetails[1])
            //                  select game).ToList();

            //    MessageBox.Show(_games.Count().ToString());
            //    players_data.ItemsSource = _games;
            //}
            //else
            //{
            //    var _games = (from game in games
            //                  where game.Year.Equals(queryDetails[0])
            //                  select game).ToList();

            //    MessageBox.Show(_games.Count().ToString());
            //    players_data.ItemsSource = _games;
            //}
            
                
        }
    }
}
