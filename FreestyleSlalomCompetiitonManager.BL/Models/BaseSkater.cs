﻿using FreestyleSlalomCompetitionManager.BL.Converters;
using FreestyleSlalomCompetitionManager.BL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreestyleSlalomCompetitionManager.BL.Models
{
    [PrimaryKey(nameof(WSID))]
    public class BaseSkater(string WSID, string firstName, string secondName, string country)
    {
        public string WSID { get; set; } = WSID;
        public string FirstName { get; set; } = NameConverter.NameWithoutDiacritics(firstName);
        public string FamilyName { get; set; } = NameConverter.NameWithoutDiacritics(secondName);
        public string Country { get; set; } = NameConverter.CountryToShortCut(country);
        public AgeCategory AgeCategory { get; set; }
        public SexCategory SexCategory { get; set; }
    }
}
