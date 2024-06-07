using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.Test.ModelTests.DisciplinesTests
{
    public class BattleTests : BaseTest
    {
        [Fact]
        public void AssignCompetitors_ShouldAssignCompetitorsToBattle()
        {
            // Arrange
            var battle = new Battle(AgeCategory.Senior, SexCategory.Man);
            var skaters = CreateListOfCompetitors(4, AgeCategory.Senior, SexCategory.Man);

            skaters.ForEach(s => s.CompetitionRankBattle = skaters.IndexOf(s) + 1);
            // Act
            battle.AssignCompetitors(skaters);

            // Assert
            Assert.Equal(4, battle.Competitors.Count);
            Assert.Equal(1, battle.Competitors[0].CompetitionRankBattle);
            Assert.Equal(2, battle.Competitors[1].CompetitionRankBattle);
            Assert.Equal(3, battle.Competitors[2].CompetitionRankBattle);
            Assert.Equal(4, battle.Competitors[3].CompetitionRankBattle);
        }

        [Fact]
        public void InitializeBattle_ShouldAddNewBattleRoundWithCompetitors()
        {
            // Arrange
            var battle = new Battle(AgeCategory.Senior, SexCategory.Man);
            var skaters = CreateListOfCompetitors(8, AgeCategory.Senior, SexCategory.Man);
            skaters.ForEach(s => s.CompetitionRankBattle = skaters.IndexOf(s) + 1);

            battle.AssignCompetitors(skaters);

            // Act
            battle.InitializeBattle();

            // Assert
            Assert.Single(battle.Rounds);

            Assert.Equal(1, battle.Rounds[0].Groups[0].Competitors.First(x => x.Competitor.Equals(skaters[0])).RankInGroup);
            Assert.Equal(2, battle.Rounds[0].Groups[1].Competitors.First(x => x.Competitor.Equals(skaters[1])).RankInGroup);
            Assert.Equal(3, battle.Rounds[0].Groups[1].Competitors.First(x => x.Competitor.Equals(skaters[2])).RankInGroup);
            Assert.Equal(4, battle.Rounds[0].Groups[0].Competitors.First(x => x.Competitor.Equals(skaters[3])).RankInGroup);
                                                                               
            Assert.Equal(5, battle.Rounds[0].Groups[0].Competitors.First(x => x.Competitor.Equals(skaters[4])).RankInGroup);
            Assert.Equal(6, battle.Rounds[0].Groups[1].Competitors.First(x => x.Competitor.Equals(skaters[5])).RankInGroup);
            Assert.Equal(7, battle.Rounds[0].Groups[1].Competitors.First(x => x.Competitor.Equals(skaters[6])).RankInGroup);
            Assert.Equal(8, battle.Rounds[0].Groups[0].Competitors.First(x => x.Competitor.Equals(skaters[7])).RankInGroup);
        }
    }
}
