using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Data.Models
{
    public class Organizer (int WSID, string name) : BaseModel
    {
        int WSID { get; set; } = WSID;
        string Name { get; set; } = name;
    }
}
