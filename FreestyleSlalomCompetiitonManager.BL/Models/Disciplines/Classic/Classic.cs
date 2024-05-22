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
            if (numberOfPrequalified <= 0 || maxNumberOfSkatersInGroup <= 0)
            {
                throw new ArgumentException("Invalid input parameters. Number of prequalified skaters, and maximum number of skaters in a group must be greater than zero.");
            }

            Rounds.Add(new ClassicRound() { Number = 0, Type = Round.Final });

            for (int i = 0; i < numberOfPrequalified; i++)
            {
                Rounds.Find(r => r.Type == Round.Final)?.Competitors.Add(Competitors[i]);
            }

            if (numberOfPrequalified >= Competitors.Count) { return; }


            if (numberOfQualificationGroups > 1)
            {
                List<ClassicRound> Qualifications = [];
                for (int i = 0; i < numberOfQualificationGroups; i++)
                {
                    Qualifications.Add(new ClassicRound() { Number = 1 + i, Type = Round.Qualification });
                }
                int roundsCounter = 0;
                bool addtion = true;

                for (int i = numberOfPrequalified; i < Competitors.Count; i++)
                {
                    if (addtion)
                    {
                        Qualifications[roundsCounter++].Competitors.Add(Competitors[i]);
                    }
                    else
                    {
                        Qualifications[roundsCounter--].Competitors.Add(Competitors[i]);
                    }

                    if (roundsCounter >= Qualifications.Count || roundsCounter < 0)
                    {
                        if (addtion)
                        {
                            addtion = false;
                            roundsCounter--;
                        }
                        else
                        {
                            addtion = true;
                            roundsCounter++;
                        }
                    }
                }

                Rounds.AddRange(Qualifications);

            }
            else
            {
                var qualification = new ClassicRound() { Number = 1, Type = Round.Qualification };

                for (int i = numberOfPrequalified; i < Competitors.Count; i++)
                {
                    qualification.Competitors.Add(Competitors[i]);
                }
                Rounds.Add(qualification);
            }
        }

        public override void AssignCompetitors(List<Competitor> skaters)
        {
            skaters.Where(s => s.CompetitionRankClassic != null && s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).OrderBy(s => s.CompetitionRankClassic).ToList().ForEach(s => Competitors.Add((int)s.CompetitionRankClassic, s));
        }
    }
}
