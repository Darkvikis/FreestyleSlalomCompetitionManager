using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Slide(AgeCategory ageCategory, SexCategory sexCategory) : BaseDiscipline(ageCategory, sexCategory, Discipline.Slide)
    {
        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankSlide != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankSlide).ToList().ForEach(s => Competitors.Add(GetRank(s.CompetitionRankSlide), s));
        }

        public override string ToString()
        {
            return "Freestyle Slide";
        }
    }
}

