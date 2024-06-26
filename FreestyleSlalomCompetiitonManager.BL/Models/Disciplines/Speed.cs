﻿using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class Speed (AgeCategory ageCategory, SexCategory sexCategory) : BaseDiscipline(ageCategory, sexCategory, Discipline.Speed)
    {
        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankSpeed != null && IsInAgeCategory(AgeCategory, s.AgeCategory) && IsInSexCategory(SexCategory, s.SexCategory)).OrderBy(s => s.CompetitionRankSpeed).ToList().ForEach(s => Competitors.Add(s));
        }

        public override void ProcessDiscipline()
        {
            foreach (var s in Competitors)
            {
                s.CompetitionResultSpeed = ConsoleCommunicator.GetSkatersResult(s);
            }
        }

        public override List<Competitor> GetResults()
        {
            return Competitors.OrderBy(x =>x.CompetitionResultSpeed).ToList();
        }

        public override string ToString()
        {
            return "Speed Slalom";
        }
    }
}
