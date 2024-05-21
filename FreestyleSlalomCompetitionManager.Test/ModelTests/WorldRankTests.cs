using Xunit;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using Bogus;

namespace FreestyleSlalomCompetitionManager.Test.ModelTests
{
    public class WorldRankTests
    {
        private readonly Faker faker = new();

        [Fact]
        public void WorldRank_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var wsid = faker.Random.Replace("####???##########");
            var dateAdded = faker.Date.Recent();
            var discipline = faker.PickRandom<Discipline>();
            var ageCategory = faker.PickRandom<AgeCategory>();
            var sexCategory = faker.PickRandom<SexCategory>();
            var rank = (ushort)faker.Random.Number(1, 100);

            // Act
            var worldRank = new WorldRank(wsid, dateAdded)
            {
                Discipline = discipline,
                AgeCategory = ageCategory,
                SexCategory = sexCategory,
                Rank = rank
            };

            // Assert
            Assert.Equal(wsid, worldRank.WSID);
            Assert.Equal(dateAdded.Date, worldRank.DateAdded.Date);
            Assert.Equal(discipline, worldRank.Discipline);
            Assert.Equal(ageCategory, worldRank.AgeCategory);
            Assert.Equal(sexCategory, worldRank.SexCategory);
            Assert.Equal(rank, worldRank.Rank);
        }
    }
}
