using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Impotrs
{
    public partial class ImportStartingListXLSX
    {
        public static async Task ImportExcelFile(string filePath)
        {
            using ExcelPackage package = new(new FileInfo(filePath));
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            // Read the data from the Excel file
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = worksheet.Dimension.Columns;
            List<Competitor> competitors = [];

            string discipline = DisciplineType().Match(filePath).Groups[0].Value;

            for (int row = 4; row <= rowCount; row++)
            {
                string firstName = worksheet.Cells[row, 3].Value?.ToString()?.Split(' ')[0] ?? string.Empty;
                string familyName = worksheet.Cells[row, 3].Value?.ToString()?.Split(' ')[1] ?? string.Empty;
                string country = worksheet.Cells[row, 4].Value?.ToString() ?? string.Empty;
                string wsid = worksheet.Cells[row, 2].Value.ToString();
                if (wsid != null)
                {
                    Competitor competitor = new(wsid, firstName, familyName, country);
                    if (int.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out int rank))
                    {
                        AssignRankToCompetitor(competitor, discipline, rank);
                    }
                    competitors.Add(competitor);
                }
                else
                {
                    ConsoleCommunicator.InvalidSkaterMissingWSID();
                }



            }

        }

        public static void AssignRankToCompetitor(Competitor competitor, string discipline, int rank)
        {
            switch (discipline.ToLower())
            {
                case "battle":
                    competitor.CompetitionRankBattle = rank;
                    break;
                case "classic":
                    competitor.CompetitionRankClassic = rank;
                    break;
                case "speed":
                    competitor.CompetitionRankSpeed = rank;
                    break;
                case "jump":
                    competitor.CompetitionRankJump = rank;
                    break;
            }
        }

        [GeneratedRegex(@"Results (.*?)")]
        private static partial Regex DisciplineType();
    }
}
