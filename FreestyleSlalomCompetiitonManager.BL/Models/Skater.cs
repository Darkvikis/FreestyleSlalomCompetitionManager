using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Skater(string WSID,string firstName, string familyName, string country) : BaseSkater(WSID, firstName, familyName, country)
    {
        public virtual List<WorldRank> WorldRanks { get; set; } = [];

    }
}

