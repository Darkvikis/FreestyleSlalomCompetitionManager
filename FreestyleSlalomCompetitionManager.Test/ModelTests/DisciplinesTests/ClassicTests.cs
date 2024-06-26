﻿using Bogus;
using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Test.ModelTests.DisciplinesTests
{
    public class ClassicTests : BaseTest
    {
        private readonly Faker faker = new();

        [Theory]
        [InlineData(2, 4, 2, 6)]
        [InlineData(2, 5, 3, 8)]
        [InlineData(2, 5, 3, 14)]
        [InlineData(2, 6, 4, 10)]
        public void GenerateRounds_ValidInputParameters_AddsRoundsToList(int numberOfPrequalified, int maxNumberOfSkatersInGroup, int numberOfQualificationGroups, int numberOfSkaters)
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);
            for (int i = 0; i < numberOfSkaters; i++)
            {
                var competitor = CreateCompetitor(null, null);
                competitor.CompetitionRankClassic = i;
                classic.Competitors.Add(competitor);
            }

            // Act
            classic.GenerateRounds(numberOfPrequalified, maxNumberOfSkatersInGroup, numberOfQualificationGroups);

            // Assert
            Assert.Equal(numberOfQualificationGroups + 1, classic.Rounds.Count);
            Assert.Contains(classic.Rounds[0].Competitors, x => x.CompetitionRankClassic == 0);
            Assert.Contains(classic.Rounds[0].Competitors, c => c.CompetitionRankClassic == 1);

            if (numberOfSkaters == 6)
            {
                Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 2);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 5);
                Assert.Contains(classic.Rounds[2].Competitors, x => x.CompetitionRankClassic == 3);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 4);
            }
            else if (numberOfSkaters == 8)
            {
                Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 2);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 7);
                Assert.Contains(classic.Rounds[2].Competitors, x => x.CompetitionRankClassic == 3);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 6);
                Assert.Contains(classic.Rounds[3].Competitors, x => x.CompetitionRankClassic == 4);
                Assert.Contains(classic.Rounds[3].Competitors, c => c.CompetitionRankClassic == 5);
            }
            else if (numberOfSkaters == 10)
            {
                Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 2);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 9);
                Assert.Contains(classic.Rounds[2].Competitors, x => x.CompetitionRankClassic == 3);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 8);
                Assert.Contains(classic.Rounds[3].Competitors, x => x.CompetitionRankClassic == 4);
                Assert.Contains(classic.Rounds[3].Competitors, c => c.CompetitionRankClassic == 7);
                Assert.Contains(classic.Rounds[4].Competitors, x => x.CompetitionRankClassic == 5);
                Assert.Contains(classic.Rounds[4].Competitors, c => c.CompetitionRankClassic == 6);
            }
            else
            {
                Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 2);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 7);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 8);
                Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 13);
                Assert.Contains(classic.Rounds[2].Competitors, x => x.CompetitionRankClassic == 3);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 6);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 9);
                Assert.Contains(classic.Rounds[2].Competitors, c => c.CompetitionRankClassic == 12);
                Assert.Contains(classic.Rounds[3].Competitors, x => x.CompetitionRankClassic == 4);
                Assert.Contains(classic.Rounds[3].Competitors, c => c.CompetitionRankClassic == 5);
                Assert.Contains(classic.Rounds[3].Competitors, c => c.CompetitionRankClassic == 10);
                Assert.Contains(classic.Rounds[3].Competitors, c => c.CompetitionRankClassic == 11);
            }
        }

        [Fact]
        public void GenerateRounds_InvalidInputParameters_ThrowsArgumentException()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man)
            {
                Competitors = CreateListOfCompetitors(5, AgeCategory.Senior, SexCategory.Man)
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => classic.GenerateRounds(0, 3));
        }

        [Fact]
        public void AssignCompetitors_ValidInputParameters_AddsCompetitorsToList()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);
            var skaters = CreateListOfCompetitors(3, AgeCategory.Senior, SexCategory.Man);
            for (int i = 0; i < skaters.Count; i++)
            {
                skaters[i].CompetitionRankClassic = i;
            }
            // Act
            classic.AssignCompetitors(skaters);

            // Assert
            Assert.Equal(3, classic.Competitors.Count);
        }

        [Fact]
        public void ValidateInputParameters_NumberOfPrequalifiedLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => classic.GenerateRounds(0, 4, 2));
        }

        [Fact]
        public void ValidateInputParameters_MaxNumberOfSkatersInGroupLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => classic.GenerateRounds(2, 0, 2));
        }

        [Fact]
        public void ValidateInputParameters_NumberOfPrequalifiedGreaterThanMaxNumberOfSkatersInGroup_ThrowsArgumentException()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => classic.GenerateRounds(4, 2, 2));
        }

        [Fact]
        public void ValidateInputParameters_DifferenceBetweenMaxNumberOfSkatersInGroupAndNumberOfPrequalifiedLessThanNumberOfQualificationGroups_ThrowsArgumentException()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => classic.GenerateRounds(2, 4, 3));
        }

        [Fact]
        public void GenerateRoundsOneQualification_ValidInputParameters_AddsRoundsToList()
        {
            // Arrange
            var classic = new Classic(AgeCategory.Senior, SexCategory.Man);
            for (int i = 0; i < 6; i++)
            {
                var competitor = CreateCompetitor(null, null);
                competitor.CompetitionRankClassic = i;
                classic.Competitors.Add(competitor);
            }

            // Act
            classic.GenerateRounds(2, 3);

            // Assert
            Assert.Equal(2, classic.Rounds.Count);
            Assert.Contains(classic.Rounds[0].Competitors, x => x.CompetitionRankClassic == 0);
            Assert.Contains(classic.Rounds[0].Competitors, c => c.CompetitionRankClassic == 1);

            Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 2);
            Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 5);
            Assert.Contains(classic.Rounds[1].Competitors, x => x.CompetitionRankClassic == 3);
            Assert.Contains(classic.Rounds[1].Competitors, c => c.CompetitionRankClassic == 4);

        }
    }
}
