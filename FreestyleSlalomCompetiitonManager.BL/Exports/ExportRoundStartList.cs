using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Exports
{
    public class ExportRoundStartList
    {
        public static async void ExportBattleRoundStartList(BattleRound round, string folderPath)
        {
            // Create a new Excel package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Skater registration data");

            worksheet.Cells[2, 2].Value = $"{round.Type} Battle";
            int groupNum = 1;
            foreach (var group in round.Groups)
            {
                worksheet.Cells[4, 2].Value = $"Group {groupNum++}";
                worksheet.Cells[5, 2].Value = "Start list";

                worksheet.Cells[6, 2].Value = "Start number";
                worksheet.Cells[6, 3].Value = "Name";
                worksheet.Cells[6, 4].Value = "Country";
                worksheet.Cells[6, 5].Value = "Rank";

                int row = 7;
                int skaterNum = 1;
                foreach (var competitor in group.Competitors)
                {
                    worksheet.Cells[row, 2].Value = skaterNum++;
                    worksheet.Cells[row, 3].Value = $"{competitor.Key.FirstName} {competitor.Key.FamilyName}";
                    worksheet.Cells[row, 4].Value = competitor.Key.Country;

                    row++;
                }

                worksheet.Cells[6, 2, row, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[6, 2, row, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Save the Excel file
                await package.SaveAsAsync($"{folderPath}/StartingList {round.Type} Battle - {DateTime.Today:dd-MM-yyyy}.xlsx");
            }
        }
    }
}
