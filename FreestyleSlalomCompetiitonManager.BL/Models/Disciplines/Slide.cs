using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Slide : BaseDiscipline
    {
        public override void AssignCompetitiors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankSlide != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankSlide).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
