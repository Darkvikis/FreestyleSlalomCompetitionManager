using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Categories
{
    public class BaseCategory : BaseModel
    {
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
    }
}
