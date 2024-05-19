using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Categories
{
    public class Battle : BaseCategory
    {
        public void AssignCompetitiors(List<SkaterOnCompetition> skaters)
        {
            skaters.Where(s => s.CompetitionRankBattle != null).OrderBy(s => s.CompetitionRankBattle).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
