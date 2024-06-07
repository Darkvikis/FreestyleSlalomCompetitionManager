using Bogus;
using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class BaseTest
    {
        private static readonly Faker faker = new();
        public static Competition CreateCompetition()
        {
            return new Competition(
                            faker.Company.CompanyName(),
                            faker.Date.Recent(),
                            faker.Date.Soon(),
                            faker.Lorem.Paragraph(),
                            faker.Address.FullAddress()

                        )
            {
                Organizer = new Organizer(faker.Name.FullName(),
                            faker.Random.Replace("####???##########"))
            }; ;
        }

        public static Competitor CreateCompetitor(AgeCategory? ageCategory, SexCategory? sexCategory)
        {
            return new(faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Address.Country().ToUpper())
            {
                AgeCategory = ageCategory ?? faker.PickRandom<AgeCategory>(),
                SexCategory = sexCategory ?? faker.PickRandom<SexCategory>(),
                WSID = faker.Random.Replace("####???##########"),
                Birthdate = faker.Date.PastDateOnly()
            };
        }
        public static List<Competitor> CreateListOfCompetitors(int numberOfCompetitors, AgeCategory? ageCategory, SexCategory? sexCategory)
        {
            var competitors = new List<Competitor>();
            for (int i = 0; i < numberOfCompetitors; i++)
            {
                competitors.Add(CreateCompetitor(ageCategory, sexCategory));
            }
            return competitors;
        }

        public static WorldRank CreateWorldRank()
        {
            return new(faker.Random.Replace("####???##########"), faker.Date.Recent())
            {
                Discipline = faker.PickRandom<Discipline>(),
                AgeCategory = faker.PickRandom<AgeCategory>(),
                SexCategory = faker.PickRandom<SexCategory>(),
                Rank = (ushort)faker.Random.Number(1, 100)
            };
        }

        public static List<WorldRank> CreateListOfWorldRanks(int numberOfWorldRanks)
        {
            var worldRanks = new List<WorldRank>();
            for (int i = 0; i < numberOfWorldRanks; i++)
            {
                worldRanks.Add(CreateWorldRank());
            }
            return worldRanks;
        }

        public static Classic CreateClassic()
        {
            return new Classic(faker.PickRandom<AgeCategory>(), faker.PickRandom<SexCategory>());
        }
    }
}
