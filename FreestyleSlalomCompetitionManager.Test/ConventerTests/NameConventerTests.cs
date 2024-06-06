using FreestyleSlalomCompetitionManager.BL.Converters;


namespace FreestyleSlalomCompetitionManager.Test
{
    public class NameConverterTests
    {
        [Theory]
        [InlineData("Lucie Halámková ", "Lucie Halamkova")]
        [InlineData("Jana Kopáčová  ", "Jana Kopacova")]
        [InlineData("Natalia Langwińska ", "Natalia Langwinska")]
        [InlineData(" Agata Pantoł ", "Agata Pantol")]
        [InlineData("Anežka Krätschmer ", "Anezka Kratschmer")]
        [InlineData("Weronika Giża  ", "Weronika Giza")]
        [InlineData(" Přemysl Trš ", "Premysl Trs")]
        [InlineData("Simona Rožková ", "Simona Rozkova")]
        [InlineData(" Petr Vaněk  ", "Petr Vanek")]
        [InlineData("Yin-Hsuan Chiu ", "Yin-Hsuan Chiu")]
        public void NameWithoutDiacritics_ShouldRemoveDiacritics(string originalName, string convertedName)
        {

            // Act
            string result = NameConverter.NameWithoutDiacritics(originalName);

            // Assert
            Assert.Equal(convertedName, result);
        }


        [Fact]
        public void NateWithoutDiacritics_ShouldHandleEmptyString()
        {
            // Arrange
            string name = string.Empty;

            // Act
            string result = NameConverter.NameWithoutDiacritics(name);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("Czech", "CZE")]
        [InlineData("Poland", "POL")]
        [InlineData("Brazil", "BRA")]
        public void CountryToShortCut_ShouldReturnCountryShortcut(string country, string expected)
        {
            // Arrange

            // Act
            string result = NameConverter.CountryToShortCut(country);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
