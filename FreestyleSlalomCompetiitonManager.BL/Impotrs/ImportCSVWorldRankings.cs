﻿using FreestyleSlalomCompetitionManager.BL.Enums;
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
        public static async Task<List<WorldRank>> ImportFromFolderAsync(string folderPath, ConcurrentDictionary<string, Skater> existingSkaters, DatabaseContext? db = null)
        {
            List<WorldRank> allWorldRanks = [];


            DirectoryInfo directory = new(folderPath);

            if (!directory.Exists)
            {
                throw new Exception($"Directory {folderPath} not found. Please provide valid folder path to import from.");
            }

            foreach (FileInfo file in directory.GetFiles("*.csv"))
            {
                List<WorldRank> worldRanks = await ImportAsync(file.FullName, existingSkaters, db);

                allWorldRanks.AddRange(worldRanks);
            }

            return allWorldRanks;
        }

        public static async Task<List<WorldRank>> ImportAsync(string filePath, ConcurrentDictionary<string, Skater> existingSkaters, DatabaseContext? db = null)
        {
            if (!File.Exists(filePath))
                throw new Exception($"File {filePath} not found. Please provide valid file path to import from.");

            // Extract category and discipline from file name
            (Discipline discipline, SexCategory sexCategory, AgeCategory ageCategory) = ExtractFileParts(filePath);

            await TryToMoveWorldRanksDB(db, discipline, sexCategory, ageCategory);

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

                    Skater skater = GetOrCreateSkater(ranksWSID, values[3], values[4], ageCategory, sexCategory, existingSkaters);

                    WorldRank worldRank = CreateWorldRank(ranksWSID, ageCategory, sexCategory, discipline, ushort.Parse(values[0]));

                    AddWorldRankToWorldRanksList(worldRank, skater, worldRanks);
                }
            }

            await TryToSaveWorldRanks(db, worldRanks);
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
            return existingSkaters.GetOrAdd(wsid, _ => new Skater(firstName, familyName, country, wsid)
            {
                AgeCategory = ageCategory,
                SexCategory = sexCategory
            });
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

        private async static Task TryToMoveWorldRanksDB(DatabaseContext? db, Discipline discipline, SexCategory sexCategory, AgeCategory ageCategory)
        {
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

        private async static Task TryToSaveWorldRanks(DatabaseContext? db, List<WorldRank> worldRanks)
        {
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
