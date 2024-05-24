using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class BaseSkater(string firstName, string secondName, string country) : BaseModel
    {
        public string FirstName { get; set; } = firstName;
        public string FamilyName { get; set; } = secondName;
        public string Country { get; set; } = country;
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
    }
}
