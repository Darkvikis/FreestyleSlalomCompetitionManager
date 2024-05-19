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
                    SkaterOnCompetition skater = new(csv.GetField("Name/Jméno") + " " + csv.GetField("\"Surname/Příjmení"), csv.GetField("Nationality/Národnost"))
                    {
                        WSID = csv.GetField("WSK ID"),
                        PhoneNumber = csv.GetField("Phone number /Telefonní číslo"),
                        Email = csv.GetField("Email address /Email"),
                        ShirtSize = csv.GetField("T-shirt size/Velikost trička"),
                        PayedFee = false,
                        SendMusic = SendMusic.No,
                        Music = null,
                        CompetitionRankBattle = csv.GetField("Freestyle Battle").Contains("Battle") ? int.MaxValue : null,
                        CompetitionRankSpeed = csv.GetField("Speed slalom").Contains("Speed") ? int.MaxValue : null,
                        CompetitionRankClassic = csv.GetField("Freestyle Classic").Contains("Classic") ? int.MaxValue : null,
                        CompetitionRankJump = csv.GetField("Free Jump").Contains("Jump") ? int.MaxValue : null
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