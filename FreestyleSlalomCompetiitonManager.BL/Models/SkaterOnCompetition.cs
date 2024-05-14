using FreestyleSlalomCompetitionManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class SkaterOnCompetition(Skater skater) : BaseModel
    {
        public required Skater Skater { get; set; } = skater;
        public bool PayedFee { get; set; }
        public SendMusic SendMusic { get; set; }
        public string? Music { get; set; }
        public AgeCategory AgeCategory { get; set; } = skater.AgeCategory;
        public SexCategory SexCategory { get; set; } = skater.SexCategory;
        public int CompetitionRankBattle { get; set; }
        public int CompetitionRankSpeed { get; set; }
        public int CompetitionRankClassic { get; set; }
        public int CompetitionRankJump { get; set; }
        public int CompetitionRankSlide { get; set; }

        public void SetMusic(string music, DateTime competitionDate)
        {
            Music = music;
            if (competitionDate < DateTime.Now.AddDays(-7))
            {
                SendMusic = SendMusic.OnTime;
            }
            else
            {
                SendMusic = SendMusic.Late;
            }
        }
    }
}
