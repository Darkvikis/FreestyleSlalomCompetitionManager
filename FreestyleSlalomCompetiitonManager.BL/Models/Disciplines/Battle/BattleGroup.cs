﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle
{

    public class BattleGroup : BaseModel
    {
        public List<BattleGroupCompetitor> Competitors { get; set; } = [];
    }
}
