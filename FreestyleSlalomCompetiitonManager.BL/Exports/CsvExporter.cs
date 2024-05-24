using FreestyleSlalomCompetitionManager.BL.Models;
using System;
using System.IO;
using System.Text;

namespace FreestyleSlalomCompetitionManager.BL
{
    public static class CsvExporter
    {
        public static void ExportSkatersToCsv(Competition competition, string filePath)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Name,Country,WSID,PayedFee,SendMusic,Music,CompetitionRankBattle,CompetitionRankSpeed,CompetitionRankClassic,CompetitionRankJump,CompetitionRankSlide");

            foreach (var skater in competition.Competitors)
            {
                csv.AppendLine($"{skater.FirstName} + {skater.FamilyName},{skater.Country},{skater.WSID},{skater.PayedFee},{skater.SendMusic},{skater.Music},{skater.CompetitionRankBattle},{skater.CompetitionRankSpeed},{skater.CompetitionRankClassic},{skater.CompetitionRankJump},{skater.CompetitionRankSlide}");
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
