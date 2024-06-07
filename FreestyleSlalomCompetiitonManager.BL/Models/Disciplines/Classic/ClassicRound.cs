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
            foreach (var competitor in Competitors)
            {
                ClassicRun run = new ClassicRun() { Competitor = competitor};
                run.FinalMark = ConsoleCommunicator.ClassicRunGetMark(run.Competitor);
                Runs.Add(run);
            }

        }

    }
}
