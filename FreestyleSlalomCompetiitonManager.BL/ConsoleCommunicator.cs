using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;
using System.Data;

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
            Console.WriteLine("  createcompetition <name> <start_date> <end_date> <description> <address> <organizer_wsid> <organizer_name> - Create a new competition with the specified details");
            Console.WriteLine("  newskater <WSID> <name> <country> - Add a new skater");
            Console.WriteLine("  skatertocompetition <WSID> - Add an existing skater to the competition");
            Console.WriteLine("  export <file>             - Export skaters to a CSV file");
            Console.WriteLine("  importskatertocompetition <file> - Import skaters to the current competition from the specified file");
            Console.WriteLine("  linkmusictowsid <WSID> <music_path> - Link music to a skater with the specified WSID");
            Console.WriteLine("  getskatersoncurrentcompetition - Get the skaters on the current competition");
            Console.WriteLine("  getrankingsforcurrentcompetition - Get the rankings for the current competition");
            Console.WriteLine("  createbasedisciplines         - Create base disciplines");
            Console.WriteLine("  assignskaterstodisciplines - Automatically assign skaters to their disciplines based on imported skaters and their ranks");
            Console.WriteLine("  changedefaultfolderpath <folder> - Change the default folder path for imports and exports");
            Console.WriteLine("  createdisciplineforcurrentcompetition - Create a new discipline for the current competition");
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

        public static void DisplayNoActiveCompetitionMessage()
        {
            Console.WriteLine("No active competition to work with.");
        }

        public static void DisplayFilePathMissingExportMessage()
        {
            Console.WriteLine("Please provide the file path to export to.");
        }

        public static void DisplaySkatersExportedMessage(string filePath)
        {
            Console.WriteLine($"Skaters have been exported to {filePath}");
        }

        public static void DisplaySkatersImportedMessage(string filePath, string competitionName)
        {
            Console.WriteLine($"Skaters have been imported from {filePath} to competition '{competitionName}'.");
        }

        public static void DisplaySkaterWsidAndMusicPathMissingMessage()
        {
            Console.WriteLine("Please provide the WSID and music path of the skater.");
        }

        public static void DisplayMusicLinkedToSkaterMessage(string skaterWsid, string musicPath)
        {
            Console.WriteLine($"Music '{musicPath}' linked to skater with WSID '{skaterWsid}'.");
        }

        public static void DisplaySkaterDetails(Competitor skater)
        {
            Console.WriteLine($"Skater Name: {skater.FirstName} {skater.FamilyName}, Country: {skater.Country}, WSID: {skater.WSID}");
        }

        public static void DisplayStandardDisciplinesCreatedMessage()
        {
            Console.WriteLine("Standard disciplines have been created.");
        }
        public static void DisplayNoDisciplinesCreatedMessage()
        {
            Console.WriteLine("No disciplines have been created. Please create disciplines first.");
        }

        public static void DisplayNoSkatersAddedToCompetitionMessage()
        {
            Console.WriteLine("No skaters have been added to the competition. Please add skaters first.");
        }

        public static void DisplayDisciplinesAndSkaters(IEnumerable<BaseDiscipline> disciplines, IEnumerable<Competitor> skaters)
        {
            Console.WriteLine("Available disciplines:");
            foreach (var discipline in disciplines)
            {
                Console.WriteLine($"  {nameof(discipline) + discipline.AgeCategory + discipline.SexCategory}");
            }

            Console.WriteLine("Available skaters:");
            foreach (var skater in skaters)
            {
                Console.WriteLine($" {skater.FirstName} {skater.FamilyName} - {skater.WSID}");
            }
        }

        public static void DisplayDisciplineNotFoundMessage(string disciplineName)
        {
            Console.WriteLine($"Discipline '{disciplineName}' not found.");
        }

        public static void DisplaySkaterAlreadyAssignedToDisciplineMessage(string skaterName, string disciplineName)
        {
            Console.WriteLine($"Skater '{skaterName}' is already assigned to discipline '{disciplineName}'.");
        }

        public static void DisplaySkaterAssignedToDisciplineMessage(string skaterName, string disciplineName)
        {
            Console.WriteLine($"Skater '{skaterName}' has been assigned to discipline '{disciplineName}'.");
        }

        public static void DisplaySkaterAssignedToDisciplinesMessage(string skaterName)
        {
            Console.WriteLine($"Skater '{skaterName}' has been assigned to disciplines.");
        }

        public static void DisplaySkaterNotAssignedToDisciplinesMessage(string skaterName)
        {
            Console.WriteLine($"Skater '{skaterName}' has NOT been assigned to disciplines. Not found.");
        }

        public static void DisplayNumberOfSkatersThatWereAssignedRankingsMessage(int count)
        {
            Console.WriteLine($"{count} skaters were assigned rankings.");
        }

        public static void DisplayStartDiscipline(string disciplineName, string ageCategory, string sexCategory)
        {
            Console.WriteLine($"Starting {disciplineName} - Age Category: {ageCategory}, Sex Category: {sexCategory}");
        }

        public static void DisplayStartRound(string roundName)
        {
            Console.WriteLine($"Starting round: {roundName}");
        }

        public static void DisplayStartGroup()
        {
            Console.WriteLine("Starting group:");
        }

        public static void DisplayCompetitor(int rank, string fullName)
        {
            Console.WriteLine($"{rank}: {fullName}");
        }

        public static void DisplayEndDiscipline(string disciplineName, string ageCategory, string sexCategory)
        {
            Console.WriteLine($"End of {disciplineName} - Age Category: {ageCategory}, Sex Category: {sexCategory}");
        }

        public static void DisplayEndRound()
        {
            Console.WriteLine("End of round.");
        }

        public static string? AskForBattleResults()
        {
            Console.WriteLine("Please enter the battle results (1 4 3 2):");
            return Console.ReadLine();
        }

        public static bool DisplayEndGroup(List<BattleGroupCompetitor> competitors)
        {
            Console.WriteLine("End of group:");
            foreach (var competitor in competitors)
            {
                Console.WriteLine($"{competitor.RankInGroup}: {competitor.Competitor.FirstName} {competitor.Competitor.FamilyName}");
            }

            string? input;
            do
            {
                Console.WriteLine("Do you accept these results? (y/n)");
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input) || !input.Equals("y", StringComparison.InvariantCultureIgnoreCase) && !input.Equals("n", StringComparison.InvariantCultureIgnoreCase));

            return input.Equals("y", StringComparison.CurrentCultureIgnoreCase);
        }

        public static void DisplayInvalidFolderPathMessage()
        {
            Console.WriteLine("Invalid folder path. Please provide a valid folder path.");
        }

        public static int ClassicRunGetMark(Competitor competitor)
        {
            Console.WriteLine($"{competitor.FirstName} {competitor.FamilyName} mark:");
            string? read = Console.ReadLine();
            int mark;

            while (!int.TryParse(read, out mark))
            {
                Console.WriteLine("Invalid number. Give valid mark:");
                read = Console.ReadLine();
            }
            return mark;
        }

        public static void DisplayNotEnoughCompetitorsForDiscipline()
        {
            Console.WriteLine("Not enough competitors for this discipline.");
        }

        public static string AskForDefaultFolderPath()
        {
            Console.WriteLine("Give default folder path");
            string? read = Console.ReadLine();

            while (!Directory.Exists(read))
            {
                Console.WriteLine("Invalid folder path. Fiolder does not exists.");
                read = Console.ReadLine();
            }

            return read;
        }

        public static BaseDiscipline CreateDiscipline()
        {
            Console.WriteLine("What AgeCategory you want (senior, junior):");
            string? read = Console.ReadLine();

            AgeCategory ageCategory = read?.ToLower() switch
            {
                "senior" => AgeCategory.Senior,
                "junior" => AgeCategory.Junior,
                _ => throw new ArgumentException("Invalid AgeCategory.")
            };

            Console.WriteLine("What SexCategory you want (men, women):");
            read = Console.ReadLine();
            SexCategory sexCategory = read?.ToLower() switch
            {
                "men" => SexCategory.Man,
                "women" => SexCategory.Woman,
                _ => throw new ArgumentException("Invalid AgeCategory.")
            };

            Console.WriteLine("What discipline you want (battle, classic, speed, jump):");
            read = Console.ReadLine();

            BaseDiscipline discipline = read?.ToLower() switch
            {
                "battle" => new Battle(ageCategory, sexCategory),
                "classic" => new Classic(ageCategory, sexCategory),
                "speed" => new Speed(ageCategory, sexCategory),
                "jump" => new Jump(ageCategory, sexCategory),
                _ => throw new ArgumentException("Invalid discipline."),
            };

            return discipline;
        }

        public static void ListAllDisciplines(List<BaseDiscipline> disciplines)
        {
            for (int i = 0; i < disciplines.Count; i++)
            {
                Console.WriteLine($"{i}: {disciplines[i].FullNameOfDiscipline}");
            }
        }

        public static int PickDiscipline(List<BaseDiscipline> disciplines)
        {
            ListAllDisciplines(disciplines);
            Console.WriteLine("Pick corresponding number:");
            var read = Console.ReadLine();
            int discNum;

            while (!int.TryParse(read, out discNum))
            {
                Console.WriteLine("Invalid number. Give valid discipline number:");
                read = Console.ReadLine();
            }
            return discNum;
        }

        public static bool WantToContinue()
        {
            Console.WriteLine("Do you wan to continue (y/n)?");
            var read = Console.ReadLine();
            bool discNum;

            discNum = read?.ToLower() switch
            {
                "y" => true,
                _ => false
            };

            return discNum;

        }

        public static void CannotStartCompetition()
        {
            Console.WriteLine("Cannot start the competition. Please make sure there are competitors and disciplines available.");
        }

        internal static void InvalidSkaterMissingWSID()
        {
            Console.WriteLine("Invalid skater. WSID is missing.");
        }
    }
}
