using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Skater(string firstName, string familyName, string country, string WSID) : BaseSkater(firstName, familyName, country)
    {
        public string WSID { get; set; } = WSID;
        public List<WorldRank> WorldRanks { get; set; } = [];

    }
}

