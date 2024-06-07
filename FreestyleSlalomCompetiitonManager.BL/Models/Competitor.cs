using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Competitor(string firstName, string familyName, string country) : BaseSkater(firstName, familyName, country)
    {
        public string? WSID { get; set; }
        public bool PayedFee { get; set; }
        public SendMusic SendMusic { get; set; }
        public string? Music { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ShirtSize { get; set; }
        public DateOnly? Birthdate { get; set; }
        public int? CompetitionRankBattle { get; set; }
        public int? CompetitionRankSpeed { get; set; }
        public int? CompetitionRankClassic { get; set; }
        public int? CompetitionRankJump { get; set; }
        public int? CompetitionResultBattle { get; set; }
        public int? CompetitionResultSpeed { get; set; }
        public int? CompetitionResultClassic { get; set; }
        public int? CompetitionResultJump { get; set; }

        public void SetMusic(string music, DateTime competitionDate)
        {
            Music = music;
            SendMusic = DateTime.Now.AddDays(7) >= competitionDate ? SendMusic.Late : SendMusic.OnTime;
        }
    }
}
