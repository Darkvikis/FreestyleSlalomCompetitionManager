﻿using FreestyleSlalomCompetitionManager.BL;
using FreestyleSlalomCompetitionManager.BL.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class FreestyleSlalomCompetitionManagerRunnerTests
    {
        private readonly StringWriter consoleOut;

        public FreestyleSlalomCompetitionManagerRunnerTests()
        {
            consoleOut = new StringWriter();
            Console.SetOut(consoleOut);
        }

        [Theory]
        [InlineData("help", "Available commands:")]
        [InlineData("importfolder invalidpath", "Please provide valid folder path to import from.")]
        [InlineData("importfile invalidpath", "Please provide valid file path to import from.")]
        [InlineData("createcompetition", "Please provide the competition details")]
        [InlineData("newskater", "Please provide the skater details")]
        [InlineData("skatertocompetition", "Please provide the WSID")]
        [InlineData("export", "Please provide the file path")]
        [InlineData("importskatertocompetition", "Please provide the file path")]
        [InlineData("linkmusictowsid", "Please provide the WSID and music path")]
        [InlineData("getskatersoncurrentcompetition", "No active competition to work with.")]
        [InlineData("getrankingsforcurrentcompetition", "No active competition to work with.")]
        [InlineData("createbasedisciplines", "No active competition to work with.")]
        [InlineData("assignskaterstodisciplines", "No active competition to work with.")]

        public async Task ExecuteCommandAsync_InvalidInput_ReturnsExpectedMessage(string command, string expectedOutput)
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();

            // Act
            await runner.ExecuteCommandAsync(command.Split(' ')[0], command.Split(' ')[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains(expectedOutput, output);
        }

        [Fact]
        public async Task ExecuteCommandAsync_CreateCompetitionCommand_ValidArgs_ShouldCreateCompetition()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            var input = "createcompetition TestCompetition 2024-06-01 2024-06-02 Description Address OrganizerName OrganizerWSID".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Competition 'TestCompetition' created successfully.", output);
            Assert.True(runner.currentCompetition != null);
        }

        [Fact]
        public async Task ExecuteCommandAsync_NewSkaterCommand_ValidArgs_ShouldAddNewSkater()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            var input = "newskater WSID Name Country".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("New skater 'Name' added successfully with WSID 'WSID'.", output);
            Assert.True(runner.existingSkaters.ContainsKey("WSID"));
        }

        [Fact]
        public async Task ExecuteCommandAsync_AddExistingSkaterCommand_ValidArgs_ShouldAddExistingSkaterToCompetition()
        {
            // Arrange
            FreestyleSlalomCompetitionManagerRunner runner = new()
            {
                currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new("Organizer", "WSID"))
            };
            runner.existingSkaters.TryAdd("WSID", new Skater("Name", "Country", "WSID"));
            var input = "skatertocompetition WSID".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Skater with WSID 'WSID' added to the competition 'TestCompetition'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "WSID"));
        }

        [Fact]
        public async Task ExecuteCommandAsync_ExportSkatersToCsv_ShouldExportSkaters()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);
            var competitionInput = "createcompetition TestCompetition 2024-06-01 2024-06-02 Description Address OrganizerName OrganizerWSID";
            await runner.ExecuteCommandAsync(competitionInput.Split(' ')[0], competitionInput.Split(' ')[1..]);

            var skaterInput = "newskater WSID Name Country";
            await runner.ExecuteCommandAsync(skaterInput.Split(' ')[0], skaterInput.Split(' ')[1..]);

            await runner.ExecuteCommandAsync("skatertocompetition", ["WSID"]);
            var filePath = "test_skaters.csv";

            // Act
            await runner.ExecuteCommandAsync("export", [filePath]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains($"Skaters have been exported to {filePath}", output);

            // Verify the CSV content
            var csvContent = File.ReadAllText(filePath);
            Assert.Contains("Name,Country,WSID,PayedFee,SendMusic,Music,CompetitionRankBattle,CompetitionRankSpeed,CompetitionRankClassic,CompetitionRankJump,CompetitionRankSlide", csvContent);
            Assert.Contains("Name,Country,WSID,False,No,,,,,,", csvContent);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public async Task ExecuteCommandAsync_ImportSkatersFromCsv_ShouldImportSkaters()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);
            var competitionInput = "createcompetition TestCompetition 2024-06-01 2024-06-02 Description Address OrganizerName OrganizerWSID";
            await runner.ExecuteCommandAsync(competitionInput.Split(' ')[0], competitionInput.Split(' ')[1..]);
            var filePath = "C:\\Users\\Lenovo\\Source\\repos\\Darkvikis\\FreestyleSlalomCompetitionManager\\FreestyleSlalomCompetitionManager.Test\\FilesToImport\\RegistrationExcels\\RandomlyGeneratedRegistrations.csv";
            // Act
            await runner.ExecuteCommandAsync("importskatertocompetition", [filePath]);
            var output = consoleOut.ToString().Trim();
            // Assert
            Assert.Contains($"Skaters have been imported from {filePath} to competition 'TestCompetition'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "32004POL9834671234"));
        }

        [Fact]
        public async Task ExecuteCommandAsync_LinkMusicToSkater_ShouldLinkMusicToSkater()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);
            var competitionInput = "createcompetition TestCompetition 2024-06-01 2024-06-02 Description Address OrganizerName OrganizerWSID";
            await runner.ExecuteCommandAsync(competitionInput.Split(' ')[0], competitionInput.Split(' ')[1..]);
            var skaterInput = "newskater WSID Name Country";
            await runner.ExecuteCommandAsync(skaterInput.Split(' ')[0], skaterInput.Split(' ')[1..]);
            await runner.ExecuteCommandAsync("skatertocompetition", ["WSID"]);
            var musicPath = "test_music.mp3";
            // Act
            await runner.ExecuteCommandAsync("linkmusictowsid", ["WSID", musicPath]);
            var output = consoleOut.ToString().Trim();
            // Assert
            Assert.Contains($"Music '{musicPath}' linked to skater with WSID 'WSID'.", output);

        }

        [Fact]
        public async Task ExecuteCommandAsync_AddExistingSkaterToCompetitionCommand_ValidArgs_ShouldAddExistingSkaterToCompetition()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new Organizer("Organizer", "WSID"));
            runner.existingSkaters.TryAdd("WSID", new Skater("Name", "Country", "WSID"));
            var input = "skatertocompetition WSID".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Skater with WSID 'WSID' added to the competition 'TestCompetition'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "WSID"));
        }

        [Fact]
        public async Task ExecuteCommandAsync_ExportSkatersToCsvCommand_ValidArgs_ShouldExportSkatersToCsv()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new Organizer("Organizer", "WSID"));
            runner.currentCompetition.Skaters.Add(new Competitor("Name", "Country") { WSID = "WSID" });
            var input = "export test.csv".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Skaters have been exported to test.csv", output);
            Assert.True(File.Exists("test.csv"));

            // Cleanup
            File.Delete("test.csv");
        }

        [Fact]
        public async Task ExecuteCommandAsync_ImportSkatersToCompetitionCommand_ValidArgs_ShouldImportSkatersToCompetition()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new Organizer("Organizer", "WSID"));

            var filePath = "C:\\Users\\Lenovo\\Source\\repos\\Darkvikis\\FreestyleSlalomCompetitionManager\\FreestyleSlalomCompetitionManager.Test\\FilesToImport\\RegistrationExcels\\RandomlyGeneratedRegistrations.csv";

            // Act
            await runner.ExecuteCommandAsync("importskatertocompetition", [filePath]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains($"Skaters have been imported from {filePath} to competition 'TestCompetition'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "32011ESP7482931456"));
        }

        [Fact]
        public async Task ExecuteCommandAsync_LinkMusicToSkaterCommand_ValidArgs_ShouldLinkMusicToSkater()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new Organizer("Organizer", "WSID"));
            runner.currentCompetition.Skaters.Add(new Competitor("Name", "Country") { WSID = "WSID" });
            var input = "linkmusictowsid WSID test.mp3".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Music 'test.mp3' linked to skater with WSID 'WSID'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "WSID" && skater.Music == "test.mp3"));
        }

        [Fact]
        public void ExecuteCommandAsync_GetSkatersOnCurrentCompetitionCommand_ShouldDisplaySkatersOnCurrentCompetition()
        {
            // Arrange
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new Organizer("Organizer", "WSID"));
            runner.currentCompetition.Skaters.Add(new Competitor("Name", "Country") { WSID = "WSID" });

            // Act
            runner.ExecuteCommandAsync("getskatersoncurrentcompetition", new string[0]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Name: Name", output);
            Assert.Contains("Country: Country", output);
            Assert.Contains("WSID: WSID", output);
        }
    }
}
