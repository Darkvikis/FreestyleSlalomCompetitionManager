using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.DB.Entities
{
    public class OrganizerDto(string WSID, string name) : BaseEntity
    {
        public string WSID { get; set; } = WSID;
        public string Name { get; set; } = name;
    }
}
