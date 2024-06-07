using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<WorldRank> WorldRanks { get; set; }
        public DbSet<WorldRank> WorldRanksHistory { get; set; }
        public DbSet<Skater> Skaters { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Competitor> Competitors { get; set; }

        public DbSet<Battle> Battles { get; set; }
        public DbSet<BattleRound> BattleRounds { get; set; }
        public DbSet<BattleGroup> BattleGroups { get; set; }

        public DbSet<Classic> Classics { get; set; }
        public DbSet<ClassicRound> ClassicRounds { get; set; }
        public DbSet<ClassicRun> ClassicRuns { get; set; }
        public string DbPath { get; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "freestyle.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

    }
}
