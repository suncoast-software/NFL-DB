using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_DB.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string Record { get; set; }
        public string Division { get; set; }
        public string Logo_Url { get; set; }

        public Team()
        {
        }

        public Team(string name, string record)
        {
            Name = name;
            Record = record;
            
        }

        public Team(string name, string record, string division, string logo_Url) : this(name, record)
        {
            Division = division;
            Logo_Url = logo_Url;
        }
    }
}
