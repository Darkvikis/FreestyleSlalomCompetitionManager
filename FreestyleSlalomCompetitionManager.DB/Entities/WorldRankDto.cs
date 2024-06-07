using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.DB.Entities
{
    public class WorldRankDto(string WSID, DateTime dateAdded) : BaseEntity
    {
        public string WSID { get; set; } = WSID;
        public int Discipline { get; set; }
        public int AgeCategory { get; set; }
        public int SexCategory { get; set; }
        public ushort Rank { get; set; }
        public DateTime DateAdded { get; set; } = dateAdded;
    }
}
