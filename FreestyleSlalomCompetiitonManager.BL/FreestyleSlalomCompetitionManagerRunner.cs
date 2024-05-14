using FreestyleSlalomCompetitionManager.BL.Impotrs;
using FreestyleSlalomCompetitionManager.BL.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL
{
    public class FreestyleSlalomCompetitionManagerRunner
    {
        private readonly ConcurrentDictionary<string, Skater> existingSkaters = new();
        private Competition? currentCompetition;

        public async Task RunAsync()
        {
            ConsoleCommunicator.DisplayWelcomeMessageAndHelp();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                await ExecuteCommandAsync(command, parts[1..]);
            }
        }

        private async Task ExecuteCommandAsync(string command, string[] args)
        {
            switch (command)
            {
                case "help":
                    ConsoleCommunicator.DisplayHelp();
                    break;
                case "importfolder":
                    await ImportFromFolderAsync(args);
                    break;
                case "importfile":
                    await ImportFromFileAsync(args);
                    break;
                case "createcompetition":
                    _ = CreateCompetitionAsync(args);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleCommunicator.DisplayUnknownCommandMessage();
                    break;
            }
        }

        private async Task ImportFromFolderAsync(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleCommunicator.DisplayFolderPathMissingMessage();
                return;
            }

            string folderPath = args[0];
            await ImportCSVWorldRankings.ImportFromFolderAsync(folderPath, existingSkaters);
        }

        private async Task ImportFromFileAsync(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleCommunicator.DisplayFilePathMissingMessage();
                return;
            }

            string filePath = args[0];
            try
            {
                List<WorldRank> worldRanks = await ImportCSVWorldRankings.ImportAsync(filePath, existingSkaters);
                ConsoleCommunicator.DisplayImportSuccessMessage(worldRanks.Count, filePath);
            }
            catch (Exception ex)
            {
                ConsoleCommunicator.DisplayImportErrorMessage(filePath, ex.Message);
            }
        }

        private async Task CreateCompetitionAsync(string[] args)
        {
            if (args.Length < 7)
            {
                ConsoleCommunicator.DisplayCompetitionDetailsMissingMessage();
                return;
            }

            string name = args[0];
            if (!DateTime.TryParse(args[1], out DateTime startDate) || !DateTime.TryParse(args[2], out DateTime endDate))
            {
                ConsoleCommunicator.DisplayInvalidDateFormatMessage();
                return;
            }

            string description = args[3];
            string address = args[4];
            string organizerName = args[5];
            string organizerWsid = args[6];
            Organizer organizer = new(organizerWsid, organizerName);

            CreateCompetition(name, startDate, endDate, description, address, organizer);
        }

        private void CreateCompetition(string name, DateTime startDate, DateTime endDate, string description, string address, Organizer organizer)
        {
            currentCompetition = new(name, startDate, endDate, description, address, organizer);
            ConsoleCommunicator.DisplayCompetitionCreationSuccessMessage(currentCompetition.Name);
        }

        private void AddNewSkater(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Please provide the skater details in the format: newskater <WSID> <name> <country>");
                return;
            }

            string wsid = args[1];
            string name = args[2];
            string country = args[3];

            if (existingSkaters.ContainsKey(wsid))
            {
                Console.WriteLine($"Skater with WSID '{wsid}' already exists.");
                return;
            }

            Skater newSkater = new (name, country, wsid);
            existingSkaters.TryAdd(wsid, newSkater);

            Console.WriteLine($"New skater '{name}' added successfully with WSID '{wsid}'.");
        }
    }
}
