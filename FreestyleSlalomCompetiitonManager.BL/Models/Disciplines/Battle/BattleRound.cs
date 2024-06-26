﻿using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{
    public class BattleRound : BaseRound
    {
        public List<BattleGroup> Groups = [];

        public void InitialiazeBattleRound()
        {
            CreateGroups();
            AssignCompetitorsToGroupsByRank();
            AssignRoundType();
        }

        public void CreateGroups()
        {
            int numberOfCompetitors = Competitors.Count;
            int groupSize = 4;

            int numberOfGroups = numberOfCompetitors / groupSize;

            for (int i = 0; i < numberOfGroups; i++)
            {
                BattleGroup group = new();
                Groups.Add(group);
            }
        }

        public void AssignCompetitorsToGroupsByRank()
        {
            int groupsCounter = 0;
            bool addition = true;

            foreach (var Competitor in Competitors)
            {
                Groups[groupsCounter].Competitors.Add(new() { Competitor = Competitor, RankInGroup = Competitor.CompetitionRankBattle ?? int.MaxValue });

                groupsCounter += addition ? 1 : -1;

                if (groupsCounter >= Groups.Count || groupsCounter < 0)
                {
                    addition = !addition;
                    groupsCounter += addition ? 1 : -1;
                }
            }
        }


        public virtual List<Competitor> GetAdvancing()
        {
            List<Competitor> advancing = [];
            int rank = 1;
            foreach (var group in Groups)
            {
                advancing.Add(group.Competitors.First(x => x.RankInGroup == 1).Competitor);
                advancing.Add(group.Competitors.First(x => x.RankInGroup == 2).Competitor);
            }

            return advancing;
        }

        public void AssignRoundType()
        {
            Type = Groups.Count switch
            {
                1 => Round.Final,
                2 => Round.SemiFinal,
                4 => Round.QuarterFinal,
                8 => Round.EighthFinal,
                16 => Round.SixteenthFinal,
                32 => Round.ThirtySecondFinal,
                64 => Round.SixtyFourthFinal,
                128 => Round.HundredTwentyEighthFinal,
                _ => Round.Qualification
            };
        }

        public override string ToString()
        {
            return ((Round)Type).ToString();
        }
    }
}
