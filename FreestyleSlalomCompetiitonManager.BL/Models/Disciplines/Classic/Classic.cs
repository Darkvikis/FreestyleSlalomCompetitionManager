using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic
{
    public class Classic(AgeCategory ageCategory, SexCategory sexCategory) : BaseDiscipline(ageCategory, sexCategory)
    {
        public List<ClassicRound> Rounds { get; set; } = [];

        public void GenerateRounds(int numberOfPrequalified, int maxNumberOfSkatersInGroup, int numberOfQualificationGroups = 1)
        {
            ValidateInputParameters(numberOfPrequalified, maxNumberOfSkatersInGroup, numberOfQualificationGroups);
            InitializeFinalRound();
            AddPrequalifiedCompetitorsToFinalRound(numberOfPrequalified);

            if (numberOfPrequalified < Competitors.Count)
            {
                if (numberOfQualificationGroups > 1)
                {
                    GenerateMultipleQualificationRounds(numberOfPrequalified, numberOfQualificationGroups);
                }
                else
                {
                    GenerateSingleQualificationRound(numberOfPrequalified);
                }
            }
        }

        private void ValidateInputParameters(int numberOfPrequalified, int maxNumberOfSkatersInGroup, int numberOfQualificationGroups)
        {
            if (numberOfPrequalified <= 0 || maxNumberOfSkatersInGroup <= 0)
            {
                throw new ArgumentException("Invalid input parameters. Number of prequalified skaters, and maximum number of skaters in a group must be greater than zero.");
            }
            if (numberOfPrequalified > maxNumberOfSkatersInGroup)
            {
                throw new ArgumentException("Invalid input parameters. The number of prequalified skaters must be less than or equal to the maximum number of skaters in a group.");
            }
            if (maxNumberOfSkatersInGroup - numberOfPrequalified < numberOfQualificationGroups)
            {
                throw new ArgumentException("Invalid input parameters. The difference between the maximum number of skaters in a group and the number of prequalified skaters must be greater than or equal to the number of qualification groups.");
            }
        }

        private void InitializeFinalRound()
        {
            Rounds.Add(new ClassicRound() { Number = 0, Type = Round.Final });
        }

        private void AddPrequalifiedCompetitorsToFinalRound(int numberOfPrequalified)
        {
            for (int i = 0; i < numberOfPrequalified; i++)
            {
                Rounds.Find(r => r.Type == Round.Final)?.Competitors.Add(Competitors[i]);
            }
        }

        private void GenerateMultipleQualificationRounds(int numberOfPrequalified, int numberOfQualificationGroups)
        {
            List<ClassicRound> qualifications = new List<ClassicRound>();
            for (int i = 0; i < numberOfQualificationGroups; i++)
            {
                qualifications.Add(new ClassicRound() { Number = 1 + i, Type = Round.Qualification });
            }

            int roundsCounter = 0;
            bool addition = true;

            for (int i = numberOfPrequalified; i < Competitors.Count; i++)
            {
                if (addition)
                {
                    qualifications[roundsCounter++].Competitors.Add(Competitors[i]);
                }
                else
                {
                    qualifications[roundsCounter--].Competitors.Add(Competitors[i]);
                }

                if (roundsCounter >= qualifications.Count || roundsCounter < 0)
                {
                    if (addition)
                    {
                        addition = false;
                        roundsCounter--;
                    }
                    else
                    {
                        addition = true;
                        roundsCounter++;
                    }
                }
            }

            Rounds.AddRange(qualifications);
        }

        private void GenerateSingleQualificationRound(int numberOfPrequalified)
        {
            var qualification = new ClassicRound() { Number = 1, Type = Round.Qualification };

            for (int i = numberOfPrequalified; i < Competitors.Count; i++)
            {
                qualification.Competitors.Add(Competitors[i]);
            }

            Rounds.Add(qualification);
        }


        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankClassic != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankClassic).ToList().ForEach(s => Competitors.Add((int)s.CompetitionRankClassic, s));
        }
    }
}
