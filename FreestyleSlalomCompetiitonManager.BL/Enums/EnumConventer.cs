using FreestyleSlalomCompetitionManager.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Enums
{
    public static class EnumConventer
    {
        public static Discipline GetDisciplineFromString(string disciplineStr)
        {
            return disciplineStr.ToLower() switch
            {
                "classic" => Discipline.Classic,
                "battle" => Discipline.Battle,
                "speed" => Discipline.Speed,
                "slide" => Discipline.Slide,
                "jump" => Discipline.Jump,
                _ => throw new ArgumentException("Invalid discipline string."),
            };
        }

        public static SexCategory GetSexCategoryFromString(string sexCategoryStr)
        {
            return sexCategoryStr.ToLower() switch
            {
                "men" => SexCategory.Man,
                "women" => SexCategory.Woman,
                _ => throw new ArgumentException("Invalid sex category string."),
            };
        }

        public static AgeCategory GetAgeCategoryFromString(string ageCategoryStr)
        {
            // Assuming only Junior and Senior categories
            return ageCategoryStr.ToLower() switch
            {
                "junior" => AgeCategory.Junior,
                "senior" => AgeCategory.Senior,
                _ => throw new ArgumentException("Invalid age category string."),
            };
        }
    }
}
