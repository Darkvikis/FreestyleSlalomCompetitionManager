using FreestyleSlalomCompetitionManager.BL.Models;
using Xunit;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class CompetitionTests
    {
        [Fact]
        public void CreateCompetition_ShouldCreateCompetitionWithCorrectProperties()
        {
            // Arrange
            var competitionName = "Test Competition";
            var startDate = new System.DateTime(2022, 1, 1);
            var endDate = new System.DateTime(2022, 1, 2);
            var description = "Test competition description";
            var address = "Test competition address";
            var organizerName = "Test Organizer";
            var organizerWSID = "1234ABCD";

            // Act
            var competition = new Competition(competitionName, startDate, endDate, description, address)
            {
                Organizer = new Organizer(organizerWSID,organizerName)
            };

            // Assert
            Assert.Equal(competitionName, competition.Name);
            Assert.Equal(startDate, competition.StartDate);
            Assert.Equal(endDate, competition.EndDate);
            Assert.Equal(description, competition.Description);
            Assert.Equal(address, competition.Address);
            Assert.Equal(organizerName, competition.Organizer.Name);
            Assert.Equal(organizerWSID, competition.Organizer.WSID);
        }
    }
}
