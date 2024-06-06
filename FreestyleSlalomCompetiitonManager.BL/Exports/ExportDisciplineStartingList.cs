﻿using FreestyleSlalomCompetitionManager.BL.Enums;
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
        public static void Export(BaseDiscipline discipline, Discipline disciplineType)
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
            int row = 4;
            foreach (var competitor in discipline.Competitors)
            {
                worksheet.Cells[row, 2].Value = competitor.Value.WSID;
                worksheet.Cells[row, 3].Value = $"{competitor.Value.FirstName} {competitor.Value.FamilyName}";
                worksheet.Cells[row, 4].Value = competitor.Value.Country;
                worksheet.Cells[row, 5].Value = competitor.Key;

                row++;
            }

            // Set border for the cells
            worksheet.Cells[2, 2, row, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            worksheet.Cells[2, 2, row, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            // Auto-fit column widths
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }
    }
}