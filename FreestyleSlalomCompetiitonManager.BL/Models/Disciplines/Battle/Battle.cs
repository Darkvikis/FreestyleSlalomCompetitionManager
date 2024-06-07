using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{
    public class Battle(AgeCategory ageCategory, SexCategory sexCategory) : BaseDiscipline(ageCategory, sexCategory, Discipline.Battle)
    {
        public List<BattleRound> Rounds = [];

        private BattleRound CurrentRound => Rounds.Last();

        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankBattle != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankBattle).ToList().ForEach(s => Competitors.Add(GetRank(s.CompetitionRankBattle), s));
        }

        public void InitializeBattle()
        {
            Rounds.Add(new BattleRound(Competitors));
        }

        public override void ProcessDiscipline()
        {
            ConsoleCommunicator.DisplayStartDiscipline(this.ToString(), AgeCategory.ToString(), SexCategory.ToString());

            InitializeBattle();
            while (true)
            {
                ConsoleCommunicator.DisplayStartRound(CurrentRound.ToString());
                CurrentRound.Groups.ForEach(g =>
                {
                    ConsoleCommunicator.DisplayStartGroup();
                    int groupRank = 1;
                    g.Competitors.Keys.ToList().ForEach(c =>
                    {
                        ConsoleCommunicator.DisplayCompetitor(groupRank, c.FirstName + " " + c.FamilyName);
                    });

                    do
                    {
                        ValidateGivenResults(ConsoleCommunicator.AskForBattleResults(), g.Competitors.Count);
                    }
                    while (!ConsoleCommunicator.DisplayEndGroup(g.Competitors));

                });

                ConsoleCommunicator.DisplayEndRound();

                if (CurrentRound.Type == Round.Final)
                {
                    break;
                }
                else
                {
                    Rounds.Add(new BattleRound(CurrentRound.GetAdvancing()));
                }

            }

            ConsoleCommunicator.DisplayEndDiscipline(this.ToString(), AgeCategory.ToString(), SexCategory.ToString());
        }

        private List<int> ValidateGivenResults(string? inputs, int numOfSkatersInGroup)
        {
            if (inputs == null)
            {
                throw new ArgumentException("Invalid input. Results must be provided.");
            }

            var inputsList = inputs.Split(" ");

            if (inputsList.Length != numOfSkatersInGroup)
            {
                throw new ArgumentException("Invalid input. The number of results must be equal to the number of skaters in a group.");
            }

            List<int> results = [];
            foreach (var input in inputsList)
            {
                if (!int.TryParse(input, out int result))
                {
                    throw new ArgumentException("Invalid input. Results must be integers.");
                }
                results.Add(result);
            }

            return results;
        }

        public override string ToString()
        {
            return "Freestyle Slalom Battle";
        }
    }
}
