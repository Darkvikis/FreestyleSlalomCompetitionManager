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
    public class BaseSkater(string name, string country) : BaseModel
    {
        public string Name { get; set; } = name;
        public string Country { get; set; } = country;
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
    }
}
