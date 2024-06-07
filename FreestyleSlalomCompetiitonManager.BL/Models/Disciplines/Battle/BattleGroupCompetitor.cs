using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{

    public class BattleGroupCompetitor : BaseModel
    {
        public Competitor Competitor { get; set; }
        public int RankInGroup { get; set; }
    }
}
