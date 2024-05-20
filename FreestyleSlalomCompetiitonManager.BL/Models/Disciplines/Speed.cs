using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Speed : BaseDiscipline
    {
        public override void AssignCompetitiors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankSpeed != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankSpeed).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
