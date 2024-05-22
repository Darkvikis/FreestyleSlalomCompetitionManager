using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Impotrs;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
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
        public readonly ConcurrentDictionary<string, Skater> existingSkaters = new();
        public Competition? currentCompetition;

        public async Task RunAsync()
        {
            ConsoleCommunicator.DisplayWelcomeMessageAndHelp();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                List<string> parts = [];
                bool inQuotes = false;
                StringBuilder currentPart = new();

                foreach (char c in input)
                {
                    if (c == ' ' && !inQuotes)
                    {
                        if (currentPart.Length > 0)
                        {
                            parts.Add(currentPart.ToString());
                            currentPart.Clear();
                        }
                    }
                    else if (c == '"')
                    {
                        inQuotes = !inQuotes;
                    }
                    else
                    {
                        currentPart.Append(c);
                    }
                }

                if (currentPart.Length > 0)
                {
                    parts.Add(currentPart.ToString());
                }

                string command = parts[0].ToLower();

                await ExecuteCommandAsync(command, parts.GetRange(1, parts.Count - 1).ToArray());
            }
        }

        public async Task ExecuteCommandAsync(string command, string[] args)
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
                    CreateCompetition(args);
                    break;
                case "newskater":
                    AddNewSkater(args);
                    break;
                case "skatertocompetition":
                    AddExistingSkaterToCompetition(args);
                    break;
                case "export":
                    ExportSkatersToCsv(args);
                    break;
                case "importskatertocompetition":
                    ImportSkatersToCompetition(args);
                    break;
                case "linkmusictowsid":
                    LinkMusicToSkater(args);
                    break;
                case "getskatersoncurrentcompetition":
                    GetSkatersOnCurrentCompetition();
                    break;
                case "getrankingsforcurrentcompetition":
                    GetRankingsForCurrentCompetition();
                    break;
                case "createbasedisciplines":
                    CreateBaseDisciplines();
                    break;
                case "assignskaterstodisciplines":
                    AssignSkatersToDisciplines();
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
            try
            {
                var worldRanks = await ImportCSVWorldRankings.ImportFromFolderAsync(folderPath, existingSkaters);
                ConsoleCommunicator.DisplayImportSuccessMessage(worldRanks.Count, folderPath);
            }
            catch (Exception ex)
            {
                ConsoleCommunicator.DisplayImportErrorMessage(folderPath, ex.Message);
            }
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
        private void CreateCompetition(string[] args)
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
            if (args.Length < 3)
            {
                ConsoleCommunicator.DisplaySkaterDetailsMissingMessage();
                return;
            }

            string wsid = args[0];
            string name = args[1];
            string country = args[2];

            if (existingSkaters.ContainsKey(wsid))
            {
                ConsoleCommunicator.DisplaySkaterAlreadyExistsMessage(wsid);
                return;
            }

            Skater newSkater = new(name, country, wsid);
            existingSkaters.TryAdd(wsid, newSkater);

            ConsoleCommunicator.DisplaySkaterCreationSuccessMessage(name, wsid);
        }

        private void AddExistingSkaterToCompetition(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleCommunicator.DisplaySkaterWsidMissingMessage();
                return;
            }

            string skaterWsid = args[0];

            if (!existingSkaters.TryGetValue(skaterWsid, out Skater skater))
            {
                ConsoleCommunicator.DisplaySkaterNotFoundMessage(skaterWsid);
                return;
            }

            if (!CurrentCompetitionCheck()) { return; }

            Competitor competitor = new(skater.Name, skater.Country)
            {
                WSID = skater.WSID,
            };

            currentCompetition.Skaters.Add(competitor);

            ConsoleCommunicator.DisplaySkaterAddedToCompetitionMessage(skaterWsid, currentCompetition.Name);
        }
        private void ExportSkatersToCsv(string[] args)
        {

            if (args.Length < 1)
            {
                ConsoleCommunicator.DisplayFilePathMissingExportMessage();
                return;
            }
            if (!CurrentCompetitionCheck()) { return; }

            string filePath = args[0];
            CsvExporter.ExportSkatersToCsv(currentCompetition, filePath);
            ConsoleCommunicator.DisplaySkatersExportedMessage(filePath);
        }


        private void ImportSkatersToCompetition(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleCommunicator.DisplayFilePathMissingMessage();
                return;
            }

            string filePath = args[0];
            List<Competitor> skaters = ImportCSVIntoSkaterOnCompetition.ImportCSV(filePath);
            if (!CurrentCompetitionCheck()) { return; }

            foreach (var skater in skaters)
            {
                currentCompetition.Skaters.Add(skater);
            }

            ConsoleCommunicator.DisplaySkatersImportedMessage(filePath, currentCompetition.Name);
        }

        private void LinkMusicToSkater(string[] args)
        {
            if (args.Length < 2)
            {
                ConsoleCommunicator.DisplaySkaterWsidAndMusicPathMissingMessage();
                return;
            }

            if (!CurrentCompetitionCheck()) { return; }

            string skaterWsid = args[0];
            string musicPath = args[1];

            if (!currentCompetition.Skaters.Any(x => x.WSID == skaterWsid))
            {
                ConsoleCommunicator.DisplaySkaterNotFoundMessage(skaterWsid);
                return;
            }

            currentCompetition.Skaters?.FirstOrDefault()?
                .SetMusic(musicPath, currentCompetition.StartDate);

            ConsoleCommunicator.DisplayMusicLinkedToSkaterMessage(skaterWsid, musicPath);
        }

        private void GetSkatersOnCurrentCompetition()
        {
            if (!CurrentCompetitionCheck()) { return; }

            if (currentCompetition.Skaters.Count == 0)
            {
                ConsoleCommunicator.DisplayNoSkatersAddedToCompetitionMessage();
                return;
            }
            foreach (var skater in currentCompetition.Skaters)
            {
                ConsoleCommunicator.DisplaySkaterDetails(skater);
            }
        }

        private bool CurrentCompetitionCheck()
        {
            if (currentCompetition == null)
            {
                ConsoleCommunicator.DisplayNoActiveCompetitionMessage();
                return false;
            }
            return true;
        }
        private void GetRankingsForCurrentCompetition()
        {
            if (!CurrentCompetitionCheck()) { return; }

            if (currentCompetition.Skaters.Count == 0)
            {
                ConsoleCommunicator.DisplayNoSkatersAddedToCompetitionMessage();
                return;
            }
            int counterOfAssingedRankings = 0;
            foreach (var skater in currentCompetition.Skaters)
            {
                if (skater.WSID != null && existingSkaters.TryGetValue(skater.WSID, out Skater WSRankSkater))
                {
                    skater.CompetitionRankBattle = GetRankForDiscipline(WSRankSkater, Discipline.Battle, skater.AgeCategory, skater.SexCategory);
                    skater.CompetitionRankSpeed = GetRankForDiscipline(WSRankSkater, Discipline.Speed, skater.AgeCategory, skater.SexCategory);
                    skater.CompetitionRankClassic = GetRankForDiscipline(WSRankSkater, Discipline.Classic, skater.AgeCategory, skater.SexCategory);
                    skater.CompetitionRankJump = GetRankForDiscipline(WSRankSkater, Discipline.Jump, skater.AgeCategory, skater.SexCategory);
                    skater.CompetitionRankSlide = GetRankForDiscipline(WSRankSkater, Discipline.Slide, skater.AgeCategory, skater.SexCategory);
                    counterOfAssingedRankings++;
                    ConsoleCommunicator.DisplaySkaterAssignedToDisciplinesMessage(skater.Name);
                }
            }
            ConsoleCommunicator.DisplayNumberOfSkatersThatWereAssignedRankingsMessage(counterOfAssingedRankings);
        }

        private static int? GetRankForDiscipline(Skater skater, Discipline discipline, AgeCategory ageCategory, SexCategory sexCategory)
        {
            return skater.WorldRanks.FirstOrDefault(x => x.Discipline == discipline && x.AgeCategory == ageCategory && x.SexCategory == sexCategory)?.Rank;
        }

        private void CreateBaseDisciplines()
        {
            if (!CurrentCompetitionCheck()) { return; }
            var ageCategories = Enum.GetValues(typeof(AgeCategory)).Cast<AgeCategory>();
            var sexCategories = Enum.GetValues(typeof(SexCategory)).Cast<SexCategory>();

            foreach (var ageCategory in ageCategories)
            {
                foreach (var sexCategory in sexCategories)
                {
                    var disciplines = GenerateBaseDisciplines(ageCategory, sexCategory);
                    currentCompetition?.Disciplines.AddRange(disciplines);
                }
            }

            ConsoleCommunicator.DisplayStandardDisciplinesCreatedMessage();
        }

        private static List<BaseDiscipline> GenerateBaseDisciplines(AgeCategory ageCategory, SexCategory sexCategory)
        {
            List<BaseDiscipline> disciplines = [];

            disciplines.Add(new Battle(ageCategory, sexCategory));
            disciplines.Add(new Speed(ageCategory, sexCategory));
            disciplines.Add(new Slide(ageCategory, sexCategory));

            return disciplines;
        }

        private void AssignSkatersToDisciplines()
        {
            if (!CurrentCompetitionCheck()) { return; }

            if (currentCompetition.Disciplines.Count == 0)
            {
                ConsoleCommunicator.DisplayNoDisciplinesCreatedMessage();
                return;
            }

            if (currentCompetition.Skaters.Count == 0)
            {
                ConsoleCommunicator.DisplayNoSkatersAddedToCompetitionMessage();
                return;
            }

            ConsoleCommunicator.DisplayDisciplinesAndSkaters(currentCompetition.Disciplines, currentCompetition.Skaters);

            foreach (var discipline in currentCompetition.Disciplines)
            {
                discipline.AssignCompetitors(currentCompetition.Skaters);
            }
        }

    }
}
