﻿using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Jump (AgeCategory ageCategory, SexCategory sexCategory) : BaseDiscipline(ageCategory, sexCategory)
    {
        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankJump != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankJump).ToList().ForEach(s => Competitors.Add(GetRank(s.CompetitionRankJump), s));
        }
    }
}
