using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class BaseRound : BaseModel
    {
        public Round Type { get; set; }
        public int Number { get; set; }

        public virtual SortedDictionary<int,Competitor> Competitors { get; set; } = [];

    }
}
