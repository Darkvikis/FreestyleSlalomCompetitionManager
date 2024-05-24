using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic
{
    public class ClassicRun (Competitor competitor) : BaseModel
    {
        public Competitor Competitor { get; set; } = competitor;
        public Dictionary<string, int> Marks { get; set; } = [];
        public int Penalities { get; set; }
        public int FinalMark => (Marks.Sum(x => x.Value) / 3) - Penalities;
    }
}
