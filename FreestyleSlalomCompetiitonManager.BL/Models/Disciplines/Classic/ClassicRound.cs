using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic
{
    public class ClassicRound : BaseRound
    {
        public List<ClassicRun> Runs = [];

        public void ProcessClassicRound()
        {
            foreach (ClassicRun run in Runs)
            {
                run.FinalMark = ConsoleCommunicator.ClassicRunGetMark(run.Competitor);
            }

        }
    }
}
