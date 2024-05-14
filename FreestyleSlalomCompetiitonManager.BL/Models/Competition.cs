using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class Competition(string name, Date startDate, Date endDate, string description, string address, Organizer organizer) : BaseModel
    {
        public string Name { get; set; } = name;
        public Date StartDate { get; set; } = startDate;
        public Date EndDate { get; set; } = endDate;
        public string Description { get; set; } = description;
        public string Address { get; set; } = address;
        public Organizer Organizer { get; set; } = organizer;

    }
}
