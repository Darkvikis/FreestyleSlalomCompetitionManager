using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Exports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Models.Disciplines
{
    public abstract class BaseDiscipline(AgeCategory ageCategory, SexCategory sexCategory, Discipline disciplineType) : BaseModel
    {
        public AgeCategory AgeCategory { get; set; } = ageCategory;
        public SexCategory SexCategory { get; set; } = sexCategory;
        public Discipline DisciplineType { get; set; } = disciplineType;

        public virtual SortedDictionary<int, Competitor> Competitors { get; set; } = [];
        public virtual SortedDictionary<int, Competitor> Results { get; set; } = [];

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

        public virtual void ExportCompetitors(string folderPath)
        {
            ExportDisciplineStartingList.Export(this, DisciplineType, folderPath);
        }

    }
}
