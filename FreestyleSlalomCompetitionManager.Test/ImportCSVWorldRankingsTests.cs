using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace FreestyleSlalomCompetitionManager.BL.Impotrs.Tests
{
    public class ImportCSVWorldRankingsTests
    {
        readonly string folderPath = "C:\\Users\\vkonupcik\\source\\repos\\FreestyleSlalomCompetitionManager\\FreestyleSlalomCompetitionManager.Test\\CSVsToTest";

        [Fact]
        public void Import_ValidFolder_ReturnsExpectedNumberOfWorldRanks()
        {
            List<Skater> existingSkaters = [];
            // Act
            var worldRanks = ImportCSVWorldRankings.ImportFromFolder(folderPath, existingSkaters);

            // Assert
            Assert.NotNull(worldRanks);
            Assert.NotEmpty(worldRanks);
            // Add more assertions as needed
        }

        [Theory]
        [InlineData("Classic", "Men", "Senior")]
        [InlineData("Battle", "Women", "Junior")]
        [InlineData("Speed", "Men", "Junior")]
        public void Import_CorrectCategoriesAndDisciplines(string disciplineStr, string sexCategoryStr, string ageCategoryStr)
        {
            // Arrange
            string fileName = $"World Ranking March 2024 - {disciplineStr} - {sexCategoryStr} - {ageCategoryStr}.csv";
            string filePath = Path.Combine(folderPath, fileName);
            List<Skater> existingSkaters = [];

            // Act
            var worldRanks = ImportCSVWorldRankings.Import(filePath, existingSkaters);

            // Assert
            Assert.NotNull(worldRanks);
            Assert.NotEmpty(worldRanks);
            Assert.All(worldRanks, rank =>
            {
                Assert.Equal(EnumConventer.GetDisciplineFromString(disciplineStr), rank.Discipline);
                Assert.Equal(EnumConventer.GetSexCategoryFromString(sexCategoryStr), rank.SexCategory);
                Assert.Equal(EnumConventer.GetAgeCategoryFromString(ageCategoryStr), rank.AgeCategory);
            });
        }
    }
}