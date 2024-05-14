﻿using FreestyleSlalomCompetitionManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class SkaterOnCompetition : BaseModel
    {
        public required Skater Skater { get; set; }
        public bool PayedFee { get; set; }
        public SendMusic SendMusic { get; set; }
        public string? Music { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public int CompetitionRankBattle { get; set; }
        public int CompetitionRankSpeed { get; set; }
        public int CompetitionRankClassic { get; set; }
        public int CompetitionRankJump { get; set; }
        public int CompetitionRankSlide { get; set; }
    }
}
