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
        [InlineData("addexistingskater", "Please provide the WSID")]
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
            var runner = new FreestyleSlalomCompetitionManagerRunner();
            runner.currentCompetition = new Competition("TestCompetition", DateTime.Now, DateTime.Now, "Description", "Address", new("Organizer", "WSID"));
            runner.existingSkaters.TryAdd("WSID", new Skater("Name", "Country", "WSID"));
            var input = "addexistingskater WSID".Split(' ');

            // Act
            await runner.ExecuteCommandAsync(input[0], input[1..]);
            var output = consoleOut.ToString().Trim();

            // Assert
            Assert.Contains("Skater with WSID 'WSID' added to the competition 'TestCompetition'.", output);
            Assert.True(runner.currentCompetition?.Skaters.Any(skater => skater.WSID == "WSID"));
        }
    }
}
