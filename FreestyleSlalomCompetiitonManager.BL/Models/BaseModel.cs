﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
