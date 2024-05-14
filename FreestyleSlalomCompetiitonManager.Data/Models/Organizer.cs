using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetiitonManager.Data.Models
{
    public class Organizer : BaseModel
    {
        int WSID { get; set; }
        string Name { get; set; }
    }
}
