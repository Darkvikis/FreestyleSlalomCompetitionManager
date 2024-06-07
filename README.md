# Freestyle Slalom Competition Manager

The Freestyle Slalom Competition Manager is a command-line application designed to help manage freestyle slalom competitions. It allows users to create competitions, add new skaters, and manage existing skaters within the competition.

## Features

- Create competitions: Users can create new competitions by providing details such as the competition name, start date, end date, description, address, organizer name, and organizer WSID.
- Add skaters: The application allows users to add new skaters to the competition. Users need to provide the skater's WSID (World Skate ID), first name, family name, and country.
- Manage skaters: Users can manage existing skaters within the competition. They can add an existing skater to the current competition, link music to a skater, display the skaters in the current competition, and get rankings for the skaters in the current competition.
- Import and export skaters: Users can import skaters from a folder or a file. They can also export skaters to a CSV file.
- Import and export starting lists: The application allows users to import skaters from a CSV file and add them to the current competition. Users can also export starting lists for the current competition.
- Create disciplines: Users can create base disciplines for the current competition. They can also create a new discipline for the current competition.
- Assign skaters to disciplines: Users can assign skaters to disciplines in the current competition.
- Change default folder path: Users have the option to change the default folder path for exporting files.
- Run competition: Users can run the current competition.
- Help and exit: The application provides a help command to display a help message with all available commands. Users can also exit the program using the exit command.


## Getting Started

### Prerequisites

- .NET Core SDK 3.1 or later

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/FreestyleSlalomCompetitionManager.git
   cd FreestyleSlalomCompetitionManager
   ```

2. Restore the dependencies:

   ```bash
   dotnet restore
   ```

### Building the Project

To build the project, run:

```bash
dotnet build
```

### Running the Application

To run the application, use:

```bash
dotnet run --project FreestyleSlalomCompetitionManager
```

### Running Tests

To run the tests, use:

```bash
dotnet test
```

## Usage

Once the application is running, you can use the following commands:

-  `help` : Display this help message
-  `importfolder <folderPath>` : Import skaters from a folder
-  `importfile <filePath>` : Import skaters from a file
-  `createcompetition <name> <startDate> <endDate> <description> <address> <organizerName> <organizerWsid>` : Create a new competition
-  `newskater <wsid> <firstName> <familyName> <country>` : Add a new skater
-  `skatertocompetition <skaterWsid>` : Add an existing skater to the current competition
-  `export <filePath>` : Export skaters to a CSV file
-  `importskatertocompetition <filePath>` : Import skaters from a CSV file and add them to the current competition
-  `linkmusictowsid <skaterWsid> <musicPath>` : Link music to a skater
-  `getskatersoncurrentcompetition` : Display the skaters in the current competition
-  `getrankingsforcurrentcompetition` : Display the rankings for the skaters in the current competition
-  `createbasedisciplines` : Create base disciplines for the current competition
-  `assignskaterstodisciplines` : Assign skaters to disciplines in the current competition
-  `exportstartinglists` : Export starting lists for the current competition
-  `changedefaultfolderpath` : Change the default folder path for exporting files
-  `createdisciplineforcompetition` : Create a new discipline for the current competition
-  `runcompetition` : Run the current competition
-  `exit` : Exit the program

To execute a command, type the command followed by the required parameters. For example, to import world rankings from a folder, you would type `importfolder <folder>`, replacing `<folder>` with the actual folder path.


