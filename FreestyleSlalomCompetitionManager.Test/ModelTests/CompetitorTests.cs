using Xunit;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Enums;
using Bogus;
using System;
using FreestyleSlalomCompetitionManager.BL.Converters;

namespace FreestyleSlalomCompetitionManager.Test.ModelTests
{
    public class CompetitorTests : BaseTest
    {
        private readonly Faker faker = new();

        [Fact]
        public void Competitor_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var firstName = faker.Name.FirstName();
            var familyName = faker.Name.LastName();
            var country = faker.Address.Country();
            var WSID = faker.Random.Replace("####???##########");
            // Act
            var competitor = new Competitor(WSID, firstName, familyName, country);

            // Assert
            Assert.Equal(NameConverter.NameWithoutDiacritics(firstName), competitor.FirstName);
            Assert.Equal(NameConverter.NameWithoutDiacritics(familyName), competitor.FamilyName);
            Assert.Equal(NameConverter.CountryToShortCut(country), competitor.Country);
            Assert.Equal(WSID, competitor.WSID);
        }

        [Fact]
        public void SetMusic_WithCompetitionDateMoreThan7DaysAway_SetsSendMusicToOnTime()
        {
            // Arrange
            var competitor = CreateCompetitor(null, null);
            var music = faker.Random.Word();
            var competitionDate = DateTime.Now.AddDays(8);

            // Act
            competitor.SetMusic(music, competitionDate);

            // Assert
            Assert.Equal(music, competitor.Music);
            Assert.Equal(SendMusic.OnTime, competitor.SendMusic);
        }

        [Fact]
        public void SetMusic_WithCompetitionDateLessThan7DaysAway_SetsSendMusicToLate()
        {
            // Arrange
            var competitor = CreateCompetitor(null, null);
            var music = faker.Random.Word();
            var competitionDate = DateTime.Now.AddDays(6);

            // Act
            competitor.SetMusic(music, competitionDate);

            // Assert
            Assert.Equal(music, competitor.Music);
            Assert.Equal(SendMusic.Late, competitor.SendMusic);
        }
    }
}
