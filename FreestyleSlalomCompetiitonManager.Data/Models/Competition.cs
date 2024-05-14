using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace FreestyleSlalomCompetiitonManager.Data.Models
{
    public class Competition : BaseModel
    {
        public string Name { get; set; }
        public Date StartDate { get; set; }
        public Date FinishDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Organizer Organizer { get; set; }

    }
}
