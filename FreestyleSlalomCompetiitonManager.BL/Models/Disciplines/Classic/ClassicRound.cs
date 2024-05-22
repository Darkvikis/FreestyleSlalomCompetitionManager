using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic
{
    public class ClassicRound : BaseRound
    {
        public int Number { get; set; }
        public List<ClassicRun> Runs = [];
    }
}
