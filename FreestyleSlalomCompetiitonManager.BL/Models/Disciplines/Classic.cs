using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Classic : BaseDiscipline
    {
        public override void AssignCompetitiors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankClassic != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankClassic).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
