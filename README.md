# Freestyle Slalom Competition Manager

The Freestyle Slalom Competition Manager is a command-line application designed to help manage freestyle slalom competitions. It allows users to create competitions, add new skaters, and manage existing skaters within the competition.

## Features

- **Import Data from CSV**: Users can import data from a CSV file or a folder containing multiple CSV files. The `importfile` and `importfolder` commands are used for this purpose.

- **Create Competition**: Users can create a new competition using the `createcompetition` command. They need to provide the name, start date, end date, description, address, and organizer details.

- **Add New Skater**: Users can add a new skater to the existing skaters list using the `newskater` command. They need to provide the WSID, name, and country of the skater.

- **Add Existing Skater to Competition**: Users can add an existing skater to the current competition using the `skatertocompetition` command. They need to provide the WSID of the skater.

- **Export Data to CSV**: Users can export the skaters of the current competition to a CSV file using the `export` command. They need to provide the file path where the CSV file will be saved.

- **Import Skaters to Competition**: Users can import skaters to the current competition from a CSV file using the `importskatertoskateroncompetition` command. They need to provide the file path of the CSV file.

- **Link Music to Skater**: Users can link a music file to a skater in the current competition using the `linkmusictowsid` command. They need to provide the WSID of the skater and the path of the music file.

- **Help**: Users can display the help message using the `help` command. The help message contains the list of available commands and their usage.



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

The application is command-line based and supports the following commands:

- `help`: Displays a list of available commands.
- `importfolder <folder>`: Imports world rankings from the specified folder.
- `importfile <file>`: Imports world rankings from the specified file.
- `createcompetition <name> <start_date> <end_date> <description> <address> <organizer_wsid> <organizer_name>`: Creates a new competition with the specified details.
- `newskater <WSID> <name> <country>`: Adds a new skater.
- `skatertocompetition <WSID>`: Adds an existing skater to the competition.
- `export <file>`: Exports skaters to a CSV file.
- `importskatertoskateroncompetition <file>`: Imports skaters to the current competition from the specified file.
- `linkmusictowsid <WSID> <music_path>`: Links music to a skater with the specified WSID.
- `exit`: Exits the program.

