﻿using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Exports
{
    public class ResultsExports
    {
        public void ExportResultsToExcel(Competition competition)
        {
            // Create a new Excel package
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Skater registration data");

            worksheet.Cells[2, 2].Value = "NAME OF EVENT"; worksheet.Cells[2, 3].Value = competition.Name;
            worksheet.Cells[3, 2].Value = "PLACE OF EVENT"; worksheet.Cells[3, 3].Value = competition.Address;
            worksheet.Cells[4, 2].Value = "DATE"; worksheet.Cells[4, 3].Value = competition.StartDate.Date;
            worksheet.Cells[6, 2].Value = "REGISTRATION DATA";

            worksheet.Cells[7, 2].Value = "WORLD SKATE SKATER ID";
            worksheet.Cells[7, 3].Value = "NATIONAL FEDERATION ID";
            worksheet.Cells[7, 4].Value = "FIRST NAME";
            worksheet.Cells[7, 5].Value = "FAMILY NAME";
            worksheet.Cells[7, 6].Value = "GENDER";
            worksheet.Cells[7, 7].Value = "BIRTHDATE DD/MM/YYYY";
            worksheet.Cells[7, 8].Value = "NATIONALITY";
            worksheet.Cells[7, 9].Value = "PREVIOUS SKATER ID";

            // Set bold font for the 2nd column
            worksheet.Cells[2, 2, worksheet.Dimension.End.Row, 2].Style.Font.Bold = true;

            // Set bold font for the 7th row
            worksheet.Cells[7, 2, 7, worksheet.Dimension.End.Column].Style.Font.Bold = true;

            int row = 8;
            foreach (var competitor in competition.Competitors.Where(x => string.IsNullOrEmpty(x.WSID)))
            {
                worksheet.Cells[row, 2].Value = string.Empty;
                worksheet.Cells[row, 3].Value = string.Empty;
                worksheet.Cells[row, 4].Value = competitor.FirstName;
                worksheet.Cells[row, 5].Value = competitor.FamilyName;
                worksheet.Cells[row, 6].Value = competitor.SexCategory.ToString();
                worksheet.Cells[row, 7].Value = competitor.Birthdate?.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 8].Value = competitor.Country;
                worksheet.Cells[row, 9].Value = string.Empty;

                row++;
            }

            // Set border for the cells
            worksheet.Cells[8, 1, row, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            // Create a new worksheet for each discipline
            foreach (BaseDiscipline discipline in competition.Disciplines)
            {
                ExcelWorksheet disciplineWorksheet = package.Workbook.Worksheets.Add(discipline.ToString() + " " + discipline.AgeCategory.ToString()[0].ToString().ToUpper() + discipline.SexCategory.ToString()[0].ToString().ToUpper());
                // Add code here to populate the worksheet with discipline-specific data

                worksheet.Cells[2, 2].Value = "NAME OF EVENT";
                worksheet.Cells[2, 3].Value = competition.Name;

                worksheet.Cells[3, 2].Value = "PLACE OF EVENT";
                worksheet.Cells[3, 3].Value = competition.Address;

                worksheet.Cells[4, 2].Value = "DATE";
                worksheet.Cells[4, 3].Value = competition.StartDate.Date;

                worksheet.Cells[5, 2].Value = "DISCIPLINE";
                worksheet.Cells[5, 3].Value = discipline.ToString();

                worksheet.Cells[6, 2].Value = "CATEGORY";
                worksheet.Cells[6, 3].Value = discipline.AgeCategory + " " + discipline.SexCategory;

                // Set bold font for the 2nd column
                disciplineWorksheet.Cells[2, 2, disciplineWorksheet.Dimension.End.Row, 2].Style.Font.Bold = true;

                // Set bold font for the 9th row
                disciplineWorksheet.Cells[9, 2, 9, disciplineWorksheet.Dimension.End.Column].Style.Font.Bold = true;

                
                // Set the headers for the final results
                disciplineWorksheet.Cells[8, 1].Value = "FINAL RESULTS";
                disciplineWorksheet.Cells[9, 1].Value = "RANK";
                disciplineWorksheet.Cells[9, 2].Value = "WORLD SKATE SKATER ID";
                disciplineWorksheet.Cells[9, 3].Value = "NAME";
                disciplineWorksheet.Cells[9, 4].Value = "SURNAME";
                disciplineWorksheet.Cells[9, 5].Value = "COUNTRY";


                // Populate the final results
                int rank = 1;
                int resultsRow = 3;
                foreach (var result in discipline.Results)
                {
                    disciplineWorksheet.Cells[resultsRow, 1].Value = rank;
                    disciplineWorksheet.Cells[resultsRow, 2].Value = result.Value.WSID ?? string.Empty;
                    disciplineWorksheet.Cells[resultsRow, 3].Value = result.Value.FirstName;
                    disciplineWorksheet.Cells[resultsRow, 4].Value = result.Value.FamilyName;
                    disciplineWorksheet.Cells[resultsRow, 5].Value = result.Value.Country;

                    rank++;
                    resultsRow++;
                }

                // Set border for the cells
                disciplineWorksheet.Cells[8, 1, resultsRow, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            }

            // Save the Excel file
            package.SaveAs(new System.IO.FileInfo("Results.xlsx"));
        }
    }
}
