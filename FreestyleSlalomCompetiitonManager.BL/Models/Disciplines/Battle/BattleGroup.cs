using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{
    public class BattleGroup
    {
        public Dictionary<Competitor, int> Competitors { get; set; } = [];
    }
}
