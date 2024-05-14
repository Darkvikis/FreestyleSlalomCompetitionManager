using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class Competition(string name, DateTime startDate, DateTime endDate, string description, string address, Organizer organizer) : BaseModel
    {
        public string Name { get; set; } = name;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;
        public string Description { get; set; } = description;
        public string Address { get; set; } = address;
        public Organizer Organizer { get; set; } = organizer;
        public List<SkaterOnCompetition> Skaters { get; set; } = [];
    }
}
