using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class Organizer (string WSID, string name) : BaseModel
    {
        public string WSID { get; set; } = WSID;
        public string Name { get; set; } = name;
    }
}
