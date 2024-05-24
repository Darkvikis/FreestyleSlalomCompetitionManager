﻿using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Exports;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Competition(string name, DateTime startDate, DateTime endDate, string description, string address, Organizer organizer) : BaseModel
    {
        public string Name { get; set; } = name;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;
        public string Description { get; set; } = description;
        public string Address { get; set; } = address;
        public Organizer Organizer { get; set; } = organizer;
        public List<Competitor> Competitors { get; set; } = [];
        public List<BaseDiscipline> Disciplines { get; set; } = [];

        public void ExportResultsToCsv(string folderPath)
        {
            ResultsExports.ExportResultsToExcel(this, folderPath);
        }

        public void AssignCompetitors(List<Competitor> competitors)
        {
            Competitors = competitors;
        }

        public void AssignCompetitorsToDisciplines()
        {
            foreach (var discipline in Disciplines ?? Enumerable.Empty<BaseDiscipline>())
            {
                discipline?.AssignCompetitors(Competitors ?? []);
            }
        }
    }
}

