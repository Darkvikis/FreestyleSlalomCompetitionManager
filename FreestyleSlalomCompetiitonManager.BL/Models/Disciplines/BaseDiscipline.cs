using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public class BaseDiscipline (AgeCategory ageCategory, SexCategory sexCategory) : BaseModel
    {
        public AgeCategory AgeCategory { get; set; } = ageCategory;
        public SexCategory SexCategory { get; set; } = sexCategory;
        public SortedDictionary<int, Competitor> Competitors { get; set; } = [];
        public SortedDictionary<int, Competitor> Results { get; set; } = [];
        public virtual void AssignCompetitors(List<Competitor> skaters)
        {
            int count = 0;
            skaters.Where(s => s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).ToList().ForEach(s => Competitors.Add(count++, s));
        }

        public virtual void AssignResults(SortedDictionary<int, Competitor> results)
        {
            Results = results;
        }

        public virtual void ClearResults()
        {
            Results.Clear();
        }

        public virtual void ClearCompetitors()
        {
            Competitors.Clear();
        }

        public virtual int GetRank(int? allegedRank)
        {
            return allegedRank ?? default;
        }
    }
}
