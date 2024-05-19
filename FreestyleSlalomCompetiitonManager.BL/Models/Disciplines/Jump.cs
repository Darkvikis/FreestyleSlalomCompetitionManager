using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Jump : BaseDiscipline
    {
        public void AssignCompetitiors(List<SkaterOnCompetition> skaters)
        {
            skaters.Where(s => s.CompetitionRankJump != null).OrderBy(s => s.CompetitionRankJump).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
