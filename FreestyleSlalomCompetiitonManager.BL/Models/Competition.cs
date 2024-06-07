using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Exports;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Competition(string name, DateTime startDate, DateTime endDate, string description, string address) : BaseModel
    {
        public string Name { get; set; } = name;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;
        public string Description { get; set; } = description;
        public string Address { get; set; } = address;
        public Organizer Organizer { get; set; }
        public virtual List<Competitor> Competitors { get; set; } = [];
        public virtual List<BaseDiscipline> Disciplines { get; set; } = [];

       

        public void RunCompetition()
        {
            if(Competitors.Count == 0 || Disciplines.Count == 0)
            {
                ConsoleCommunicator.CannotStartCompetition();
                return;
            }

            do
            {
                int discNum = ConsoleCommunicator.PickDiscipline(Disciplines);
                Disciplines[discNum].ProcessDiscipline();
            }
            while (ConsoleCommunicator.WantToContinue());
        }

        public async Task ExportResultsToCsv(string folderPath)
        {
            await ResultsExports.ExportResultsToExcel(this, folderPath);
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

        public void CreateDiscipline()
        {
            Disciplines.Add(ConsoleCommunicator.CreateDiscipline());
        }
    }
}

