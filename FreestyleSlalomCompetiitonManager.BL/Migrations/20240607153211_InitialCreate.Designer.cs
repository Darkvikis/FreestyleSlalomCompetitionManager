﻿// <auto-generated />
using System;
using FreestyleSlalomCompetitionManager.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FreestyleSlalomCompetitionManager.BL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240607153211_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Competitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("BaseDisciplineId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BattleRoundId")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ClassicRoundId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CompetitionRankBattle")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionRankClassic")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionRankJump")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionRankSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionResultBattle")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionResultClassic")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionResultJump")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionResultSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Music")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PayedFee")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("SendMusic")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SexCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShirtSize")
                        .HasColumnType("TEXT");

                    b.Property<string>("WSID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BaseDisciplineId");

                    b.HasIndex("BattleRoundId");

                    b.HasIndex("ClassicRoundId");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Competitors");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("DisciplineType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<int>("SexCategory")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("BaseDiscipline");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseDiscipline");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BattleGroups");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleGroupCompetitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BattleGroupId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CompetitorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("RankInGroup")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BattleGroupId");

                    b.HasIndex("CompetitorId");

                    b.ToTable("BattleGroupCompetitor");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleRound", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BattleRounds");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRound", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ClassicId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClassicId");

                    b.ToTable("ClassicRounds");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRun", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompetitorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("FinalMark")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitorId");

                    b.ToTable("ClassicRuns");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Organizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WSID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Skater", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SexCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WSID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skaters");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.WorldRank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<int>("Discipline")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SexCategory")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SkaterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WSID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SkaterId");

                    b.ToTable("WorldRank");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.Battle", b =>
                {
                    b.HasBaseType("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline");

                    b.HasDiscriminator().HasValue("Battle");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.Classic", b =>
                {
                    b.HasBaseType("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline");

                    b.HasDiscriminator().HasValue("Classic");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Competition", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Competitor", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline", null)
                        .WithMany("Competitors")
                        .HasForeignKey("BaseDisciplineId");

                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleRound", null)
                        .WithMany("Competitors")
                        .HasForeignKey("BattleRoundId");

                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRound", null)
                        .WithMany("Competitors")
                        .HasForeignKey("ClassicRoundId");

                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Competition", null)
                        .WithMany("Competitors")
                        .HasForeignKey("CompetitionId");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Competition", null)
                        .WithMany("Disciplines")
                        .HasForeignKey("CompetitionId");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleGroupCompetitor", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleGroup", null)
                        .WithMany("Competitors")
                        .HasForeignKey("BattleGroupId");

                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Competitor", "Competitor")
                        .WithMany()
                        .HasForeignKey("CompetitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competitor");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRound", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.Classic", null)
                        .WithMany("Rounds")
                        .HasForeignKey("ClassicId");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRun", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Competitor", "Competitor")
                        .WithMany()
                        .HasForeignKey("CompetitorId");

                    b.Navigation("Competitor");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.WorldRank", b =>
                {
                    b.HasOne("FreestyleSlalomCompetitionManager.BL.Models.Skater", null)
                        .WithMany("WorldRanks")
                        .HasForeignKey("SkaterId");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Competition", b =>
                {
                    b.Navigation("Competitors");

                    b.Navigation("Disciplines");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.BaseDiscipline", b =>
                {
                    b.Navigation("Competitors");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleGroup", b =>
                {
                    b.Navigation("Competitors");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle.BattleRound", b =>
                {
                    b.Navigation("Competitors");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.ClassicRound", b =>
                {
                    b.Navigation("Competitors");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Skater", b =>
                {
                    b.Navigation("WorldRanks");
                });

            modelBuilder.Entity("FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic.Classic", b =>
                {
                    b.Navigation("Rounds");
                });
#pragma warning restore 612, 618
        }
    }
}
