using FreestyleSlalomCompetiitonManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetiitonManager.Data.Models
{
    public class Skater : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public string WSID { get; set; }
    }
}

