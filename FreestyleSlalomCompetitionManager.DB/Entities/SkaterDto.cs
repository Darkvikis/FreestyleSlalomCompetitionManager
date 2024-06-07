using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.DB.Entities
{
    public class SkaterDto (string firstname, string familyName, string countryName)
    {
        public string FirstName { get; set; } = firstname;
        public string FamilyName { get; set; } = familyName;
        public string Country { get; set; } = countryName;
        public int AgeCategory { get; set; }
        public int SexCategory { get; set; }

        public virtual List<>
    }
}
