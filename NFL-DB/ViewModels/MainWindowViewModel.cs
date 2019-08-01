using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NFL_DB.ViewModels
{
    /// <summary>
    /// ViewModel Class for the Main Window.
    /// </summary>
    public class MainWindowViewModel
    {
        public static void Handle_Click(object sender, EventArgs e)
        {
            Button obj = sender as Button;
            string name = obj.Name;

            switch (name)
            {
                case "btnOne":
                    MessageBox.Show("clicked me!");
                    break;

                default:

                    break;
            }
        }
    }
}
