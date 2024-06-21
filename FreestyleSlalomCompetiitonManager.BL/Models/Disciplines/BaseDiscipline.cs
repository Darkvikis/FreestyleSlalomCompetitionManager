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

        public virtual List<Competitor> Competitors { get; set; } = [];

        public virtual void AssignCompetitors(List<Competitor> skaters)
        {
            int count = 0;
            skaters.Where(s => s.AgeCategory == AgeCategory && s.SexCategory == SexCategory).ToList().ForEach(s => Competitors.Add(s));
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

        public string FullNameOfDiscipline()
        {
            return $"{ToString()} {AgeCategory} {SexCategory}";
        }

        public static bool IsInAgeCategory(AgeCategory disciplineCategory, AgeCategory skaterCategory)
        {
            if (disciplineCategory == skaterCategory)
            {
                return true;
            }
            if (disciplineCategory == AgeCategory.Mixed && (skaterCategory == AgeCategory.Junior || skaterCategory == AgeCategory.Senior))
            {
                return true;
            }
            return false;
        }

        public static bool IsInSexCategory(SexCategory disciplineCategory, SexCategory skaterCategory)
        {

            if (disciplineCategory == skaterCategory)
            {
                return true;
            }
            if (disciplineCategory == SexCategory.Mixed && (skaterCategory == SexCategory.Man || skaterCategory == SexCategory.Woman))
            {
                return true;
            }
            return false;
        }


        public abstract List<Competitor> GetResults();
        public abstract void ProcessDiscipline();
    }
}
