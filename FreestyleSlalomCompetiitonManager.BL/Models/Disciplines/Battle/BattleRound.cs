using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{
    public class BattleRound : BaseRound
    {
        public List<BattleGroup> Groups = [];

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

            for (int i = 0; i < Competitors.Count; i++)
            {
                Groups[groupsCounter].Competitors.Add(Competitors[i], Competitors[i].CompetitionRankClassic ?? int.MaxValue);

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
