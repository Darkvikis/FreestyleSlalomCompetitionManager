using FreestyleSlalomCompetiitonManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FreestyleSlalomCompetiitonManager.Data.Models
{
    public class WorldRank : BaseModel
    {
        public Guid SkaterId { get; set; }
        public Discipline Discipline { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
        public ushort Rank { get; set; }
        public Guid DateAdded { get; set; }
    }
}
