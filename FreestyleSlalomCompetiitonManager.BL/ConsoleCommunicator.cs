using System;
using System.IO;

namespace FreestyleSlalomCompetitionManager.BL
{
    public static class ConsoleCommunicator
    {
        public static void DisplayWelcomeMessageAndHelp()
        {
            Console.WriteLine("Welcome to the Freestyle Slalom Competition Manager!");
            DisplayHelp();
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help                      - Display this help message");
            Console.WriteLine("  importfolder <folder>     - Import world rankings from the specified folder");
            Console.WriteLine("  importfile <file>         - Import world rankings from the specified file");
            Console.WriteLine("  createcompetition <args>  - Create a new competition with the specified details");
            Console.WriteLine("  exit                      - Exit the program");
        }

        public static void DisplayUnknownCommandMessage()
        {
            Console.WriteLine("Unknown command. Type 'help' to see available commands.");
        }

        public static void DisplayFolderPathMissingMessage()
        {
            Console.WriteLine("Please provide the folder path to import from.");
        }

        public static void DisplayFilePathMissingMessage()
        {
            Console.WriteLine("Please provide the file path to import.");
        }

        public static void DisplayImportSuccessMessage(int count, string filePath)
        {
            Console.WriteLine($"Successfully imported {count} world rankings from file: {filePath}");
        }

        public static void DisplayImportErrorMessage(string filePath, string errorMessage)
        {
            Console.WriteLine($"Error importing world rankings from file: {filePath}. Error: {errorMessage}");
        }

        public static void DisplayCompetitionDetailsMissingMessage()
        {
            Console.WriteLine("Please provide the competition details in the format: createcompetition <name> <start_date> <end_date> <description> <address> <organizer>");
        }

        public static void DisplayInvalidDateFormatMessage()
        {
            Console.WriteLine("Invalid date format. Please provide dates in the format: yyyy-MM-dd HH:mm:ss");
        }

        public static void DisplayCompetitionCreationSuccessMessage(string competitionName)
        {
            Console.WriteLine($"Competition '{competitionName}' created successfully.");
        }

        public static void DisplaySkaterDetailsMissingMessage()
        {
            Console.WriteLine("Please provide the skater details in the format: newskater <WSID> <name> <country>");
        }

        public static void DisplaySkaterAlreadyExistsMessage(string wsid)
        {
            Console.WriteLine($"Skater with WSID '{wsid}' already exists.");
        }

        public static void DisplaySkaterCreationSuccessMessage(string name, string wsid)
        {
            Console.WriteLine($"New skater '{name}' added successfully with WSID '{wsid}'.");
        }

        public static void DisplaySkaterWsidMissingMessage()
        {
            Console.WriteLine("Please provide the WSID of the skater to add.");
        }

        public static void DisplaySkaterNotFoundMessage(string wsid)
        {
            Console.WriteLine($"Skater with WSID '{wsid}' not found.");
        }

        public static void DisplayNoCompetitionCreatedMessage()
        {
            Console.WriteLine("No competition created. Please create a competition first.");
        }

        public static void DisplaySkaterAddedToCompetitionMessage(string wsid, string competitionName)
        {
            Console.WriteLine($"Skater with WSID '{wsid}' added to the competition '{competitionName}'.");
        }
    }
}
