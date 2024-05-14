using FreestyleSlalomCompetitionManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class WorldRank (string WSID, DateTime dateAdded) : BaseModel
    {
        public string WSID { get; set; } = WSID;
        public Discipline Discipline { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public ushort Rank { get; set; }
        public DateTime DateAdded { get; set; } = dateAdded;
    }
}
