using FreestyleSlalomCompetitionManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class Skater(string name, string country, string WSID) : BaseModel
    {
        public string Name { get; set; } = name;
        public string Country { get; set; } = country;
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public string WSID { get; set; } = WSID;
        public List<WorldRank> WorldRanks { get; set; } = [];
    }
}

