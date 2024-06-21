using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Impotrs
{
    public class ImportCSVWorldRankings
    {
        public static async Task<List<WorldRank>> ImportFromFolderAsync(string folderPath, ConcurrentDictionary<string, Skater> existingSkaters)
        {
            List<WorldRank> allWorldRanks = [];


            DirectoryInfo directory = new(folderPath);

            if (!directory.Exists)
            {
                throw new Exception($"Directory {folderPath} not found. Please provide valid folder path to import from.");
            }

            foreach (FileInfo file in directory.GetFiles("*.csv"))
            {
                List<WorldRank> worldRanks = await ImportAsync(file.FullName, existingSkaters);

                allWorldRanks.AddRange(worldRanks);
            }

            foreach (Skater skater in existingSkaters.Values)
            {
                skater.WorldRanks.AddRange(allWorldRanks.Where(x =>x.WSID == skater.WSID));
            }
            return allWorldRanks;
        }

        public static async Task<List<WorldRank>> ImportAsync(string filePath, ConcurrentDictionary<string, Skater> existingSkaters)
        {
            if (!File.Exists(filePath))
                throw new Exception($"File {filePath} not found. Please provide valid file path to import from.");

            // Extract category and discipline from file name
            (Discipline discipline, SexCategory sexCategory, AgeCategory ageCategory) = ExtractFileParts(filePath);

            await TryToMoveWorldRanksDB(discipline, sexCategory, ageCategory);

            List<WorldRank> worldRanks = [];

            using (StreamReader reader = new(filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (IsHeaderRow(line))
                        continue;

                    string[] values = line.Replace("\"", "").Split(',');

                    string ranksWSID = values[5];

                    if(ranksWSID == "Davito Moci")
                    {
                        break;
                    }

                    Skater skater = GetOrCreateSkater(ranksWSID, values[3], values[4], ageCategory, sexCategory, existingSkaters);

                    WorldRank worldRank = CreateWorldRank(ranksWSID, ageCategory, sexCategory, discipline, ushort.Parse(values[0]));

                    AddWorldRankToWorldRanksList(worldRank, skater, worldRanks);
                }
            }

            await TryToSaveWorldRanks(worldRanks);
            return worldRanks;
        }

        private static (Discipline, SexCategory, AgeCategory) ExtractFileParts(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] fileParts = fileName.Split('-');

            if (fileParts.Length != 4)
                throw new FormatException($"File name {fileName} is not in the correct format. Please provide file names in the format: 'World Ranking March - <Discipline> - <SexCategory> - <AgeCategory>.csv'");

            string disciplineStr = fileParts[1].Trim();
            string sexCategoryStr = fileParts[2].Trim();
            string ageCategoryStr = fileParts[3].Trim();

            Discipline discipline = EnumConventer.GetDisciplineFromString(disciplineStr);
            SexCategory sexCategory = EnumConventer.GetSexCategoryFromString(sexCategoryStr);
            AgeCategory ageCategory = EnumConventer.GetAgeCategoryFromString(ageCategoryStr);

            return (discipline, sexCategory, ageCategory);
        }

        private static bool IsHeaderRow(string line)
        {
            return line.StartsWith("\"Rank\"");
        }

        private static Skater GetOrCreateSkater(string wsid, string name, string country, AgeCategory ageCategory, SexCategory sexCategory, ConcurrentDictionary<string, Skater> existingSkaters)
        {
            string[] nameParts = name.Split(' ');

            string firstName = string.Join(' ', nameParts[..^1]);
            string familyName = nameParts[^1];

            if (existingSkaters.TryGetValue(wsid, out Skater? skater))
            {
                if (skater.AgeCategory < ageCategory)
                {
                    skater.AgeCategory = ageCategory;
                }

                using var db = new DatabaseContext();
                if (!db.Skaters.Any(dbSkater => dbSkater.WSID == skater.WSID))
                {
                    db.Skaters.Update(skater);
                    db.SaveChanges();
                }
            }
            else
            {

                skater = new(wsid, firstName, familyName, country)
                {
                    AgeCategory = ageCategory,
                    SexCategory = sexCategory
                };

                using var db = new DatabaseContext();
                if (!db.Skaters.Any(dbSkater => dbSkater.WSID == skater.WSID))
                {
                    db.Skaters.Add(skater);
                    db.SaveChanges();
                }
            }

            return existingSkaters.GetOrAdd(wsid, _ => skater);
        }

        private static WorldRank CreateWorldRank(string wsid, AgeCategory ageCategory, SexCategory sexCategory, Discipline discipline, ushort rank)
        {
            return new WorldRank(wsid, DateTime.Today)
            {
                Discipline = discipline,
                AgeCategory = ageCategory,
                SexCategory = sexCategory,
                Rank = rank
            };
        }

        private static void AddWorldRankToWorldRanksList(WorldRank worldRank, Skater skater, List<WorldRank> worldRanks)
        {
            skater.WorldRanks.Add(worldRank);
            worldRanks.Add(worldRank);
        }

        private async static Task TryToMoveWorldRanksDB(Discipline discipline, SexCategory sexCategory, AgeCategory ageCategory)
        {
            using DatabaseContext? db = new();
            if (db == null) return;

            var ranks = await db.WorldRanks
                .Where(x => x.Discipline == discipline && x.SexCategory == sexCategory && x.AgeCategory == ageCategory)
                .ToListAsync<WorldRank>();

            if (ranks.Count > 0)
            {
                db.WorldRanksHistory.AddRange(ranks);
                db.WorldRanks.RemoveRange(ranks);
                await db.SaveChangesAsync();
            }
        }

        private async static Task TryToSaveWorldRanks(List<WorldRank> worldRanks)
        {
            using DatabaseContext? db = new();
            if (db == null) return;

            Discipline discipline = worldRanks[0].Discipline;
            SexCategory sexCategory = worldRanks[0].SexCategory;
            AgeCategory ageCategory = worldRanks[0].AgeCategory;

            var existingRanks = db?.WorldRanks
                .Where(x => x.Discipline == discipline && x.SexCategory == sexCategory && x.AgeCategory == ageCategory)
                .ToHashSet() ?? [];

            var newRanks = worldRanks
                .Where(rank => !existingRanks.Any(x => x.WSID == rank.WSID && x.Rank == rank.Rank))
                .ToList();

            await db.WorldRanks.AddRangeAsync(newRanks);
            await db.SaveChangesAsync();
        }

    }
}
