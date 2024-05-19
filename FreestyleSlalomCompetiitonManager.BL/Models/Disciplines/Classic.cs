using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Categories
{
    public class Classic : BaseDiscipline
    {
        public void AssignCompetitiors(List<SkaterOnCompetition> skaters)
        {
            skaters.Where(s => s.CompetitionRankClassic != null).OrderBy(s => s.CompetitionRankClassic).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
