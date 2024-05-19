﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Speed : BaseDiscipline
    {
        public void AssignCompetitiors(List<SkaterOnCompetition> skaters)
        {
            skaters.Where(s => s.CompetitionRankSpeed != null).OrderBy(s => s.CompetitionRankSpeed).ToList().ForEach(s => Competitors.Add(s));
        }
    }
}
