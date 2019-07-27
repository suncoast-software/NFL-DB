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

        public MainWindow()
        {
            InitializeComponent();

            //DataHelper.Get_Matchups("1");
            playerData.ItemsSource = DataHelper.Get_Current_Player_Stats();
            //DataHelper.Get_Week_Scores("1970", "1", SeasonType.REG);

            var players = (from p in db.players
                           where p.Team == "NY Jets" && p.Pos == "OT"
                           select p).Take(2);

            lblCount.Content = players.Count();       
               
        }
    }
}
