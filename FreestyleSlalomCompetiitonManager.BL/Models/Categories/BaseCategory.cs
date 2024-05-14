using FreestyleSlalomCompetiitonManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetiitonManager.Data.Models.Categories
{
    public class BaseCategory : BaseModel
    {
        public Competition Competition { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
    }
}
