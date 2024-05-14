using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.Data.Enums;
using FreestyleSlalomCompetitionManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Impotrs
{

    public class ImportCSVWorldRankings
    {
        public static List<WorldRank> Import(string filePath)
        {
            List<WorldRank> worldRanks = new();

            // Extract category and discipline from file name
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] fileParts = fileName.Split('-');

            string disciplineStr = fileParts[1].Trim();
            string sexCategoryStr = fileParts[2].Trim();
            string ageCategoryStr = fileParts[3].Trim();

            // Map string values to enums
            Discipline discipline = EnumConventer.GetDisciplineFromString(disciplineStr);
            SexCategory sexCategory = EnumConventer.GetSexCategoryFromString(sexCategoryStr);
            AgeCategory ageCategory = EnumConventer.GetAgeCategoryFromString(ageCategoryStr);

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    // Skip header row
                    if (values[0] == "Rank")
                        continue;

                    Skater skater = new(values[3], values[4], values[5]);

                    WorldRank worldRank = new(skater.WSID, DateTime.Today)
                    {
                        Discipline = discipline,
                        AgeCategory = ageCategory,
                        SexCategory = sexCategory,
                        Rank = ushort.Parse(values[0])
                    };

                    worldRanks.Add(worldRank);
                }
            }

            return worldRanks;
        }
    }
}
