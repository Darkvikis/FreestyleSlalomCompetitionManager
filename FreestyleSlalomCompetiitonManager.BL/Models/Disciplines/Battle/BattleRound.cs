using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{
    public class BattleRound : BaseRound
    {
        public List<BattleGroup> Groups = [];

        public BattleRound(SortedDictionary<int, Competitor> competitors)
        {
            Competitors = competitors;
            CreateGroups();
            AssignCompetitorsToGroups();
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

        public void AssignCompetitorsToGroups()
        {
            int groupsCounter = 0;
            bool addition = true;

            foreach(var Competitor in Competitors)
            {
                Groups[groupsCounter].Competitors.Add(Competitor.Value, Competitor.Value.CompetitionRankBattle ?? int.MaxValue);

                groupsCounter += addition ? 1 : -1;

                if (groupsCounter >= Groups.Count || groupsCounter < 0)
                {
                    addition = !addition;
                    groupsCounter += addition ? 1 : -1;
                }
            }
        }
    }
}
