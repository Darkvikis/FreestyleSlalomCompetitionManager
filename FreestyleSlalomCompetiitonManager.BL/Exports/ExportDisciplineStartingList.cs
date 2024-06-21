using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Exports
{
    public static class ExportDisciplineStartingList
    {
        public static async void Export(BaseDiscipline discipline, Discipline disciplineType, string folderPath)
        {
            // Create a new Excel package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(disciplineType.ToString());

            worksheet.Cells[2, 2, 2, 5].Merge = true;

            worksheet.Cells[2, 2].Value = $"{disciplineType} {discipline.AgeCategory} {discipline.SexCategory}";

            worksheet.Cells[3, 2].Value = "WS ID";
            worksheet.Cells[3, 3].Value = "NAME";
            worksheet.Cells[3, 4].Value = "COUNTRY";
            worksheet.Cells[3, 5].Value = "RANK";
            int row = 3;
            int counter = 1;
            foreach (var competitor in discipline.Competitors)
            {
                row++;
                worksheet.Cells[row, 2].Value = competitor.WSID.ToUpper();
                worksheet.Cells[row, 3].Value = $"{competitor.FamilyName} {competitor.FirstName}";
                worksheet.Cells[row, 4].Value = competitor.Country;
                worksheet.Cells[row, 5].Value = GetRankForDiscipline(disciplineType, competitor).ToUpper();

            }

            // Set border for the cells
            worksheet.Cells[2, 2, row, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            // Auto-fit column widths
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Save the Excel file
            await package.SaveAsAsync($"{folderPath}/StartingList{disciplineType}{discipline.AgeCategory}{discipline.SexCategory}.xlsx");

        }

        private static string GetRankForDiscipline(Discipline disciplineType, Competitor competitor)
        {
            var rank = disciplineType switch
            {
                Discipline.Battle => competitor.CompetitionRankBattle ?? int.MaxValue,
                Discipline.Classic => competitor.CompetitionRankClassic ?? int.MaxValue,
                Discipline.Speed => competitor.CompetitionRankSpeed ?? int.MaxValue,
                Discipline.Jump => competitor.CompetitionRankJump ?? int.MaxValue,
                _ => int.MaxValue
            };

            if (rank == int.MaxValue) return "none";
            return rank.ToString();
        }
    }
}
