using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class BaseDiscipline : BaseModel
    {
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public List<Competitor> Competitors { get; set; } = [];
        public Dictionary<Competitor, int> Results { get; set; } = [];
        public virtual void AssignCompetitiors(List<Competitor> skaters)
        {
        }
    }
}
