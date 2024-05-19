using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Enums;

namespace FreestyleSlalomCompetitionManager.BL.Impotrs
{
    public class ImportCSVIntoSkaterOnCompetition
    {
        public static List<SkaterOnCompetition> ImportCSV(string path)
        {
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var skaters = new List<SkaterOnCompetition>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    SkaterOnCompetition skater = new(csv.GetField(1) + " " + csv.GetField(2), csv.GetField(10))
                    {
                        WSID = csv.GetField(9),
                        PhoneNumber = csv.GetField(11),
                        Email = csv.GetField(12),
                        ShirtSize = csv.GetField(13),
                        PayedFee = false,
                        SendMusic = SendMusic.No,
                        Music = null,
                        CompetitionRankBattle = csv.GetField(3).Contains("Battle") ? int.MaxValue : null,
                        CompetitionRankSpeed = csv.GetField(6).Contains("Speed") ? int.MaxValue : null,
                        CompetitionRankClassic = csv.GetField(4).Contains("Classic") ? int.MaxValue : null,
                        CompetitionRankJump = csv.GetField(5).Contains("Jump") ? int.MaxValue : null,
                        AgeCategory = csv.GetField(7).Contains("Senior") ? AgeCategory.Senior : AgeCategory.Junior,
                        SexCategory = csv.GetField(7).Contains("women") ? SexCategory.Woman : SexCategory.Man,
                    };

                    skaters.Add(skater);
                }

                return skaters;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while importing the CSV file: {ex.Message}");
                return null;
            }
        }
    }
}