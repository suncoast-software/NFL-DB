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
        DataClasses_PlayerDataDataContext db = new DataClasses_PlayerDataDataContext();
        List<Matchup> matchups = new List<Matchup>();
        Dictionary<string, string> links = new Dictionary<string, string>();
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

            //for (int i = 2017; i < 2019; i++)
            //{
            //    matchups.Clear();
            //    for (int y = 1; y < 18; y++)
            //    {
            //      matchups = DataHelper.Get_Week_Scores(i.ToString(), y.ToString(), SeasonType.REG);
            //      DataHelper.Save_Scores_To_XML(matchups, i.ToString(), y.ToString());
            //    }  
            //}

            links = DataHelper.Get_Player_Links();
            DataHelper.Save_Player_Links_To_XML(links);
           
        }
    }
}
