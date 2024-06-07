using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Exports;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using OfficeOpenXml;
using System.IO;
using Xunit;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class ExportDisciplineStartingListTests : BaseTest
    {
        public const string folderPath = "..\\..\\..\\ExportedFiles\\";
        [Fact]
        public void Export_ShouldCreateExcelFileWithStartingList()
        {
            // Arrange
            var competitors = CreateListOfCompetitors(2, AgeCategory.Senior, SexCategory.Man);
            for (int i = 1; i <= competitors.Count; i++)
            {
                competitors[i - 1].CompetitionRankClassic = i;
            }

            var discipline = new Classic(AgeCategory.Senior,
                SexCategory.Man)
            {
                Competitors = competitors
            };
            var disciplineType = Discipline.Classic;

            string folderPath = Path.GetTempPath();
            // Act
            ExportDisciplineStartingList.Export(discipline, disciplineType, folderPath);

            // Assert
            var filePath = Path.Combine(folderPath, $"Results {disciplineType} {discipline.AgeCategory} {discipline.SexCategory}.xlsx");
            Assert.True(File.Exists(filePath));

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[disciplineType.ToString()];

                Assert.Equal($"{disciplineType} {discipline.AgeCategory} {discipline.SexCategory}", worksheet.Cells[2, 2].Value.ToString());
                Assert.Equal("WS ID", worksheet.Cells[3, 2].Value.ToString());
                Assert.Equal("NAME", worksheet.Cells[3, 3].Value.ToString());
                Assert.Equal("COUNTRY", worksheet.Cells[3, 4].Value.ToString());
                Assert.Equal("RANK", worksheet.Cells[3, 5].Value.ToString());

                Assert.Equal(competitors[0].WSID, worksheet.Cells[4, 2].Value.ToString());
                Assert.Equal(competitors[0].FirstName + " " + competitors[0].FamilyName, worksheet.Cells[4, 3].Value.ToString());
                Assert.Equal(competitors[0].Country, worksheet.Cells[4, 4].Value.ToString());
                Assert.Equal(competitors[0].CompetitionRankClassic.ToString(), worksheet.Cells[4, 5].Value.ToString());

                Assert.Equal(competitors[1].WSID, worksheet.Cells[5, 2].Value.ToString());
                Assert.Equal(competitors[1].FirstName + " " + competitors[1].FamilyName, worksheet.Cells[5, 3].Value.ToString());
                Assert.Equal(competitors[1].Country, worksheet.Cells[5, 4].Value.ToString());
                Assert.Equal(competitors[1].CompetitionRankClassic.ToString(), worksheet.Cells[5, 5].Value.ToString());
            }
        }
    }
}
