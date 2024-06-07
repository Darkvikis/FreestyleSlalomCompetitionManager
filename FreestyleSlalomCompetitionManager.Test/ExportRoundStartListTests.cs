using FreestyleSlalomCompetitionManager.BL.Exports;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class ExportRoundStartListTests :BaseTest
    {
        [Fact]
        public void ExportBattleRoundStartList_ShouldCreateExcelFile()
        {
            // Arrange
            BattleRound round = new BattleRound
            {
                Type = BL.Enums.Round.Final,
                Groups = new List<BattleGroup>
                {
                    new BattleGroup
                    {
                        Competitors = new List<BattleGroupCompetitor>
                        {
                            new BattleGroupCompetitor
                            {
                                Competitor = CreateCompetitor(null, null)
                            },
                            new BattleGroupCompetitor
                            {
                                Competitor = CreateCompetitor(null, null)
                            }
                        }
                    }
                }
            };

            string folderPath = Path.GetTempPath();

            // Act
            ExportRoundStartList.ExportBattleRoundStartList(round, folderPath);

            // Assert
            string filePath = Path.Combine(folderPath, $"StartingList {round.Type} Battle - {DateTime.Today:dd-MM-yyyy}.xlsx");
            Assert.True(File.Exists(filePath));
        }
    }
}
