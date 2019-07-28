using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Helpers
{
    public static class DirectoryHelper
    {
        public static void Create_Dir()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\data\xml\");
        }
    }
}
