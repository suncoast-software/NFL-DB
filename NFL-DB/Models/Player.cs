using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Pos { get; set; }
        public string Team { get; set; }
        public string  College { get; set; }

        public Player()
        {
        }

        public Player(string name, string pos, string team, string college)
        {
            Name = name;
            Pos = pos;
            Team = team;
            College = college;
        }
    }
}
