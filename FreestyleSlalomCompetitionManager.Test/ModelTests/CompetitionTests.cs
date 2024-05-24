using FreestyleSlalomCompetitionManager.BL.Exports;
using FreestyleSlalomCompetitionManager.BL.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.Threading.Tasks;


namespace FreestyleSlalomCompetitionManager.Test.ModelTests
{
    public class CompetitionTests : BaseTest
    {
        public const string folderPath = "..\\..\\..\\ExportedFiles\\";

        [Fact]
        public void ExportResultsToCsv_ShouldCallResultsExports_ExportResultsToExcel()
        {
            // Arrange
            var competition = CreateCompetition();
            var competitors = CreateListOfCompetitors(10);

            competitors[3].WSID = null;
            competitors[7].WSID = null;

            competition.AssignCompetitors(competitors);
            competition.Disciplines.Add(CreateClassic());

            competition.AssignCompetitorsToDisciplines();

            for (int i = 0; i < 5; i++)
            {
                competition.Disciplines[0].Results.Add(i, competitors[i]);
            }
            try
            {


                // Delete all files in a folder
                DirectoryInfo directory = new(folderPath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                // Delete all subfolders in a folder
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }
            }
            catch (Exception)
            {
                
            }
            // Act
            competition.ExportResultsToCsv(folderPath);

            // Assert
            Assert.True(Directory.GetFiles(folderPath).Length == 1);
            Assert.True(File.Exists($"{folderPath}/Results {competition.Name} - {DateTime.Today:dd-MM-yyyy}.xlsx"));

            string filePath = $"{folderPath}/Results {competition.Name} - {DateTime.Today:dd-MM-yyyy}.xlsx";
            using ExcelPackage package = new(new FileInfo(filePath));
            int worksheetCount = package.Workbook.Worksheets.Count;
            Assert.True(worksheetCount == 2);
        }
    }
}
